using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    float verticalMove = 0f;

    bool jump = false;
    bool up = false;


    // Update is called once per frame
    void Update () {
		horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("JUMP");
            jump = true;
        }

    
    }

    private void FixedUpdate() {

        controller.Move(
            horizontalMove * this.runSpeed * Time.fixedDeltaTime,
            verticalMove * this.runSpeed * Time.fixedDeltaTime,
            false,
            jump
        );

        jump = false;
        up = false;
    }
}
