using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundaryMove : MonoBehaviour
{
    public GameObject cameraGameObject; // GameObject of player
    private Transform cameraTransform;  // Transform of player
    private float cameraBoundaryOffset = 9f;   // Offset between camera and boundary transform
    
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = cameraGameObject.transform;
    }

    void LateUpdate()
    {
        // Update position of boundary if the player's x position is greater than boundary's x position.
        // Otherwise, do not update the position of boundary
        if (cameraTransform.position.x >= transform.position.x)
            UpdateBoundaryPosition();
    }

    // This function updates the position of the camera
    void UpdateBoundaryPosition()
    {
        Vector3 boundaryCurrentPosition = transform.position;    // Stores the current position of the camera
        boundaryCurrentPosition.x = cameraTransform.position.x - cameraBoundaryOffset;  // Apply the current x position of the player to camera
        transform.position = boundaryCurrentPosition;            // Update the position of camera
    }
}
