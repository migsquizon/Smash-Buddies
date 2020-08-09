using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_1 : MonoBehaviour
{
    public bool hasCollided = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reach destination");
            hasCollided = true;
        }
    }
}
