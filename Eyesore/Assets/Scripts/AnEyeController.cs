using UnityEngine;
using System.Collections;

public class AnEyeController : MonoBehaviour {
	public float moveForce = 365f;			// Amount of force added to move the player in four direction.
	public float maxSpeed = 5f;				// The fastest the player can travel.
	
	[HideInInspector]
	public bool stop = false;				// Condition for wheather the player should stop.
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// If the break button is pressed.
		if (Input.GetButtonDown("Break")) {
			stop = true;
		}
	}
	
	void FixedUpdate() {
		// If player should stop...
		if (stop) {
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			stop = false;
		} 
		
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		
		// If the player is changing direction (h has different sign to velocity.x) or hasn't reached maxSpeed yet...
		if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed) {
			// ... add a force to the player.
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
		}
		
		// If the player's horizontal velocity is greater than the maxSpeed...
		if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed) {
			// ... set the player's velocity to the maxSpeed in the x axis.
			GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
		}
		
		// If the player is changing direction in the y axis or hasn't reached maxSpeed yet...
		if (h * GetComponent<Rigidbody2D>().velocity.y < maxSpeed) {
			// ... add a force to the player.
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * v * moveForce);
		}
		
		// If the player's vertical velocity is greater than the maxSpeed...
		if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > maxSpeed) {
			// ... set the player's velocity to the maxSpeed in the y axis.
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Mathf.Sign(GetComponent<Rigidbody2D>().velocity.y) * maxSpeed);
		}
	}
}
