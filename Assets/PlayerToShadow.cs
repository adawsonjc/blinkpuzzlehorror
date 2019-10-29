using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToShadow : MonoBehaviour
{
    public GameObject shadowPlane; 

    // Update is called once per frame
    void FixedUpdate()
    {
        Camera cam = Camera.main;

        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);

        shadowPlane.GetComponent<Renderer>().material.SetVector("_Player_pos", screenPos);
    }
}
