using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    Rigidbody2D rigidbody2D;
    Camera viewCamera;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    float verticalMove = 0f;

    bool jump = false;
    bool up = false;


    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        viewCamera = Camera.main;
    }


    // Update is called once per frame
    void Update () {
		horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");


        //Get the mouse position, and have the Player look towards it:
        //The below is a translation of Vector3.Angle
        Vector3 diff = viewCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //Turn the Vector into a Unit vector using normalize
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
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
