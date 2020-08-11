using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tankTower : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pusher;
    public int atkRange = 1;
    public int atkSpeed;
    public int atkSize;

    public Transform launchoffset;

    public Transform launchoffset2;
    bool fired = false;
    public float dur = 5f;
    public AudioSource attackSound;
    void Start()
    {
        //rb2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, dur);
    }

    // Update is called once per frame
    void Update()
    {
        var hitcolliders = Physics2D.OverlapBoxAll(launchoffset.position, new Vector2(atkRange * 1f, 1f), 0);
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
            attackSound.Play();
            GameObject pusherobj = Instantiate(pusher, launchoffset2.position + new Vector3(0, -0.1f, 0), transform.rotation);
            pusherobj.GetComponent<push>().AtkRange = atkRange;
            //            push.GetComponent<Fire>().size = atkSize;
            fired = true;
            StartCoroutine(fireCD());
        }
    }

    IEnumerator fireCD()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(4 - atkSpeed);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        fired = false;
    }
}
