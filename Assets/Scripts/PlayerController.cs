using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed at which the player moves

    // Update is called once per frame
    void Update()
    {
        // Get input from the arrow keys
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveHorizontal = -1f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveHorizontal = 1f;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveVertical = 1f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveVertical = -1f;
        }

        // Move the player
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
