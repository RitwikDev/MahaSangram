using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Properties.PLAYER_BOUNDARY)
            return;

        if(collision.gameObject.layer == Properties.GROUND_LAYER_CAN_INTERACT_NUMBER)
        {
            // If the player is above the ground
            isGrounded = (collision.transform.position.y < transform.position.y);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Properties.PLAYER_BOUNDARY)
            return;

        isGrounded = false;
    }
}
