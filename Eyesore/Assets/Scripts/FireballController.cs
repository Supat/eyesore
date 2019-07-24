using UnityEngine;
using System.Collections;

public class FireballController : MonoBehaviour {
	
	public float moveForce = 365f;
	public float maxSpeed = 0.1f;
	public float velocity = 0.1f;
	public float acceleration = -0.01f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate () {
		if (GetComponent<Rigidbody2D>().velocity.y < maxSpeed) {
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * -velocity * moveForce);
			velocity -= acceleration;
		}
	}
	
	void OnExplode () {
		
	}
	
	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Floor") {
			//Debug.Log(rigidbody2D.velocity);
			Instantiate(Resources.Load("Explosion"), transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
