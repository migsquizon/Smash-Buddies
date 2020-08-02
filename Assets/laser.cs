﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class laser : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lineRenderer;
    public Transform offset;
    public Transform colliderCheck;

    public float atkRange;
    public float damage;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //raycast method
        /*
        float dir = transform.localRotation.x > 1 ? 1:-1;
        Debug.DrawRay(offset.position,new Vector2(dir*1f, 0));
        RaycastHit2D hit = Physics2D.Raycast(transform.position,new Vector3 (dir,0,0),atkRange);
        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0,offset.position);
            lineRenderer.SetPosition(1,hit.collider.transform.position);
            

        }
        else{
            lineRenderer.enabled = false;
        }*/
        //box collidermethod
        var closest = 999f;
        Vector3 enemy_loc = new Vector3 (0f,0f,0f);
        var hitcolliders = Physics2D.OverlapBoxAll(colliderCheck.position,new Vector2 (3f,1f),0);
        foreach (var hitcollider in hitcolliders)
        {
            var enemy = hitcollider.gameObject.tag;
            if (enemy == "Enemy")
            {
                // get position
                var enemy_location_x = hitcollider.gameObject.transform.position.x;
                var dist_to_player = Math.Abs(enemy_location_x - transform.position.x);
                if (dist_to_player < closest){
                    closest = dist_to_player * 1f;
                    enemy_loc = hitcollider.gameObject.transform.position;
                }

                //Debug.Log(transform.position.x);
                
            }
        }
        Debug.Log(closest);
        if (closest < atkRange){
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0,offset.position);
        lineRenderer.SetPosition(1,enemy_loc);}
        //ENEMY TAKE DAMAGE HERE
        else{
            lineRenderer.enabled = false;
        }
    }
}
