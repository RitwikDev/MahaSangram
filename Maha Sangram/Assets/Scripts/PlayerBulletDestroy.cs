using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletDestroy : MonoBehaviour
{
    public float bulletTTl = 1.5f;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        if(timer >= bulletTTl)
        {
            PlayerFire.bulletCount = PlayerFire.bulletCount - 1;
            Destroy(gameObject);
        }
    }
}
