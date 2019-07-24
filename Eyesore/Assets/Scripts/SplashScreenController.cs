using UnityEngine;
using System.Collections;

public class SplashScreenController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine("ReloadGame");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	IEnumerator ReloadGame() {
		yield return new WaitForSeconds(4);
		Application.LoadLevel("SupatScene");
	}
}
