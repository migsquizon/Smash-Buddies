using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthforall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (hitInfo.gameObject.tag == "Player")
        {
           
            hitInfo.gameObject.GetComponent<PlayerHealth>().Heal();
            Destroy(gameObject);
        }

        
    }
}
