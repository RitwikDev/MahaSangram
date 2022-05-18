using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;      // For the new InputSystem

public class PlayerMovement : MonoBehaviour
{
    private const float JUMP_FACTOR = 12f;       // Multiplication factor for jump action
    private const float MOVEMENT_SPEED = 3.5f;    // Multiplication factor for movement
    private const float DECREASING_FACTOR = 1.3f;   // Multiplication factor for decreasing the momentum while in air

    private PlayerInputActions playerInputActions;  // Reference to the Input System
    private InputAction inputActionMovement;
    private Rigidbody2D rigidbody2DPlayer;  // Player's RigidBody
    private Collider2D collider2DCurrent;   // To store the gameObject the player is currently standing on
    private GroundCheck groundCheck;

    public bool isDucking { get; private set; }     // To keep track if the player is ducking or not
    public bool isRunningStraight { get; private set; }
    public bool isFalling { get; private set; }
    public bool isJumping { get; private set; }
    public bool noMovement { get; private set; }
    public Vector2 movementDirection { get; private set; }
    
    private void Awake()
    {
        groundCheck = groundCheck = transform.Find(Properties.PLAYER_GROUND_CHECK_NAME).gameObject.GetComponent<GroundCheck>();

        rigidbody2DPlayer = GetComponent<Rigidbody2D>();
        playerInputActions = new PlayerInputActions();
    }

    private void Update()
    {
        if (groundCheck.isGrounded)
            isJumping = false;

        PlayerMove();
        UpdateGravity();
    }

    // This function is checks if the player is touching the ground or not
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Properties.GROUND_LAYER_CANNOT_INTERACT_NUMBER)
            return;

        if(collision.gameObject.tag == Properties.GROUND_TAG_CANNOT_FALL_NAME || collision.gameObject.tag == Properties.GROUND_TAG_CAN_FALL_NAME)
        {
            collider2DCurrent = collision;
            isFalling = false;
        }
    }

    // This function is checks if the player is touching the ground or not
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Properties.GROUND_LAYER_CANNOT_INTERACT_NUMBER)
            return;

        if (collision.gameObject.tag == Properties.GROUND_TAG_CANNOT_FALL_NAME || collision.gameObject.tag == Properties.GROUND_TAG_CAN_FALL_NAME)
        {
            collider2DCurrent = collision;
            isFalling = false;
        }
    }

    // This function is called when the player's collider exits something
    private void OnTriggerExit2D(Collider2D collision)
    {
        collider2DCurrent = null;
    }

    private void OnEnable()
    {
        //inputActionMovement = playerInputActions.Player.Movement;
        inputActionMovement = playerInputActions.Player.MovementDirections;
        inputActionMovement.Enable();
    }

    private void OnDisable()
    {
        inputActionMovement.Disable();
    }

    // This function updates the value of gravity based on the elevation of the player
    private void UpdateGravity()
    {
        // Increasing the gravity when the player starts to descend
        if (rigidbody2DPlayer.velocity.y < 0.1)
            rigidbody2DPlayer.gravityScale = 3f;
        else
            rigidbody2DPlayer.gravityScale = 2f;    // Else, restoring the gravity to its default value of 1
    }

    // This function moves the player. It is also responsible for providing momentum to the player.
    private void PlayerMove()
    {
        movementDirection = inputActionMovement.ReadValue<Vector2>();
        float moveX = movementDirection.x;
        float moveY = movementDirection.y;

        // Player if touching the ground
        if(groundCheck.isGrounded)
        {
            isRunningStraight = (moveX != 0 && moveY == 0);
            isDucking = (moveX == 0 && moveY < 0);
            noMovement = (moveX == 0 && moveY == 0);

            // Player is not moving in the x-direction
            if(moveX == 0)
                rigidbody2DPlayer.velocity = new Vector2(MOVEMENT_SPEED * moveX, rigidbody2DPlayer.velocity.y); // For jumping

            else
                rigidbody2DPlayer.velocity = new Vector2(MOVEMENT_SPEED * moveX, rigidbody2DPlayer.velocity.y);
        }

        else
        {
            if (moveX == 0)
            {
                // The new velocity of the player. Speed is the same as before in y-direction
                // The below if-else block is used to provide momentum to the player while in air

                if (rigidbody2DPlayer.velocity.x > 0)
                    rigidbody2DPlayer.velocity = new Vector2(rigidbody2DPlayer.velocity.x - Time.deltaTime * MOVEMENT_SPEED * DECREASING_FACTOR, rigidbody2DPlayer.velocity.y);

                else if (rigidbody2DPlayer.velocity.x < 0)
                    rigidbody2DPlayer.velocity = new Vector2(rigidbody2DPlayer.velocity.x + Time.deltaTime * MOVEMENT_SPEED * DECREASING_FACTOR, rigidbody2DPlayer.velocity.y);
            }

            else
                rigidbody2DPlayer.velocity = new Vector2(MOVEMENT_SPEED * moveX, rigidbody2DPlayer.velocity.y);
        }
    }

    // This function is used for making the player jump
    public void PlayerJump(InputAction.CallbackContext context)
    {
        // If the jump button is pressed and the player is on the ground, then jump
        if(context.performed && groundCheck.isGrounded)
        {
            // If the player is ducking
            if (isDucking)
            {
                // If the current gameObject on which the player is standing is not null (the player is not in the air)
                if (collider2DCurrent.gameObject != null && collider2DCurrent.gameObject.tag == Properties.GROUND_TAG_CAN_FALL_NAME)
                {
                    collider2DCurrent.gameObject.GetComponent<GroundLayerUpdate>().UpdateGroundLayer();
                    isFalling = true;
                    groundCheck.isGrounded = false;
                }
            }

            else if(Mathf.Abs(rigidbody2DPlayer.velocity.y) < 0.01)
            {
                rigidbody2DPlayer.AddForce(Vector2.up * JUMP_FACTOR, ForceMode2D.Impulse);   // Adding upward force to jump
                isJumping = true;
                groundCheck.isGrounded = false;
            }
        }
    }
}