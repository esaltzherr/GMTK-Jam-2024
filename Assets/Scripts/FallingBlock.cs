using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    public int points = 10;
    private bool hasCollided = false;  // To track if the block has collided for the first time
    private TowerHeightTracker towerHeightTracker;  // Reference to the TowerHeightTracker

    // Start is called before the first frame update
    void Start()
    {
        // Find the TowerHeightTracker in the scene
        towerHeightTracker = FindObjectOfType<TowerHeightTracker>();
    }

    // This method is called when the block collides with another object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided)
        {
            hasCollided = true;

            // Call the TowerHeightTracker to add this block to the list
            if (towerHeightTracker != null)
            {
                towerHeightTracker.AddBlock(gameObject);
            }
        }
    }
}
