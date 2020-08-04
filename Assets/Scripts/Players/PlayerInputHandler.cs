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
    private PlayerHealth playerHealth;
    private GameObject towerPicker;
    public GameObject towerOne;
    public int towerOnePrice;
    public GameObject towerTwo;
    public int towerTwoPrice;
    public GameObject towerThree;
    public int towerThreePrice;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var players = FindObjectsOfType<PlayerActions>();
        var index = playerInput.playerIndex;
        player = players.FirstOrDefault(m => m.GetPlayerIndex() == index);
        //mover = GetComponent<PlayerMovement>();
        playerHealth = player.GetComponent<PlayerHealth>();
        towerPicker = player.towerPicker;
    }

    public void OnLeftStick(InputValue val)
    {

        if (player != null)
        {


            //Debug.Log(val.Get());
            //Debug.Log(context.ReadValue<Vector2>());
            // Debug.Log(val.Get<Vector2>());
            player.playerMovementX = val.Get<Vector2>()[0];


            player.playerMovementY = val.Get<Vector2>()[1];

            //Debug.Log(playerMovementY);
        }

    }


    public void OnX()
    {
        if (player != null)
        {
            player.jump = true;
        }
    }

    public void OnSquare()
    {
        if (towerPicker.activeInHierarchy)
        {
            player.BuildTower(towerOne, towerOnePrice);
        }
        else
        {
            if (player != null)
            {
                player.Shoot();
            }
        }

    }

    public void OnTriangle()
    {
        if (towerPicker.activeInHierarchy)
        {
            player.BuildTower(towerTwo, towerTwoPrice);
            return;
        }
        Debug.Log(player.gameObject.name);
        if(player.gameObject.name == "Ronin"){

            player.GetComponent<PlayerAbility>().isActive = true;
        }
    }

    public void OnCircle()
    {
        if (towerPicker.activeInHierarchy)
        {
            player.BuildTower(towerThree, towerThreePrice);
            return;
        }
    }



    public void OnShowShop()
    {
        Debug.Log("show shop");
        if (towerPicker.activeInHierarchy)
        {
            towerPicker.SetActive(false);
        }
        else
        {
            towerPicker.SetActive(true);
        }
    }

    public void OnTakeDamage()
    {
        Debug.Log("R1 is pressed");
        playerHealth.TakeDamage(1);
    }


}