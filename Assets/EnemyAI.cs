using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform portal; 

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

    	RaycastHit2D hit_front = Physics2D.Raycast(transform.position + new Vector3(1,0,0), new Vector2(visionRange, 0), visionRange);
		RaycastHit2D hit_back = Physics2D.Raycast(transform.position + new Vector3(-1,0,0), new Vector2(visionRange * -1f, 0), visionRange);

        if (seeker.IsDone()) {
            if ((hit_front.collider != null && hit_front.collider.CompareTag("Player")) || (hit_back.collider != null && hit_back.collider.CompareTag("Player"))) { //Chases the nearest player 
                if (hit_front.collider == null) { //Player is behind
                    seeker.StartPath(rb.position, hit_back.point, OnPathComplete);
                } else if (hit_back.collider == null) { //Player is in front
                    seeker.StartPath(rb.position, hit_front.point, OnPathComplete);
                } else if (Vector2.Distance(transform.position, hit_front.point) > Vector2.Distance(transform.position, hit_back.point)){ //enemy behind is closer
                    seeker.StartPath(rb.position, hit_back.point, OnPathComplete);
                } else {
                    seeker.StartPath(rb.position, hit_front.point, OnPathComplete);
                }
            } else {
                seeker.StartPath(rb.position, portal.position, OnPathComplete); //Chases the portal instead
            }
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

    }
}
