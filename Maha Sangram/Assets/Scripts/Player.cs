using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private GameObject elevationReference;
    public GameObject bulletDefault;  // Default bullet sprite
    public GameObject currentBullet { get; private set; } // Current bullet sprite
    public Sprite spriteDefaultBullet;

    public float playerHeight { get; private set; }    // Player's height
    public float playerElevation { get; private set; }  // Player's elevation

    //public bool isGrounded { get; private set; }

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<Collider2D>().GetComponent<SpriteRenderer>();
        elevationReference = transform.Find(Properties.PLAYER_ELEVATION_REFERENCE_NAME).gameObject;
        bulletDefault.GetComponent<SpriteRenderer>().sprite = spriteDefaultBullet;
        currentBullet = bulletDefault;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Calculating the elevationReference's position based on the player's position. It is necessary when the player is ducking.
        playerHeight = spriteRenderer.bounds.size.y;
        // Changing elevationReference's and groundCheck's position based on the player's position. It is necessary when the player is ducking.
        elevationReference.transform.position = new Vector3(transform.position.x, spriteRenderer.bounds.center.y - playerHeight / 2, transform.position.z);
        // Calculating the player's elevation based on the elevationReference
        playerElevation = elevationReference.transform.position.y;
    }

    public GameObject GetPlayer()
    {
        return gameObject;
    }
}