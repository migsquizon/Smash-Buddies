using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public int scale = 0;
    public int aoeSlow = 5;

    public int aoeheal = 0;


    public int aoedamage = 5;
    void Start()
    {
        // Debug.Log(transform.localScale);
        transform.localScale = transform.localScale + new Vector3(scale, 0, 0);
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
        // Debug.Log(hitInfo);
        //Instantiate(impactEffect, transform.position, transform.rotation);
        if (hitInfo.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy enter!");
            Debug.Log("Going to slow enemy by " + aoeSlow.ToString());
            
            Debug.Log("Going to damage enemy by " + aoedamage.ToString());
            hitInfo.gameObject.GetComponent<EnemyHealth>().TakeDamage(aoedamage);
        }
        if (hitInfo.gameObject.tag == "Player")
        {
            Debug.Log("Player enter!");
            Debug.Log("Going to heal player by " + aoeheal.ToString());
        }
    }

    void OnTriggerExit2D(Collider2D hitInfo)
    {
        //Enemy enemy = hitInfo.GetComponent<Enemy>();
        //if (enemy != null)
        //{
        //    enemy.TakeDamage(damage);
        //}
        Debug.Log(hitInfo);
        //Instantiate(impactEffect, transform.position, transform.rotation);
        if (hitInfo.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy exit!");
        }
        if (hitInfo.gameObject.tag == "Player")
        {
            Debug.Log("Player exit!");
        }

    }

}
