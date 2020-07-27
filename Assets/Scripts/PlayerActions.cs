using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerActions : MonoBehaviour
{

    public CharacterController2D controller;
    bool teleport = false;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    public bool jump { get; set; } = false;
    bool crouch = false;

    public float playerMovementX { get; set; } = 0;
    public float playerMovementY { get; set; } = 0;

    [SerializeField]
    private int playerIndex;


    public bool willShoot { get; set; } = false;


    public Transform firePoint;
    public GameObject bulletPrefab;

    void Shoot(bool s)
    {

        if (s) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }



    public int GetPlayerIndex()
    {
        return playerIndex;
    }



    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();

    }


    // Update is called once per frame
    void Update()
    {
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
                Debug.Log(transform.position);
                Debug.Log("teleporting!");
                if (transform.position.y > -1)
                {
                    
                    teleport = true;
                    transform.position = new Vector2(transform.position.x, -4f);
                    Debug.Log(transform.position);
                    StartCoroutine(ExampleCoroutine());
                }
                else if (transform.position.y < -1 )
                {
                    teleport = true;
                    transform.position = new Vector2(transform.position.x, 2f);
                    StartCoroutine(ExampleCoroutine());
                }
            }
        }
    }
    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
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

        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        //        Debug.Log(willShoot);
        Shoot(willShoot);
        willShoot = false;
        jump = false;
        // Move our character
        //controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        //jump = false;
    }
}