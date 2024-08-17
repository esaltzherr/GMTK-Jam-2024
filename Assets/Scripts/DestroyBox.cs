using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This method is called when another collider stops touching the trigger collider attached to this object
    private void OnTriggerExit2D(Collider2D other)
    {
        // Destroy the GameObject that has exited the trigger
        Destroy(other.gameObject);
    }
}
