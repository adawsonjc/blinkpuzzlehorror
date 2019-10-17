using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsScript : MonoBehaviour
{
    private CharacterController2D controller;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.isMoving)
        {
            //Debug.Log(controller.isMoving);
            audio.Play();
        }
    }
}
