using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    float playerMovementX = 0;
    float playerMovementY = 0;


    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();

    }

    private void OnLeftStick(InputValue val)
    {
        playerMovementX = val.Get<Vector2>()[0];
        playerMovementY = val.Get<Vector2>()[1];
        Debug.Log(playerMovementY);
 

    }


    private void OnJump()
    {
        jump = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovementX > 0) { horizontalMove = 1.5f * runSpeed; }
        else if (playerMovementX == 0) { horizontalMove = 0 * runSpeed; }
        else { horizontalMove = -1.5f * runSpeed; }


        if (playerMovementY < 0) { crouch = true; }
        else { crouch = false; }

        //if (Input.GetButtonDown("Jump"))
        //{
        //    jump = true;
        //   // animator.SetBool("IsJumping", true);
        //}

        //if (Input.GetButtonDown("Crouch"))
        //{
        //    crouch = true;
        //}
        //else if (Input.GetButtonUp("Crouch"))
        //{
        //    crouch = false;
        //}

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