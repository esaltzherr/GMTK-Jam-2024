using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public GameObject spawnPoint; // The spawn point where the new object will be instantiated
    public GameObject[] blockPrefabs; // Array of block prefabs
    public float moveSpeed = 5f; // Speed at which the player moves left and right
    public TowerHeightTracker heightTracker; // Reference to the TowerHeightTracker script
    public float maxScaleMultiplier = 5f; // Maximum scale multiplier for the blocks
    public float heightScaleFactor = 0.05f; // How much the scale increases per unit of height
    public float cooldown = 1f; // Cooldown time between block spawns

    public float minOffset = 1f; // Minimum offset distance from the tower's height
    public float maxOffset = 3f; // Maximum offset distance from the tower's height

    private float currCooldown = 0f; // Current cooldown time
    private GameObject currentBlock; // Reference to the currently spawned block
    private bool isBlockReadyToDrop = false; // Whether the block is ready to be dropped

    void Update()
    {
        // Handle instant left and right movement
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector3.right;
        }

        // Move the spawn area horizontally
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Move the current block horizontally with the spawn area
        if (currentBlock != null)
        {
            currentBlock.transform.position = spawnPoint.transform.position;
        }

        // Handle vertical movement
        AdjustSpawnPosition();

        // Handle block spawning and dropping
        HandleBlockSpawning();
        HandleBlockDropping();
    }

    void AdjustSpawnPosition()
    {
        if (heightTracker != null)
        {
            // Get the current height of the tower
            float currentHeight = heightTracker.height / 10f;

            // Calculate the desired y position for the spawn area
            float desiredYMin = currentHeight + minOffset;
            float desiredYMax = currentHeight + maxOffset;

            // Update the spawn area's position to maintain the offset within the desired range
            Vector3 newPosition = transform.position;

            // Calculate the target Y position based on the minimum and maximum offset range
            float targetY = Mathf.Clamp(newPosition.y, desiredYMin, desiredYMax);

            // Smoothly move the spawn area to the target position
            float smoothSpeed = 5f; // Adjust this to control smoothness
            newPosition.y = Mathf.Lerp(newPosition.y, targetY, Time.deltaTime * smoothSpeed);

            // Apply the new position to the spawn area
            transform.position = newPosition;

        }
    }

    void HandleBlockSpawning()
    {
        // Decrease the cooldown timer
        currCooldown -= Time.deltaTime;

        // Check if cooldown has expired and there is no block ready to drop
        if (currCooldown <= 0f && !isBlockReadyToDrop)
        {
            // Spawn a new block
            SpawnBlock();
            isBlockReadyToDrop = true; // Mark that the block is ready to be dropped
        }
    }

    void SpawnBlock()
    {
        if (blockPrefabs.Length > 0 && spawnPoint != null && heightTracker != null)
        {
            // Calculate the scale factor based on the current height
            float height = heightTracker.height / 10f; // Assuming heightTracker.height is scaled by 10
            float scaleMultiplier = 1f + (height * heightScaleFactor);

            // Clamp the scale to avoid too large blocks
            scaleMultiplier = Mathf.Clamp(scaleMultiplier, 1f, maxScaleMultiplier);

            // Choose a random block from the array
            int randomIndex = Random.Range(0, blockPrefabs.Length);
            GameObject blockPrefab = blockPrefabs[randomIndex];

            // Instantiate the block at the spawn point
            currentBlock = Instantiate(
                blockPrefab,
                spawnPoint.transform.position,
                spawnPoint.transform.rotation
            );
            Debug.Log("fdhsjkfhdkjsl" + spawnPoint.transform.position);
            // Apply the scale to the new block
            currentBlock.transform.localScale *= scaleMultiplier;

            // Disable the Rigidbody2D to allow the block to move with the spawn area
            Rigidbody2D rb = currentBlock.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            Debug.Log("Block spawned with scale multiplier: " + scaleMultiplier);
        }
    }

    void HandleBlockDropping()
    {
        // Check if the space key is pressed and there is a block ready to be dropped
        if (Input.GetKeyDown(KeyCode.Space) && currentBlock != null)
        {
            // Enable the Rigidbody2D to drop the block
            DropBlock();

            // Reset the cooldown and state
            currentBlock = null;
            isBlockReadyToDrop = false;
            currCooldown = cooldown;
        }
    }

    void DropBlock()
    {
        if (currentBlock != null)
        {
            // Enable the Rigidbody2D to drop the block
            Rigidbody2D rb = currentBlock.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            Debug.Log("Block dropped.");
        }
    }

    public void DeleteCurrentBlock()
    {
        Destroy(currentBlock);
        currentBlock = null;
    }
}
