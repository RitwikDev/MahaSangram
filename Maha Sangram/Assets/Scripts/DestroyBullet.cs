using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Each enemy has a child object called Hit Box. if the bullet hits an enemy, it will
        // also hit its child object. Upon hitting the hit box the bullet gameobject should be destroyed.
        if (collision.name.Equals(Properties.ENEMY_HIT_BOX))
            Destroy(this.gameObject);
    }
}
