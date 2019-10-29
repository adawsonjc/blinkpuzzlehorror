using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow2Camera : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Camera cam = Camera.main;

        float pos = (cam.nearClipPlane);

        transform.position = cam.transform.position + cam.transform.forward * pos;

        float h = Mathf.Tan(cam.fieldOfView * Mathf.Deg2Rad) * pos * 3f;

        transform.localScale = new Vector3(h * cam.aspect, h, -10);
    }
}
