using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{

    [SerializeField]
    private float stunCooldown = 10.0f;
    [SerializeField]
    public int stunTime = 5;
    private float stunTimer;


    public bool isActive {get;set;} = false;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float duration = 5.0f;
    private float timeSinceAction;
    void Start()
    {
        //rb2D = GetComponent<Rigidbody2D>();
        timeSinceAction = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAction += Time.deltaTime;
        if(isActive) RoninAbility();
  
    }

    public void RoninAbility()
    {
    
      
        Debug.Log("shooting");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        StartCoroutine(shootCD());
        Transform opposite = transform.GetChild(5).GetComponent<Transform>();
        //Debug.Log(firePoint.rotation);
        //temporary.Rotate(0f, 180f, 0f);
        //Debug.Log(firePoint.rotation);
        //Quaternion temporary = firePoint.rotation;
        Instantiate(bulletPrefab, opposite.position, opposite.rotation);
        // temporary.Rotate(0f, 180f, 0f);
        if(timeSinceAction > duration) {
            isActive = false;
            timeSinceAction = 0;
        }
        
    }


        IEnumerator shootCD()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    
    }



}
