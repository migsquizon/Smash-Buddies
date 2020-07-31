﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

	public Transform target;

	public float speed = 300f;
	public float nextWaypointDistance = 3f;
	public int visionRange;
	public Transform enemyGFX;

	Path path;
	int currentWaypoint = 0;
	bool reachedEndOfPath = false;

	Seeker seeker;
	Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
    	//float distToPlayer = Vector2.Distance(transform.position, target.position);
    	RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(1,0,0), new Vector2(visionRange, 0), visionRange);
    	Debug.DrawRay(transform.position, new Vector2(visionRange*1f, 0));
    	Debug.Log(hit.collider);
    	Debug.Log(hit.collider.CompareTag("Player"));
    	if (seeker.IsDone() && hit.collider != null && hit.collider.CompareTag("Player")) {
    		seeker.StartPath(rb.position, target.position, OnPathComplete);
    		Debug.Log("Update Path");
    	}

    }

    void OnPathComplete(Path p)
    {
    	if (!p.error)
    	{
    		path = p;
    		currentWaypoint = 0;
    	}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	//float distToPlayer = Vector2.Distance(transform.position, target.position);
    	//Debug.Log("Distance to Player" + distToPlayer);
    	//RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(visionRange, 0));
        //if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        //if (distToPlayer < visionRange)
        //{
    	if (path == null)
        	return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
        	reachedEndOfPath = true;
        	return;
        } else
        {
        	reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        Vector2 velocity = rb.velocity;


        //rb.AddForce(force);

        velocity.x = force.x;
        rb.velocity = velocity;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
        	currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
        	enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
        	enemyGFX.localScale = new Vector3(-1f ,1f, 1f);
        }
        //}

    }
}
