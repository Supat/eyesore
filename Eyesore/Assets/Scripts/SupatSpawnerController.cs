using UnityEngine;
using System.Collections;

public class SupatSpawnerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Supat(Clone)") == null) {
			Spawn (); 
		}
	}

	void Awake () {
		Spawn ();
	}

	public void Spawn () {
		Instantiate(Resources.Load("Supat"), transform.position, transform.rotation);
	}
}
