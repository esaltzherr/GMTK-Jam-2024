using System.Collections;
using System.Collections.Generic;
using TMPro; // Add this line to include TextMeshPro
using UnityEngine;

public class TowerHeightTracker : MonoBehaviour
{
    public GameObject line; // The line object that will indicate the tower's overall height
    public int height;
    public int lastHeight;
    public float offset = 1f; // Distance above the highest block where the line will be placed
    public float smallOffset = 0.1f;
    public TextMeshProUGUI heightText; // Reference to the TextMeshProUGUI component

    private List<GameObject> towerBlocks = new List<GameObject>(); // List of all blocks in the tower
    public float smoothSpeed = 5f; // Speed of the smooth movement

    public PointsManager pointsManager;
    // Call this method to add a new block to the list (for example, when a block is placed)
    public void AddBlock(GameObject block)
    {
        towerBlocks.Add(block);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLinePosition();
        UpdateText();
    }

    // This method updates the line's position based on the highest block
    void UpdateLinePosition()
    {
        // Remove any null references (destroyed blocks) from the list
        towerBlocks.RemoveAll(block => block == null);

        if (towerBlocks.Count > 0)
        {
            // Find the highest point on the blocks
            float highestY = float.MinValue;
            foreach (GameObject block in towerBlocks)
            {
                if (block != null)
                {
                    // Get the Collider2D component attached to the block
                    Collider2D collider = block.GetComponent<Collider2D>();
                    if (collider != null)
                    {
                        // Calculate the highest point of the block using the collider's bounds
                        float blockTopY = collider.bounds.max.y;
                        if (blockTopY > highestY)
                        {
                            highestY = blockTopY;
                        }
                    }
                    else
                    {
                        // Fallback to the block's center if there's no collider (not recommended, but safe)
                        float blockHeight = block.transform.position.y;
                        if (blockHeight > highestY)
                        {
                            highestY = blockHeight;
                        }
                    }
                }
            }

            // Ensure the height does not go below zero
            if (highestY <= 0)
            {
                highestY = 0;
            }

            // Calculate the target Y position for the line
            float targetY = highestY + offset;

            // Smoothly move the line to the target position
            Vector3 newLinePosition = line.transform.position;
            newLinePosition.y = Mathf.Lerp(
                newLinePosition.y,
                targetY,
                Time.deltaTime * smoothSpeed
            );
            line.transform.position = newLinePosition;

            // Update the height value based on the highest point's position
            height = (int)(highestY * 10);
            int heightDiff = lastHeight - height;
            pointsManager.HeightPoints(heightDiff);
            lastHeight = height;

        }
    }

    // This method updates the TextMeshProUGUI component with the current height
    void UpdateText()
    {
        if (heightText != null)
        {
            heightText.text = "" + height.ToString() + "m";
        }
    }

    public void FreezeBlocks()
    {
        foreach (GameObject block in towerBlocks)
        {
            if (block != null)
            {
                Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeAll; // Freeze position and rotation
                }
            }
        }
    }


    public void ReduceLine(){
        offset = smallOffset;
    }
}
