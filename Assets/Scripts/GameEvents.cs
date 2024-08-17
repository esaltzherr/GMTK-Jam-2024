using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    public TowerHeightTracker towerHeightTracker;
    public GameObject spawnArea; // Reference to the spawn area object
    public GameObject playerPrefab; // Reference to the player prefab
    public Transform playerSpawnPoint; // Reference to the player spawn point
    public CameraFollow cameraFollow; // Reference to the CameraFollow script on the camera
    public CountdownTimer countdownTimer; // Reference to the CountdownTimer script

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnTimerEnd()
    {
        Debug.Log("DOING NEXT THING");

        if (towerHeightTracker != null)
        {
            towerHeightTracker.FreezeBlocks();
            towerHeightTracker.ReduceLine();
        }

        if (spawnArea != null)
        {
            // Get the SpawnArea script component from the spawnArea GameObject
            SpawnArea spawnAreaScript = spawnArea.GetComponent<SpawnArea>();

            // If the SpawnArea script is found, delete the current block
            if (spawnAreaScript != null)
            {
                spawnAreaScript.DeleteCurrentBlock();
            }

            // Disable the spawn area object
            spawnArea.SetActive(false);
        }

        if (cameraFollow != null)
        {
            StartCoroutine(HandleEndOfTimerSequence());
        }
    }

    private IEnumerator HandleEndOfTimerSequence()
    {
        // Move the camera down to its starting position
        yield return StartCoroutine(cameraFollow.MoveCameraToStart(3f)); // Adjust the duration as needed

        // Spawn the player at the designated spawn point
        if (playerPrefab != null && playerSpawnPoint != null)
        {
            GameObject player = Instantiate(
                playerPrefab,
                playerSpawnPoint.position,
                playerSpawnPoint.rotation
            );
            cameraFollow.SetTarget(player.transform); // Set the player as the new camera target
        }

        // Refreeze just in case you dropped a block RIGHT before the timer ended so it wasn't added to the array yet
        if (towerHeightTracker != null)
        {
            towerHeightTracker.FreezeBlocks();
        }
        
        // TODO
        // enable the heightlines collider



        // Start the Climb Timer
        if (countdownTimer != null)
        {
            StartCoroutine(countdownTimer.ClimbCountdown());
        }

        // if collides with the height side a function here will be called to cancel the climb ountdown
        // count the points

        

        Debug.Log("End of timer sequence complete");
    }
}
