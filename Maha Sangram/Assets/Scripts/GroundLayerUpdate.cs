using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundLayerUpdate : MonoBehaviour
{
    private Collider2D collider2DGround;
    private Player playerScript;    // Reference to the player script
    private float groundElevationUpper;
    private float groundElevationLower;

    public bool overrideUpdate { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<Player>();      // Creating the object of the player script
        collider2DGround = gameObject.GetComponent<Collider2D>();
        groundElevationUpper = collider2DGround.transform.position.y + collider2DGround.bounds.size.y / 2;
        groundElevationLower = collider2DGround.transform.position.y - collider2DGround.bounds.size.y / 2;

        overrideUpdate = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is below the ground elevation, then overrideUpdate is no longer valid.
        if (overrideUpdate)
            overrideUpdate = !(playerScript.playerElevation < groundElevationLower);

        if(!overrideUpdate && gameObject.tag == Properties.GROUND_TAG_CAN_FALL_NAME)
        {
            if (playerScript.playerElevation >= groundElevationUpper)
                gameObject.layer = Properties.GROUND_LAYER_CAN_INTERACT_NUMBER;

            else if(playerScript.playerElevation < groundElevationLower)
                gameObject.layer = Properties.GROUND_LAYER_CANNOT_INTERACT_NUMBER;
        }
    }

    public void UpdateGroundLayer()
    {
        if(gameObject.tag == Properties.GROUND_TAG_CAN_FALL_NAME)
        {
            overrideUpdate = true;
            gameObject.layer = Properties.GROUND_LAYER_CANNOT_INTERACT_NUMBER;
        }
    }

    public float GetGroundElevation()
    {
        return groundElevationUpper;
    }
}