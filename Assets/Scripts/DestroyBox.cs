using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBox : MonoBehaviour
{
    public PointsManager pointsManager;  // Reference to the PointsManager script

    // This method is called when another collider stops touching the trigger collider attached to this object
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the exiting object has the tag "Block"
        if (other.gameObject.CompareTag("Block"))
        {
            // Get the FallingBlock script component from the exiting object
            FallingBlock fallingBlock = other.GetComponent<FallingBlock>();

            // If the FallingBlock script is found, subtract the points
            if (fallingBlock != null && pointsManager != null)
            {
                pointsManager.SubtractPoints(fallingBlock.points);
            }

            // Destroy the exiting object
            Destroy(other.gameObject);
        }
    }
}
