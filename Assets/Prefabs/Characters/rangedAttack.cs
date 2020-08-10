﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class rangedAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rangedProjectile;

    public EnemyHealth enemyHealth;
    public int atkRange;
    public int atkSpeed;
    public Transform collideroffset;
    bool fired = false;
    public Transform launchoffset;
    public Transform playerpos;
    Transform toShootAt;
    public float dur = 5f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var hitcolliders = Physics2D.OverlapBoxAll(collideroffset.position, new Vector2(atkRange * 1f, 2f), 0);
        //var hitcolliders = Physics2D.OverlapCircleAll(new Vector3(launchoffset.position.x + (dir * Math.Abs(transform.position.y)), launchoffset.position.y, 0f), 3f);
        bool goingtofire = false;
        var closest = 99999;
        foreach (var hitcollider in hitcolliders)
        {
            var enemy = hitcollider.gameObject.tag;
            if (enemy == "Player" || enemy == "Obstacle")
            {
                //Debug.Log("Eneemy took hit");
                goingtofire = true;
                playerpos = hitcollider.gameObject.transform;
                var playerx = playerpos.position.x - transform.position.x;
                if (Math.Abs(playerx) < closest) toShootAt = playerpos;

            }
        }
        if (goingtofire)
        {
            toFire();
        }
    }

    public void toFire()
    {
        if (!fired)
        {

            float ms = gameObject.GetComponent<EnemyHealth>().moveSpeed.speed;
            gameObject.GetComponent<EnemyHealth>().TakeStatus(0, ms, atkSpeed);

            GameObject fire = Instantiate(rangedProjectile, launchoffset.position, Quaternion.identity);
            Debug.Log("fire.GetComponent<Rigidbody2D>().velocity");
            fire.GetComponent<Rigidbody2D>().velocity = (toShootAt.position - launchoffset.position).normalized * 4f;
            // Debug.Log(fire.GetComponent<Rigidbody2D>().velocity);
            fire.GetComponent<Fire>().AtkRange = atkRange;
            fired = true;
            StartCoroutine(attackCD());
        }
    }

    IEnumerator attackCD()
    {
        yield return new WaitForSeconds(atkSpeed);
        fired = true;
        //hasAttacked = false;
        //animator.SetBool("Attack", false);
    }
}
