using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningEnemyMovementLeftwards : MonoBehaviour
{
    public Rigidbody2D runningEnemyRigidbody2D; // RigidBody2D of running enemy
    public float runningEnemySpeed = 2f;        // Speed of this enemy
    public float jumpFactor = 7f;               // Enemy jump factor

    // Update is called once per frame
    void Update()
    {
        runningEnemyRigidbody2D.velocity = new Vector2(-runningEnemySpeed, runningEnemyRigidbody2D.velocity.y); // '-' to move towards left
    }

    // This function is triggered when the collider of the enemy is triggered
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If enemy is not moving, then the enemy should jump
        //if (gameObject.GetComponent<Rigidbody2D>().velocity.x < 2f)
        //    runningEnemyRigidbody2D.AddForce(Vector2.up * jumpFactor, ForceMode2D.Impulse);

        // If enemy collides with exit boundary, destroy that enemy's GameObject instance
        if (collision.gameObject.layer == 11)
            Destroy(this.gameObject);
    }
}
