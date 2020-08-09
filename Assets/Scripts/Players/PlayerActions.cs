using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerActions : MonoBehaviour
{

    CharacterController2D controller;
    bool teleport = false;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    public bool jump { get; set; } = false;
    bool crouch = false;

    public float playerMovementX { get; set; } = 0;
    public float playerMovementY { get; set; } = 0;

    [SerializeField]
    private int playerIndex;


    public GameObject[] respawns;

    [SerializeField]
    public float buildTowerCooldown = 0f;

    private float buildTowerTimer;


    [SerializeField]
    private float stunCooldown = 10.0f;
    [SerializeField]
    public int stunTime = 5;
    private float stunTimer;
    public Animator animator;


    public bool willShoot { get; set; } = false;

    public bool buildTower { get; set; } = false;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject towerPrefab;
    public GameObject fire;
    public GameObject coinManager;
    public GameObject towerPicker;
    public GameObject powerUpPicker;
    private PlayerHealth playerHealth;
    public CoolDownBar towerCoolDownBar;
    private float currentTowerCoolDownTime;
    private bool towerCoolDownIsRunning;
    public CoolDownBar skillCoolDownBar;
    private float currentSkillCoolDownTime;
    private bool skillCoolDownIsRunning;
    private float skillCoolDown;


    void Start()
    {

        buildTowerTimer = buildTowerCooldown + 1;
        // currentTowerCoolDownTime = 0;
        if (towerCoolDownBar != null)
        {
            towerCoolDownBar.SetMaxTime(buildTowerCooldown);
            towerCoolDownBar.SetTime(buildTowerCooldown);
            towerCoolDownIsRunning = false;
        }
        if (skillCoolDownBar != null)
        {
            skillCoolDownBar.SetMaxTime(1);
            skillCoolDownBar.SetTime(1);
            skillCoolDownIsRunning = false;
        }
    }

    public void BuildTower(GameObject tower, int price)
    {
        if (coinManager.GetComponent<CoinManager>().coin >= price)
        {
            Vector3 buildPos = new Vector3(firePoint.position.x, firePoint.position.y + 0.5f, firePoint.position.y);
            Instantiate(tower, buildPos, firePoint.rotation);
            coinManager.GetComponent<CoinManager>().BuyCoin(price);
            Debug.Log(horizontalMove);
        }

    }


    public void Ability()
    {

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Transform opposite = transform.GetChild(4).GetComponent<Transform>();
        //Debug.Log(firePoint.rotation);
        //temporary.Rotate(0f, 180f, 0f);
        //Debug.Log(firePoint.rotation);
        //Quaternion temporary = firePoint.rotation;
        Instantiate(bulletPrefab, opposite.position, opposite.rotation);
        //temporary.Rotate(0f, 180f, 0f);

    }


    public void Shoot()
    {
        animator.SetTrigger("Attack");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }


    public void BuildTower()
    {
        if (buildTowerTimer > buildTowerCooldown)
        {
            buildTowerTimer = 0;
            Debug.Log(transform.position);

            //Debug.Log(transform.localRotation);

            Debug.Log(firePoint.position);

            //Debug.Log(firePoint.localRotation);
            Instantiate(towerPrefab, firePoint.position, Quaternion.Euler(0, transform.position.x < firePoint.position.x ? 180f : 0, 0));
            // Instantiate(towerPrefab, firePoint.position, firePoint.rotation);
            //towerPrefab.transform.rotation = transform.rotation;


        }
        else
        {
            Debug.Log("Build tower is on cooldown");
        }

    }


    public void TankStun()
    {
        if (stunTimer > stunCooldown)
        {
            buildTowerTimer = 0;

        }
        else
        {
            Debug.Log("stun is on cooldown");
        }

    }



    private bool isStun;

    public void PlayerHit()
    {
        // If we are already stun, quit; we don't want to be hit again when we are stun
        if (isStun)
        {
            Debug.Log("player is stunned, cannot stun again");
            return;
        }

        //DamageTaken();
        isStun = true;


        StartCoroutine(WaitForStunToEnd());
    }

    private IEnumerator WaitForStunToEnd()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(stunTime);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        isStun = false;

    }



    public int GetPlayerIndex()
    {
        return playerIndex;
    }



    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        towerPicker.SetActive(false);
        powerUpPicker.SetActive(false);
        playerHealth = gameObject.GetComponent<PlayerHealth>();
    }


    // Update is called once per frame
    void Update()
    {

        if (playerMovementX > 0) { horizontalMove = 1.5f * runSpeed; }
        else if (playerMovementX == 0) { horizontalMove = 0 * runSpeed; }
        else { horizontalMove = -1.5f * runSpeed; }
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (playerMovementY < 0) { crouch = true; }
        else { crouch = false; }

        buildTowerTimer += Time.deltaTime;

        if (playerMovementX > 0) { horizontalMove = 1.5f * runSpeed; }
        else if (playerMovementX == 0) { horizontalMove = 0 * runSpeed; }
        else { horizontalMove = -1.5f * runSpeed; }


        if (playerMovementY < 0) { crouch = true; }
        else { crouch = false; }

        if (towerCoolDownIsRunning)
        {
            if (currentTowerCoolDownTime < buildTowerCooldown)
            {
                currentTowerCoolDownTime += Time.deltaTime;
                towerCoolDownBar.SetTime(currentTowerCoolDownTime);
            }
            else
            {
                towerCoolDownIsRunning = false;
                currentTowerCoolDownTime = buildTowerCooldown;
                towerCoolDownBar.SetTime(currentTowerCoolDownTime);
            }
        }

        if (skillCoolDownIsRunning)
        {
            if (currentSkillCoolDownTime < skillCoolDown)
            {
                currentSkillCoolDownTime += Time.deltaTime;
                skillCoolDownBar.SetTime(currentSkillCoolDownTime);
            }
            else
            {
                skillCoolDownIsRunning = false;
                currentSkillCoolDownTime = skillCoolDown;
                skillCoolDownBar.SetTime(currentSkillCoolDownTime);
            }
        }
    }

    public void startTowerCoolDown()
    {
        if (towerCoolDownIsRunning == false)
        {
            towerCoolDownBar.SetMaxTime(buildTowerCooldown);
            towerCoolDownIsRunning = true;
            currentTowerCoolDownTime = 0;
        }
    }

    public void startSkillCoolDown(float skillTime)
    {
        if (skillCoolDownIsRunning == false)
        {
            skillCoolDown = skillTime;
            skillCoolDownBar.SetMaxTime(skillCoolDown);
            skillCoolDownIsRunning = true;
            currentSkillCoolDownTime = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("teleport") && !teleport)
        {
            {
                //Debug.Log(transform.position);
                //Debug.Log("teleporting!");
                if (transform.position.y > -1)
                {
                    teleport = true;
                    transform.position = new Vector2(transform.position.x, -4f);
                    //Debug.Log(transform.position);
                    StartCoroutine(PortalCoroutine());
                }
                else if (transform.position.y < -1)
                {
                    teleport = true;
                    transform.position = new Vector2(transform.position.x, 2f);
                    StartCoroutine(PortalCoroutine());
                }
            }
        }
    }



    IEnumerator PortalCoroutine()
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);

        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        teleport = false;
    }

    public void OnLanding()
    {
        //   animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        // animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        if (isStun) return;
        if (playerHealth.dead) return;
        //else Debug.Log("player is stunned!");
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}