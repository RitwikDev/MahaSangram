using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    public Transform firePoint;

    private GameObject bulletInstance;
    private GameObject player;

    private Player playerScript;

    public float bulletSpeed = 6f;
    public bool isPressingFireButton { get; private set; }
    public bool canFire { get; private set; }
    public bool recoil { get; private set; }
    public bool fired { get; private set; }


    private const float INTER_FIRE_TIME = 0.1f;
    private float timer;
    private int bulletLimit = 5;
    public static int bulletCount;

    private PlayerInputActions playerInputActions;
    private InputAction inputActionMovement;

    private void Awake()
    {
        playerScript = FindObjectOfType<Player>();
        playerInputActions = new PlayerInputActions();
    }

    private void Start()
    {
        player = GameObject.Find(Properties.PLAYER_GAME_OBJECT_NAME);
        timer = 0;
        canFire = true;
    }

    private void OnEnable()
    {
        inputActionMovement = playerInputActions.Player.MovementDirections;
        inputActionMovement.Enable();
    }

    private void OnDisable()
    {
        inputActionMovement.Disable();
    }

    private void FixedUpdate()
    {
        timer = timer + Time.deltaTime;

        if(fired && timer >= INTER_FIRE_TIME)
        {
            recoil = false;
            fired = false;
        }

        canFire = (bulletCount < bulletLimit);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.started && timer >= INTER_FIRE_TIME)
        {
            if (bulletCount >= bulletLimit)
                return;

            bulletCount = bulletCount + 1;
            timer = 0;
            isPressingFireButton = true;
            recoil = true;
            fired = true;

            // Call a function to play recoil animation/change the current sprite

            Vector2 playerDirection = inputActionMovement.ReadValue<Vector2>();
            bulletInstance = Instantiate(playerScript.currentBullet, firePoint.transform.position, player.transform.rotation);
            Vector2 bulletDirection;

            // If the player is not pressing any button
            if (playerDirection == Vector2.zero)
            {
                // If the player is facing right
                if (player.transform.eulerAngles.y == 0)
                    bulletDirection = new Vector2(bulletSpeed, 0f);

                else
                    bulletDirection = new Vector2(-bulletSpeed, 0f);
            }

            else
            {
                // If the player is pressing just the down button
                if (playerDirection.x == 0 && playerDirection.y != 0)
                {
                    // If the player is pressing the up button
                    if (playerDirection.y == 1)
                        bulletDirection = new Vector2(0f, bulletSpeed);

                    // If the player is jumping and pressing the down button, then the bullet should travel vertically down
                    else if (System.Math.Abs(player.GetComponent<Rigidbody2D>().velocity.y) > 0.05)
                        bulletDirection = new Vector2(0f, -bulletSpeed);

                    else
                    {
                        // If the player is facing right
                        if (player.transform.eulerAngles.y == 0)
                            bulletDirection = new Vector2(bulletSpeed, 0f);

                        else
                            bulletDirection = new Vector2(-bulletSpeed, 0f);
                    }
                }

                else
                    bulletDirection = new Vector2(playerDirection.x * bulletSpeed, playerDirection.y * bulletSpeed);
            }

            bulletInstance.layer = Properties.PLAYER_BULLET;
            bulletInstance.GetComponent<SpriteRenderer>().sortingOrder = Properties.PLAYER_BULLET_SORTING_ORDER;
            bulletInstance.GetComponent<Rigidbody2D>().AddForce(bulletDirection, ForceMode2D.Impulse);
            bulletInstance.AddComponent<PlayerBulletDestroy>();
            //Destroy(bulletInstance, bulletTTl);
        }

        if (context.canceled)
            isPressingFireButton = false;
    }
}