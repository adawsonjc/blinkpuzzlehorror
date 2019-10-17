using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoor : MonoBehaviour
{

    public Transform pos1;
    public float speed;
    public bool isOpen = false;

    public void OpenDoor(bool isOpen)
    {
        this.isOpen = isOpen;
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
       if (isOpen)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos1.position, speed * Time.deltaTime);
        }

       if (transform.position.x > pos1.position.x)
        {
            isOpen = false;
        }
    }
}
