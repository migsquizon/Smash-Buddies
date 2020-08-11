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


    public GameObject fire;
    public GameObject heart;
    public bool RoninActive { get; set; } = false;

    public Transform firePoint;
    public Transform supportOffset;
    public GameObject bulletPrefab;
    public GameObject stunEffect;
    public float duration = 5.0f;
    private float timeSinceAction;
    public AudioSource abilitySound;
    bool fired = false;
    SpriteRenderer sprite;
    void Start()
    {
        //rb2D = GetComponent<Rigidbody2D>();
        timeSinceAction = 0.0f;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAction += Time.deltaTime;

        // if(isActive && !fired) RoninAbility();

    }

    public void RoninAbility()
    {
        fired = true;
        abilitySound.Play();
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        StartCoroutine(shootCD());
        Transform opposite = transform.GetChild(5).GetComponent<Transform>();
        GameObject oppBullet = Instantiate(bulletPrefab, opposite.position, opposite.rotation);
    }

    public void tankStun(float AOE, int stunDuration)
    {
        abilitySound.Play();
        GameObject stun_ = (GameObject)Instantiate(stunEffect, transform.position, transform.rotation);
        Destroy(stun_, 2.0f);
        var hitcolliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(AOE, 2f), 0);
        foreach (var hitcollider in hitcolliders)
        {
            var enemy = hitcollider.gameObject.tag;
            if (enemy == "Enemy")
            {
                var ms = hitcollider.gameObject.GetComponent<EnemyHealth>().moveSpeed.speed;
                hitcollider.gameObject.GetComponent<EnemyHealth>().TakeStatus(0, ms, stunDuration);
            }
        }
    }

    public void fireBurn()
    {
        abilitySound.Play();
        //Transform opposite = transform.GetChild(4).GetComponent<Transform>();
        Instantiate(fire, firePoint.position, firePoint.rotation);
    }

    public void heartSpawn()
    {
        abilitySound.Play();
        // Transform opposite = transform.GetChild(4).GetComponent<Transform>();
        Instantiate(heart, firePoint.position, firePoint.rotation);
    }
    public void RoninDurFunc()
    {
        StartCoroutine(RoninDuration());
    }


    IEnumerator shootCD()
    {
        //Print the time of when the function is first called.
        // Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.3f);
        fired = false;
        //After we have waited 5 seconds print the time again.
        // Debug.Log("Finished Coroutine at timestamp : " + Time.time);


    }

    IEnumerator RoninDuration()
    {
        //Print the time of when the function is first called.
        // Debug.Log("Ronin ability duration started at : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        sprite.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(5.0f);
        RoninActive = false;
        //After we have waited 5 seconds print the time again.
        // Debug.Log("Ronin Ability duration ended at : " + Time.time);
        sprite.color = new Color(1, 1, 1, 1);
    }
}
