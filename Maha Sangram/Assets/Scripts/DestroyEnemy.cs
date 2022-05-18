using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Each enemy has a child object called Hit Box. If the hit box is hit by a bullet,
        // destroy the parent of the hit box object.
        // Layer 10 = Player's bullet
        if (collision.gameObject.layer == 10)
            Destroy(this.transform.parent.gameObject);
    }
}
