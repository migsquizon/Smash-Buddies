using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    private Transform portal;

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

        portal = GameObject.FindGameObjectWithTag("MainPortal").GetComponent<Transform>();
        Debug.Log(portal);

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {

        //RaycastHit2D hit_front = Physics2D.Raycast(transform.position + new Vector3(1, 0, 0), new Vector2(visionRange, 0), visionRange);
        //RaycastHit2D hit_back = Physics2D.Raycast(transform.position + new Vector3(-1, 0, 0), new Vector2(visionRange * -1f, 0), visionRange);

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 5.0f, new Vector2(1, 0), visionRange);
        //Debug.Log(hit.collider.tag);
        if (seeker.IsDone())
        {
            foreach (RaycastHit2D hit in hits)
            {
                if ((hit.collider != null && hit.collider.CompareTag("Player")))
                {
                    seeker.StartPath(rb.position, hit.point, OnPathComplete);
                    return;
                }
            }
            seeker.StartPath(rb.position, portal.position, OnPathComplete); //Cases the portal instead  
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

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
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
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }

    }

    
}