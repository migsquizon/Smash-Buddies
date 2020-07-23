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
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var players = FindObjectsOfType<PlayerActions>();
        var index = playerInput.playerIndex;
        player = players.FirstOrDefault(m => m.GetPlayerIndex() == index);
        //mover = GetComponent<PlayerMovement>();
    }


    public void OnLeftStick(CallbackContext val)
    {
        if (player != null)
        {
            //Debug.Log(context.ReadValue<Vector2>());
            player.playerMovementX = val.ReadValue<Vector2>()[0];
            player.playerMovementY = val.ReadValue<Vector2>()[1];

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
        Debug.Log("input read");
        if (player != null)
        {
            player.willShoot = true;
        }
    }


}