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
    private float RoninCD = 10f;
    private float RoninTimer;
    public GameObject towerOne;
    public int towerOnePrice;
    public GameObject towerTwo;
    public int towerTwoPrice;
    public GameObject towerThree;
    public int towerThreePrice;
    private float SageTimer;
    private float SageCD = 5f;
    private float TankTimer;
    private float TankCD = 5f;
    private float SupportTimer;
    private float SupportCD = 5f;
    private float TankAOE = 4f;
    private int stunDuration = 5;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var players = FindObjectsOfType<PlayerActions>();
        var index = playerInput.playerIndex;
        player = players.FirstOrDefault(m => m.GetPlayerIndex() == index);
        //mover = GetComponent<PlayerMovement>();
        playerHealth = player.GetComponent<PlayerHealth>();
        towerPicker = player.towerPicker;

        RoninTimer = RoninCD + 1;
        SageTimer = SageCD + 1;
        TankTimer = TankCD + 1;
        SupportTimer = SupportCD + 1;
    }
    private void Update()
    {
        RoninTimer += Time.deltaTime;
        SageTimer += Time.deltaTime;
        TankTimer += Time.deltaTime;
        SupportTimer += Time.deltaTime;

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
                if (player.GetComponent<PlayerAbility>().RoninActive && player.gameObject.name == "Ronin")
                {
                    player.GetComponent<PlayerAbility>().RoninAbility();
                    Debug.Log("ABILITYING");
                }
                else player.Shoot();
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
        if (player.gameObject.name == "Sage")
        {
            if (SageTimer > SageCD)
            {
                player.GetComponent<PlayerAbility>().fireBurn();
                SageTimer = 0;
            }
        }
        if (player.gameObject.name == "Tank")
        {
            if (TankTimer > TankCD)
            {
                player.GetComponent<PlayerAbility>().tankStun(TankAOE, stunDuration);
                TankTimer = 0;
            }
        }
        if (player.gameObject.name == "Servo")
        {
            if (SupportTimer > SupportCD)
            {
                player.GetComponent<PlayerAbility>().heartSpawn();
                Debug.Log("support skill used");
                SupportTimer = 0;
                //Tanktimer = 0;
            }
        }
        // THIS IS A WORKING RONIN SCRIPT BUT I AM TESTING

        if (player.gameObject.name == "Ronin")
        {
            if (RoninTimer > RoninCD)
            {
                player.GetComponent<PlayerAbility>().RoninActive = true;
                RoninTimer = 0;
                player.GetComponent<PlayerAbility>().RoninDurFunc();
                //player.GetComponent<PlayerAbility>().RoninAbility();
            }

            else
            {
                Debug.Log("RONIN ABILITY ON CD");
            }

        }
    }


    public void OnCircle()
    {
        if (towerPicker.activeInHierarchy)
        {
            player.BuildTower(towerThree, towerThreePrice);
            return;
        }
        player.BuildTower();
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