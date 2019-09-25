using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform playerTransform;
    public float zindex;
  
	void Start () {

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void LateUpdate () {

        // here we store current camera position as a temporary position
        Vector3 temp = transform.position;

        // set cameras position to be equal to player
        temp = playerTransform.position;
        temp.z = zindex;

        // set cameras temp position to current camera position
        transform.position = temp;
	}
}
