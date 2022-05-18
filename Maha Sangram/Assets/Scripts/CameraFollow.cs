using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject playerGameObject; // GameObject of player
    private Transform playerTransform;  // Transform of player
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = playerGameObject.transform;
    }

    
    void LateUpdate()
    {
        // Update position of camera if the player's x position is greater than camera's x position.
        // Otherwise, do not update the position of camera
        if (playerTransform.position.x >= transform.position.x)
            UpdateCameraPosition();
    }

    // This function updates the position of the camera
    void UpdateCameraPosition()
    {
        Vector3 cameraCurrentPosition = transform.position;    // Stores the current position of the camera
        cameraCurrentPosition.x = playerTransform.position.x;  // Apply the current x position of the player to camera
        transform.position = cameraCurrentPosition;            // Update the position of camera
    }
}
