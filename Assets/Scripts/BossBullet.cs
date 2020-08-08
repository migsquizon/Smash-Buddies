using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 20f;
    //public int damage = 40;
    public Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if (hitInfo.gameObject.tag == "Player")
        {
            Debug.Log("Damaged by bullet");
            hitInfo.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
            //hitInfo.gameObject.GetComponent<EnemyHealth>().TakeStatus(20f,30f,5);

        }

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
