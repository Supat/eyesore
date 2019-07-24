using UnityEngine;
using System.Collections;

public class SupatController : MonoBehaviour {

	[HideInInspector]
	public bool facingRight = true;
	
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	float h = 0.0f;
	
	private Animator anim;
	private NetworkInput networkInput;
	
	void Awake () {
		anim = GetComponent<Animator>();
		networkInput = GameObject.Find("Supat Face").GetComponent<NetworkInput>();
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate () {
		if (networkInput.IsStreaming()) {
			h = networkInput.GetMovement(h);
		} else {
			h = Input.GetAxis("Horizontal");
		}
		
		anim.SetFloat("Speed", Mathf.Abs(h));
		
		if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed) {
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
		}
		
		if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
		}
		
		if (h > 0 && !facingRight) {
			Flip();
		} else if (h < 0 && facingRight) {
			Flip();
		}
	}
	
	void Flip () {
		facingRight = !facingRight;
		
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
