using UnityEngine;
using System.Collections;

public class SpawnerController : MonoBehaviour {

	public float spawnTime = 3f;
	public float spawnDelay = 5f;

	void Awake () {
		//spawnTime = Random.Range(0.3f, 0.9f);
		//spawnDelay = Random.Range(0.3f, 0.9f);
	}
	
	// Use this for initialization
	void Start () {
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}

	// Update is called once per frame
	void Update () {

	}

	void Spawn () {
		Instantiate(Resources.Load("Fireball"), transform.position, transform.rotation);
	}
}
