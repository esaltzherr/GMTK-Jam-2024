using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // The point the camera will follow (e.g., spawnArea)
    public Vector3 offset = new Vector3(0, 1, -10);  // Offset to adjust the camera's position relative to the target
    public float followSpeed = 2f;  // Speed at which the camera follows the target
    private bool isReturningToStart = false;  // Flag to indicate if the camera is moving to the start position
    private Vector3 startPosition;  // The original position of the camera

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (!isReturningToStart && target != null)
        {
            FollowTarget();
        }
    }

    void FollowTarget()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    public IEnumerator MoveCameraToStart(float duration)
    {
        isReturningToStart = true;
        float elapsedTime = 0;

        Vector3 initialPosition = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(initialPosition, startPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = startPosition;
        isReturningToStart = false;
        yield return true;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
