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
    private float buildTowerCooldown = 10.0f;

    private float buildTowerTimer;
    

    [SerializeField]
    private float stunCooldown = 10.0f;
    [SerializeField]
    public int stunTime = 5;
    private float stunTimer;



    public bool willShoot { get; set; } = false;

    public bool buildTower { get; set; } = false;


    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject towerPrefab;
    public GameObject fire;
    public GameObject coinManager;
    public GameObject towerPicker;


    void Start()
    {
        buildTowerTimer = buildTowerCooldown + 1;

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

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);


    }


    public void BuildTower()
    {
        if (buildTowerTimer > buildTowerCooldown)
        {
            buildTowerTimer = 0;
            Instantiate(towerPrefab, firePoint.position, firePoint.rotation);
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

    }


    // Update is called once per frame
    void Update()
    {

        buildTowerTimer += Time.deltaTime;


        if (playerMovementX > 0) { horizontalMove = 1.5f * runSpeed; }
        else if (playerMovementX == 0) { horizontalMove = 0 * runSpeed; }
        else { horizontalMove = -1.5f * runSpeed; }


        if (playerMovementY < 0) { crouch = true; }
        else { crouch = false; }



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
        //else Debug.Log("player is stunned!");
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);


        jump = false;




    }
}