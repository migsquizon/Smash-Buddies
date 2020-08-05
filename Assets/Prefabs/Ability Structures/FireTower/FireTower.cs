using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fireBreadth;
    public int atkRange;
    public int atkSpeed;
    public Transform launchoffset;
    public int atkSize;
    bool fired = false;
    void Start()
    {
        //rb2D = GetComponent<Rigidbody2D>();
        
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
        
        var hitcolliders = Physics2D.OverlapBoxAll(launchoffset.position,new Vector2 (6f,1f),0);
        //var hitcolliders = Physics2D.OverlapCircleAll(new Vector3(launchoffset.position.x + (dir * Math.Abs(transform.position.y)), launchoffset.position.y, 0f), 3f);
        foreach (var hitcollider in hitcolliders)
        {
            var enemy = hitcollider.gameObject.tag;
            if (enemy == "Enemy")
            {
                //Debug.Log("Eneemy took hit");
                toFire();
            }
        }
    }

        public void toFire()
    {
        if (!fired)
        {
            GameObject fire = Instantiate(fireBreadth, transform.position, transform.rotation);
            fire.GetComponent<Fire>().AtkRange = atkRange;
            fire.GetComponent<Fire>().size = atkSize;
            fired = true;
            StartCoroutine(fireCD());
        }
    }

    IEnumerator fireCD()
    {
        //Print the time of when the function is first called.
        // Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(4-atkSpeed);

        //After we have waited 5 seconds print the time again.
        // Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        fired = false;
    }
}
