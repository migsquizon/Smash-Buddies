using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    public bool jump { get; set; } = false;
    bool crouch = false;

    public float playerMovementX { get; set; } = 0;
    public float playerMovementY { get; set; } = 0;

    [SerializeField]
    private int playerIndex = 0;



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
        jump = false;
        // Move our character
        //controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        //jump = false;
    }
}