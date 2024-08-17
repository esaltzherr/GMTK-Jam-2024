using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public GameObject spawnPoint;  // The spawn point where the new object will be instantiated
    public GameObject objectToSpawn;  // The object that will be instantiated
    public float moveSpeed = 5f;  // Speed at which the player moves left and right
    public TowerHeightTracker heightTracker;  // Reference to the TowerHeightTracker script
    public float maxScaleMultiplier = 5f;  // Maximum scale multiplier for the blocks
    public float heightScaleFactor = 0.05f;  // How much the scale increases per unit of height

    // Update is called once per frame
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

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Instantiate a new object at the spawn point when Space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBlock();
        }
    }

    void SpawnBlock()
    {
        if (objectToSpawn != null && spawnPoint != null && heightTracker != null)
        {
            // Calculate the scale factor based on the current height
            float height = heightTracker.height / 10f;  // Assuming heightTracker.height is scaled by 10
            float scaleMultiplier = 1f + (height * heightScaleFactor);

            // Clamp the scale to avoid too large blocks
            scaleMultiplier = Mathf.Clamp(scaleMultiplier, 1f, maxScaleMultiplier);
            Debug.Log("Multi:" + scaleMultiplier);
            // Instantiate the block at the spawn point
            GameObject newBlock = Instantiate(objectToSpawn, spawnPoint.transform.position, spawnPoint.transform.rotation);

            // Apply the scale to the new block
            newBlock.transform.localScale *= scaleMultiplier;
        }
    }
}
