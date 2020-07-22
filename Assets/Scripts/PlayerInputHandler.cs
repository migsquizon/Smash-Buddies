using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMovement mover;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var movers = FindObjectsOfType<PlayerMovement>();
        var index = playerInput.playerIndex;
        mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
        //mover = GetComponent<PlayerMovement>();
    }


    public void OnLeftStick(CallbackContext val)
    {
        //Debug.Log(context.ReadValue<Vector2>());
        mover.playerMovementX = val.ReadValue<Vector2>()[0];
        mover.playerMovementY = val.ReadValue<Vector2>()[1];
        Debug.Log("input read");
        //Debug.Log(playerMovementY);


    }


    public void OnJump()
    {
        mover.jump = true;
    }


}