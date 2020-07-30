using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //Enemy enemy = hitInfo.GetComponent<Enemy>();
        //if (enemy != null)
        //{
        //    enemy.TakeDamage(damage);
        //}
        Debug.Log(hitInfo);
        //Instantiate(impactEffect, transform.position, transform.rotation);
        if (hitInfo.gameObject.tag != "teleport")
        {
            //Debug.Log("Success!");
            Destroy(gameObject);
        }

        if (hitInfo.gameObject.tag == "Enemy")
        {
            Debug.Log("Success!");
            if (hitInfo.transform.position.x < transform.position.x)
            {
                hitInfo.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-2.0f, hitInfo.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                hitInfo.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(2.0f, hitInfo.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            }
            Destroy(gameObject);
        }


    }
}
