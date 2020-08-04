﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject[] hearts;
    private int life;
    private bool dead;
    private bool isHit = false;
    private int nextHit = 2;

    private void Start()
    {
        life = hearts.Length;
    }

    public void TakeDamage(int damagePoint)
    {
        if (life >= 1)
        {
            Debug.Log("Damage Taken");
            life -= damagePoint;
            Destroy(hearts[life].gameObject);
            if (life < 1)
            {
                dead = true;
                Debug.Log("<<<DEAD>>>");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D hitInfo) {
        if (hitInfo.gameObject.tag == "Enemy" && !isHit) {
            Debug.Log("Collide with enemy. Player takes damage");
            PlayerHit();
            TakeDamage(1);
            
        };


    }


    public void PlayerHit()
    {
        // If we are already stun, quit; we don't want to be hit again when we are stun
        if (isHit)
        {
            Debug.Log("player is stunned, cannot stun again");
            return;
        }

        //DamageTaken();
        isHit = true;


        StartCoroutine(WaitForPlayerToRecover());
    }

    private IEnumerator WaitForPlayerToRecover()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(nextHit);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        isHit = false;

    }


}
