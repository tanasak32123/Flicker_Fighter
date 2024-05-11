using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Teleporter hit");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in");
            other.transform.position = destination.position;
        }
        
    }
}
