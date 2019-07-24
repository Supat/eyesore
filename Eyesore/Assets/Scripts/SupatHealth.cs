using UnityEngine;
using System.Collections;

public class SupatHealth : MonoBehaviour {
	
	public float health = 100f;
	public float repeatDamagePeriod = 0.001f;
	public float hurtForce = 10f;
	public float damageAmount = 0f;
	
	private float lastHitTime;
	private Animator anim;

	void Awake () {
		anim = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Fireball") {
			if (Time.time > lastHitTime + repeatDamagePeriod) {
				if (health > 0f) {
					TakeDamage(col.transform);
					lastHitTime = Time.time;
					anim.SetTrigger("Hit");
					//Debug.Log("Hit");
				} else {
					anim.SetTrigger("Die");
					//Debug.Log("Died");
					GetComponent<SupatController>().enabled = false;
					StartCoroutine("ReloadGame");
				}
			}
		}
	}
	
	void TakeDamage (Transform fireball) {
		Vector3 hurtVector = transform.position - fireball.position + Vector3.right * 5f;
		GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);
		health -= damageAmount;
	}
	
	IEnumerator ReloadGame() {
		yield return new WaitForSeconds(10);
		Destroy (gameObject);
	}
}
