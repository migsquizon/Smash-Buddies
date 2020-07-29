using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BombTower : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bombPrefab;
    public GameObject inviball;
    public int splashRange;
    public Transform launchoffset;
    private bool bombed = false;
    public float atkRange = 5f;
    void Start()
    {
        //rb2D = GetComponent<Rigidbody2D>();

        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        float dir;
        if (transform.rotation.y > 1)
        {
            dir = 1f;
        }
        else
        {
            dir = -1f;
        }
        //RaycastHit[] hits; 
        //hits = Physics.RaycastAll(launchoffset.position, new Vector2(5f,0), 100.0F);
        Debug.DrawRay(new Vector3(launchoffset.position.x + (dir * Math.Abs(transform.position.y)), launchoffset.position.y, 0f), new Vector2(dir*1f, 0));
        // Debug.Log(hits.Length);
        //RaycastHit2D hit = Physics2D.Raycast(launchoffset.position, new Vector2(atkRange, 0));
        var hitcolliders = Physics2D.OverlapCircleAll(new Vector3(launchoffset.position.x + (dir * Math.Abs(transform.position.y)), launchoffset.position.y, 0f), 3f);
        foreach (var hitcollider in hitcolliders)
        {
            var enemy = hitcollider.gameObject.tag;
            if (enemy == "Enemy")
            {
                Debug.Log("Eneemy took hit");
                toThrow();
            }
        }
        //for (int i = 0; i < hits.Length; i++){
        /*if (hit.collider != null )//&& hit.collider.CompareTag("Enemy"))
        {
            //Debug.Log(hit.collider.tag);
            if (!bombed)
                {
                    Instantiate(bombPrefab, launchoffset.position, transform.rotation);
                    bombed = true;
                    StartCoroutine(ExampleCoroutine());
                }
            

        
       
        }*/
    }

    public void toThrow()
    {
        if (!bombed)
        {
            Instantiate(bombPrefab, launchoffset.position, transform.rotation);
            bombed = true;
            StartCoroutine(ExampleCoroutine());
        }
    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        bombed = false;
    }
}
