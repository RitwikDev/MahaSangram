using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingShootingEnemy : MonoBehaviour
{
    private GameObject playerGameObject;  // GameObject of player
    public Sprite leftStraight, leftDiagonalUp, leftDiagonalDown, up, down;   // Default sprites will look towards left
    private Dictionary<float, Sprite> enemySprites; // enemySprites contains the sprites to be loaded at the given z - angles
    private Vector3 positionDifference;
    private float angle;    // To store the angle between the player and the enemy
    private EnemyFire enemyFire;
    private int bulletSpeedX;

    private void Awake()
    {
        playerGameObject = GameObject.Find(Properties.PLAYER_GAME_OBJECT_NAME);

        enemySprites = new Dictionary<float, Sprite>()
        {
            {-90, up},
            {-45, leftDiagonalUp},
            {0, leftStraight},
            {45, leftDiagonalDown},
            {90, down}
        };

        enemyFire = FindObjectOfType<EnemyFire>();
        bulletSpeedX = enemyFire.bulletSpeedX;
    }

    // Update is called once per frame
    void Update()
    {
        // If the enemy is facing towards left, the rotation about y - axis is 0, else it is -180
        // This enemy is facing left by default
        // The angle between the player and the enemy is 0 if the player is to the left of the enemy
        // This angle increases as the player goes in a clockwise direction about the enemy
        // Consider a circle, with the enemy at the center, divided into 16 equal parts
        // Each interval is of 22.5 degrees
        // If the player is between -22.5 and 22.5 degrees of the center (enemy), the enemy should shoot straight left
        // If the player is between 22.5 and 67.5 degrees, the enemy should shoot left diagonal down
        // Start from -22.5 degrees with increments of 45 degrees to cover all 8 directions the enemy can shoot

        positionDifference = gameObject.transform.position - playerGameObject.transform.position;   // Calculating the difference between the position of player and the enemy
        angle = Mathf.Atan2(positionDifference.y, positionDifference.x) * Mathf.Rad2Deg;    // Calculating the angle between the player and the enemy

        if (angle > -22.5 && angle < 22.5)
        {
            // Left Straight
            enemyFire.bulletSpeedXVar = -bulletSpeedX;
            enemyFire.bulletSpeedYVar = 0;
        }

        else if (angle > 22.5 && angle < 67.5)
        {
            // Left Diagonal Down
            enemyFire.bulletSpeedXVar = -bulletSpeedX;
            enemyFire.bulletSpeedYVar = -bulletSpeedX;
        }

        else if (angle > 67.5 && angle < 112.5)
        {
            // Down Straight
            enemyFire.bulletSpeedXVar = 0;
            enemyFire.bulletSpeedYVar = -bulletSpeedX;
        }

        else if (angle > 112.5 && angle < 157.5)
        {
            // Right Diagonal Down
            enemyFire.bulletSpeedXVar = bulletSpeedX;
            enemyFire.bulletSpeedYVar = -bulletSpeedX;
        }

        else if (angle > 157.5 && angle <= 180 || angle >= -180 && angle < -157.5)
        {
            // Right Straight
            enemyFire.bulletSpeedXVar = bulletSpeedX;
            enemyFire.bulletSpeedYVar = 0;
        }

        else if (angle > -157.5 && angle < -112.5)
        {
            // Right Diagonal Up
            enemyFire.bulletSpeedXVar = bulletSpeedX;
            enemyFire.bulletSpeedYVar = bulletSpeedX;
        }

        else if (angle > -112.5 && angle < -67.5)
        {
            // Up Straight
            enemyFire.bulletSpeedXVar = 0;
            enemyFire.bulletSpeedYVar = bulletSpeedX;
        }

        else
        {
            // Left Diagonal Up
            enemyFire.bulletSpeedXVar = -bulletSpeedX;
            enemyFire.bulletSpeedYVar = bulletSpeedX;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
            Destroy(this.gameObject);
    }
}
