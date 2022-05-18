using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject bulletGameObject;
    public Transform firePoint;
    public int bulletSpeedX = 3;    // Bullet speed in the x - axis
    public int bulletSpeedXVar = 0; // Var means variable. It is set in the StandingShootingEnemy.cs file
    public int bulletSpeedYVar = 0; // Var means variable. It is set in the StandingShootingEnemy.cs file
    public int bulletTTL = 10;
    public int waitingTime = 5;  // Initial waiting time should be close to 0 because as soon as the enemy appears, it should shoot a bullet
    private GameObject bulletGameObjectInstance;
    private Vector2 bulletDirection;

    private void Start()
    {
        // InvokeRepeating(string methodName, float time, float repeatRate); invokes the method in time seconds, then repeatedly every repeatRate seconds.
        InvokeRepeating("EnemyShoot", 1, waitingTime);
    }

    void EnemyShoot()
    {
        // If the enemy is facing towards left, the rotation about y - axis is 0, else it is -180

        if (gameObject.name.Equals(Properties.STANDING_SHOOTING_ENEMY))
            bulletDirection = new Vector2(bulletSpeedXVar, bulletSpeedYVar);

        else
        {
            if (gameObject.transform.rotation.y == 0)
                bulletDirection = new Vector2(-bulletSpeedX, 0);

            else
                bulletDirection = new Vector2(bulletSpeedX, 0);
        }

        // Instantiating and providing force to the bullet
        bulletGameObjectInstance = Instantiate(bulletGameObject, firePoint.transform.position, gameObject.transform.rotation);
        bulletGameObjectInstance.GetComponent<Rigidbody2D>().AddForce(bulletDirection, ForceMode2D.Impulse);

        // Destroying the bullet 
        Destroy(bulletGameObjectInstance, bulletTTL);
    }
}