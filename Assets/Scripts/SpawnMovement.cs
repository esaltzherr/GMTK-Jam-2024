using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMovement : MonoBehaviour
{
    public TowerHeightTracker heightTracker; // Reference to the TowerHeightTracker script
    public float minOffset = 1f; // Minimum offset distance from the tower's height
    public float maxOffset = 3f; // Maximum offset distance from the tower's height

    // Update is called once per frame
    void Update()
    {
        AdjustSpawnPosition();
    }

    void AdjustSpawnPosition()
    {
        if (heightTracker != null)
        {
            // Get the current height of the tower
            float currentHeight = heightTracker.height / 10;

            // Calculate the desired y position for the spawn area
            float desiredYMin = currentHeight + minOffset;
            float desiredYMax = currentHeight + maxOffset;

            //Debug.Log("desiredYMin: " + desiredYMin + ", desiredYMax: " + desiredYMax);
            // Update the spawn area's position to maintain the offset within the desired range
            Vector3 newPosition = transform.position;

            // Calculate the target Y position based on the minimum and maximum offset range
            float targetY = Mathf.Clamp(newPosition.y, desiredYMin, desiredYMax);

            // Smoothly move the spawn area to the target position
            newPosition.y = Mathf.Lerp(newPosition.y, targetY, Time.deltaTime * 5f); // Adjust 5f to control smoothness

            // Apply the new position
            transform.position = newPosition;
        }
    }
}
