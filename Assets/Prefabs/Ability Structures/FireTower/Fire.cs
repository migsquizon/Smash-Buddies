using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed = 4;
    public Vector3 LaunchOffset;
    public bool Thrown;
    public float Damage = 10                                ;
    public int AtkRange = 1;

    public int size = 1;
    public Rigidbody2D rb;
    void Start()
    {                                                                                                               
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right*-1f * Speed;
        /*transform.localScale = transform.localScale + new Vector3 (0,size - 1,0);
        if (Thrown){
            
            
            var direction = -transform.right;
            GetComponent<Rigidbody2D>().AddForce(direction * Speed, ForceMode2D.Impulse); 
        }
        transform.Translate(LaunchOffset);*/
        Destroy(gameObject, AtkRange);
    }

    // Update is called once per frame
    void Update()
    {
        // if (!Thrown){
        //     //Debug.Log(transform.right);
            
        //     transform.position += -transform.right * Speed * Time.deltaTime;
            
        // }
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
        if (hitInfo.gameObject.tag == "Enemy")
        {
            // Debug.Log("burn!");
            hitInfo.gameObject.GetComponent<EnemyHealth>().TakeDamage(Damage);
            //Destroy(gameObject);
           // hitInfo.gameObject.GetComponent<EnemyHealth>().TakeStatus(20f,30f,5);
        }

        
    }
}
