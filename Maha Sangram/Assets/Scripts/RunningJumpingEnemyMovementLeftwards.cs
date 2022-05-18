using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningJumpingEnemyMovementLeftwards : MonoBehaviour
{
    public Rigidbody2D runningJumpingEnemyRigidbody2D; // RigidBody2D of running enemy
    public float runningEnemySpeed = 5f;        // Speed of this enemy
    public float jumpFactor = 7f;               // Enemy jump factor

    // Update is called once per frame
    void Update()
    {
        runningJumpingEnemyRigidbody2D.velocity = new Vector2(-runningEnemySpeed, runningJumpingEnemyRigidbody2D.velocity.y); // '-' to move towards left
    }

    // This function is triggered when the collider of the enemy is triggered
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isGrounded = false;

        if (this.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0.001)
            isGrounded = true;

        // If the collider collides with any Enemy Platform, then the enemy should jump
        if (collision.gameObject.layer == Properties.ENEMY_PLATFORM && isGrounded)
            runningJumpingEnemyRigidbody2D.AddForce(Vector2.up * jumpFactor, ForceMode2D.Impulse);

        // If enemy collides with exit boundary, destroy that enemy's GameObject instance
        if (collision.gameObject.layer == 11)
            Destroy(this.gameObject);
    }
}
