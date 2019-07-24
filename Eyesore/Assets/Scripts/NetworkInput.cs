using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Collections.Generic;


public class NetworkInput : MonoBehaviour {

	TcpClient client;
	Thread listenerThread;

	private Animator anim;
	private bool isListening;
	private String state = "";
	public String msg = "";
	private bool isStreaming = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void Awake () {
		anim = GetComponent<Animator>();
		try {
			isListening = true;
			Connect();
		} catch (Exception e) {
			Debug.Log("Cannot Connect to the input server.");
		}
	}

	public void Connect () {
		client = new TcpClient("localhost", 30000);

		ThreadStart t = new ThreadStart(Reads);
		listenerThread = new Thread(t);
		listenerThread.Start();
		Debug.Log("Connected to the input server. Start listening.");
		anim.SetBool("Connected", true);
	}

	public void Disconnect () {
		NetworkStream stream = client.GetStream();
		StreamWriter writer = new StreamWriter(stream);
		writer.Write("Close\r\n");
		client.Close();
		Debug.Log("Disconnected from the input server.");

	}

	public void Reads () {
		Debug.Log ("Start reading.");
		Debug.Log (isListening);
		while (isListening) {
			Debug.Log("Listening");
			NetworkStream stream = client.GetStream();
			if (stream.DataAvailable) {
				isStreaming = true;
				Debug.Log("Streaming detected.");
				StreamReader reader = new StreamReader(stream);
				msg = reader.ReadLine();
				state = msg;
				Debug.Log(msg);

				StreamWriter writer = new StreamWriter(stream);
				writer.Write("Ack\r\n");
				writer.Flush();
			}
		}
	}

	public float GetMovement (float axisValue) {
		switch (state) {
			case "Left" :
				anim.SetTrigger("Left");
				if (Mathf.Abs (axisValue) >= 1.00f) {
					axisValue = -1.00f;
				} else {
					axisValue -= 0.01f;
				}
				break;
			case "Right" :
				anim.SetTrigger("Right");
				if (Mathf.Abs (axisValue) >= 1.00f) {
					axisValue = 1.00f;
				} else {
					axisValue += 0.01f;
				}
			break;
			case "Blink" :
				anim.SetTrigger("Blink");
				axisValue = 0.0f;
				break;
			default :
				anim.SetTrigger("Idle");
				axisValue = 0.0f;
				break;
		}
		Debug.Log (axisValue);
		return axisValue;
	}

	public bool IsStreaming () {
		return isStreaming;
	}

	void OnApplicationQuit () {
		if (isListening) {
			Disconnect();
			isListening = false;
			listenerThread.Join(500);
		}
	}
}
