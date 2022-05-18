using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemyGameObjects;        // GameObject of running enemy
    private GameObject enemyGameObjectInstance;  // GameObject instance of running enemy
    private Transform cameraTransform;           // Trabsform of camera
    public float timer = 0f;                     // Using a timer to respawn enemies at random times
    public float waitingTime = 5f;               // At the end of waitingTime respawn enemy
    private bool activateTimer = true;                  
    public float cameraEnemyOffset = 10f;        // Offset between camera and enemy spawn point
    private Vector2 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform; // Getting the transform of camera
    }

    // Update is called once per frame
    void Update()
    {
        if(activateTimer)
            timer = timer + Time.deltaTime;

        if(timer > waitingTime)
        {
            activateTimer = false;
            // Spawn point is to the right of the camera
            spawnPoint = new Vector2(cameraTransform.position.x + cameraEnemyOffset, transform.position.y);

            // Instantiating enemies
            foreach(GameObject enemyGameObject in enemyGameObjects)
                enemyGameObjectInstance = Instantiate(enemyGameObject, spawnPoint, Quaternion.identity);

            timer = 0f;
            activateTimer = true;
            waitingTime = Random.Range(3f, 10f);
        }
    }
}
