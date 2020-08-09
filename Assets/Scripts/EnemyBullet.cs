﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
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
        // Debug.Log(hitInfo);
        //Instantiate(impactEffect, transform.position, transform.rotation);
/*         if (hitInfo.gameObject.tag != "teleport")
        {
            // Debug.Log("Success!");
            // Debug.Log(hitInfo.gameObject.name);
            Destroy(gameObject);
        } */

        if (hitInfo.gameObject.tag == "Player")
        {
            Debug.Log("Damaged by bullet");
            //hitInfo.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
            Destroy(gameObject);
            //hitInfo.gameObject.GetComponent<EnemyHealth>().TakeStatus(20f,30f,5);

        }if (hitInfo.gameObject.tag == "Obstacle")
        {
            Debug.Log("Damaged by bullet");
            hitInfo.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
            //hitInfo.gameObject.GetComponent<EnemyHealth>().TakeStatus(20f,30f,5);

        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}