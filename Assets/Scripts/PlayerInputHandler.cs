using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerActions player;
    private HeartSystem heartSystem;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var players = FindObjectsOfType<PlayerActions>();
        var index = playerInput.playerIndex;
        player = players.FirstOrDefault(m => m.GetPlayerIndex() == index);
        //mover = GetComponent<PlayerMovement>();
        heartSystem = player.GetComponent<HeartSystem>();
    }

    public void OnLeftStick(InputValue val)
    {
  
        if (player != null)
        {


            //Debug.Log(val.Get());
            //Debug.Log(context.ReadValue<Vector2>());
            Debug.Log(val.Get<Vector2>());
            player.playerMovementX = val.Get<Vector2>()[0];


            player.playerMovementY = val.Get<Vector2>()[1];

            //Debug.Log(playerMovementY);
        }

    }


    public void OnJump()
    {
        if (player != null)
        {
            player.jump = true;
        }
    }

    public void OnShoot()
    {
       
       
        if (player != null)
        {
            player.willShoot = true;
        }
    }

    public void OnTakeDamage()
    {
        heartSystem.TakeDamage(1);
    }

}