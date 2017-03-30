using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class HealthManager : MonoBehaviour {

	public GameObject[] hearts;
	public Rigidbody2D playerBody;
    public Platformer2DUserControl playerControl;

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
			hearts[health].SetActive(false);
            knockBack(coll.transform.position);
		}

		if (health <= 0) {
			Application.LoadLevel (Application.loadedLevelName);
		}
	}

	void knockBack(Vector3 hazardPosition)
	{
	    StartCoroutine("haltMovement");
	    var heading = transform.position - hazardPosition;
	    var distance = heading.magnitude;
	    var direction = heading / distance;
        var directionForVelocity = new Vector2(direction.x, direction.y);
	    playerBody.velocity = directionForVelocity * 20f;
	}

    IEnumerator haltMovement()
    {
        playerControl.movementEnabled = false;
        yield return new WaitForSeconds(1);
        playerControl.movementEnabled = true;
    }
}
