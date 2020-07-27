using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("teleport"))
        {
            Debug.Log("teleporting!");
            if (transform.position.y > 0 && Input.GetKeyDown(KeyCode.Space))
            {
                transform.position = new Vector2(transform.position.x, -4f);
            }
            if (transform.position.y < 0 && Input.GetKeyDown(KeyCode.Space))
            {
                transform.position = new Vector2(transform.position.x, 2f);
            }
        }
    }
}
