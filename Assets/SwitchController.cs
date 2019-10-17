using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{

    public GameObject door;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(door.GetComponent<MoveDoor>().isOpen == false)
        {
            door.GetComponent<MoveDoor>().OpenDoor(true);
        }
        
    }
}
