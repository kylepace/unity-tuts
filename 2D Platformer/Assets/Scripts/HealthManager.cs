using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

	public GameObject[] hearts;
	private int health;

	// Use this for initialization
	void Start () {
		health = hearts.Length;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Hazard") {
			health--;
			hearts[health].SetActive (false);
		}

		if (health == 0) {
			Application.LoadLevel (Application.loadedLevelName);
		}
	}
}
