using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

	private enum State
	{
		ChasePortal,
		ChasePlayer,
	}

	public Transform target;
	public Transform portal;
	public float speed = 300f;
	public float nextWaypointDistance = 3f;
	public Transform enemyGFX;
	private State state;

	Path path;
	int currentWaypoint = 0;
	bool reachedEndOfPath = false;

	Seeker seeker;
	Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
    	state = State.ChasePortal;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {	
    	switch(state)
    	{
    		default:
    		case State.ChasePortal:
    			if (seeker.IsDone())
    				seeker.StartPath(rb.position, portal.position, OnPathComplete);
    			break;

    		case State.ChasePlayer:
    		    if (seeker.IsDone())
    				seeker.StartPath(rb.position, target.position, OnPathComplete);
    			break;
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
    	switch(state)
    	{
    		default:
    		case State.ChasePortal:
    			FindTarget();
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
    			break;
    		// Code for portal here 

    		case State.ChasePlayer:
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

			    direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
			    force = direction * speed * Time.deltaTime;
			    velocity = rb.velocity;


			    //rb.AddForce(force);

			    velocity.x = force.x;
			    rb.velocity = velocity;

			    distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

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
			    break;
    	}

    }

    private void FindTarget()
    {
    	float targetRange = 30f;
    	Debug.Log(Vector3.Distance(transform.position, target.position));
    	if (Vector3.Distance(transform.position, target.position) < targetRange)
    	{
    		//Player within target range
    		state = State.ChasePlayer;
    	}
    	else state = State.ChasePortal;
    }
}
