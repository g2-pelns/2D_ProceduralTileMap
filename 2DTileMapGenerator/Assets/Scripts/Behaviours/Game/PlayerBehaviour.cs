using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W) && transform.position.y < (World.height - 0.5f))
        { 
            transform.position += new Vector3(0f, 1f);
        }

        if (Input.GetKeyDown(KeyCode.A) && transform.position.x > 0.5f)
        {
            transform.position -= new Vector3(1f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.S) && transform.position.y > 0.5f)
        {
            transform.position -= new Vector3(0f, 1f);
        }

        if (Input.GetKeyDown(KeyCode.D) && transform.position.x < (World.width - 0.5f))
        {
            transform.position += new Vector3(1f, 0f);
        }
    }
}
