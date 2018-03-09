using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;

	// Update is called once per frame
	void Update ()
    {
        this.transform.position = Vector3.Lerp(transform.position, player.transform.position, 5f * Time.deltaTime);
        this.transform.position -= new Vector3(0f, 0f, 10f);
	}
}
