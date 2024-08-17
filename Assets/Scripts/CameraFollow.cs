using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // The point the camera will follow (e.g., spawnArea)
    public Vector3 offset = new Vector3(0, 0f, -10);  // Offset to adjust the camera's position relative to the target
    public float followSpeed = 2f;  // Speed at which the camera follows the target
    public float scaleSpeed = 0.1f;  // Speed at which the camera scales its view
    public float maxZoomOut = 30f;  // Maximum FOV or orthographic size

    private Camera cameraComponent;
    private float initialSize;

    void Start()
    {
        cameraComponent = GetComponent<Camera>();
        if (cameraComponent.orthographic)
        {
            initialSize = cameraComponent.orthographicSize;
        }
        else
        {
            initialSize = cameraComponent.fieldOfView;
        }
    }

    void Update()
    {
        if (target != null)
        {
            FollowTarget();
            Scale();
        }
    }

    void FollowTarget()
    {
        // Calculate the desired position with offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Apply the new position to the camera
        transform.position = smoothedPosition;
    }

    void Scale()
    {
        if (cameraComponent != null)
        {
            // Determine how far the target has moved up
            float distanceFromStart = target.position.y;

            // Calculate the new size or FOV based on the distance
            float newSize = initialSize + distanceFromStart * scaleSpeed;

            // Clamp the size to ensure it doesn't zoom out too much
            newSize = Mathf.Clamp(newSize, initialSize, maxZoomOut);

            // Apply the new size or FOV to the camera
            if (cameraComponent.orthographic)
            {
                cameraComponent.orthographicSize = newSize;
            }
            else
            {
                cameraComponent.fieldOfView = newSize;
            }
        }
    }
}
