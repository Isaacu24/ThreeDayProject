using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour 
{

    public PlayerController playerController;

    void Start()
    {
        playerController = transform.root.GetComponent<PlayerController>();
       
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (true == other.CompareTag("Ground"))
        {
            if (0 >= playerController.curRigidbody.velocity.y)
            {
                playerController.isGrounded = true;
                playerController.currentJumpCount = 0;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        playerController.isGrounded = false;
    }
}
