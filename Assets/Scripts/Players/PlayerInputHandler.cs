using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerActions player;
    private PlayerHealth playerHealth;
    private GameObject towerPicker;
    private GameObject powerUpPicker;
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
    private bool teleport = false;
    public GameObject teleporting;
    bool isboss = false;

    SpriteRenderer sprite;
    private void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Round 2")
        {
            isboss = true;
        }
        playerInput = GetComponent<PlayerInput>();
        var players = FindObjectsOfType<PlayerActions>();
        var index = playerInput.playerIndex;
        player = players.FirstOrDefault(m => m.GetPlayerIndex() == index);
        //mover = GetComponent<PlayerMovement>();
        playerHealth = player.GetComponent<PlayerHealth>();
        towerPicker = player.towerPicker;
        powerUpPicker = player.powerUpPicker;

        RoninTimer = RoninCD + 1;
        SageTimer = SageCD + 1;
        TankTimer = TankCD + 1;
        SupportTimer = SupportCD + 1;
        sprite = player.GetComponent<SpriteRenderer>();
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
            if (powerUpPicker.activeInHierarchy)
            {
                // get index of player
                SageCD -= 2;

            }
            else if (SageTimer > SageCD)
            {
                player.GetComponent<PlayerAbility>().fireBurn();
                SageTimer = 0;
                player.startSkillCoolDown(SageCD);
            }
        }
        if (player.gameObject.name == "Tank")
        {
            if (powerUpPicker.activeInHierarchy)
            {
                // get index of player
                TankCD -= 2;
            }
            else if (TankTimer > TankCD)
            {
                player.GetComponent<PlayerAbility>().tankStun(TankAOE, stunDuration);
                TankTimer = 0;
                player.startSkillCoolDown(TankCD);
            }
        }
        if (player.gameObject.name == "Servo")
        {
            if (powerUpPicker.activeInHierarchy)
            {
                // get index of player
                SupportCD -= 2;

            }
            if (SupportTimer > SupportCD)
            {
                player.GetComponent<PlayerAbility>().heartSpawn();
                Debug.Log("support skill used");
                SupportTimer = 0;
                //Tanktimer = 0;
                player.startSkillCoolDown(SupportCD);
            }
        }
        // THIS IS A WORKING RONIN SCRIPT BUT I AM TESTING

        if (player.gameObject.name == "Ronin")
        {
            if (powerUpPicker.activeInHierarchy)
            {
                // get index of player
                RoninCD -= 2;
            }
            if (RoninTimer > RoninCD)
            {
                
                sprite.color = new Color(1, 0, 0, 1);
                player.GetComponent<PlayerAbility>().RoninActive = true;
                RoninTimer = 0;
                player.GetComponent<PlayerAbility>().RoninDurFunc();
                //player.GetComponent<PlayerAbility>().RoninAbility();
                player.startSkillCoolDown(RoninCD);
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
        if (powerUpPicker.activeInHierarchy)
        {
            // get index of player
            player.buildTowerCooldown -= 5;
            return;
        }

        player.BuildTower();
        player.startTowerCoolDown();
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
    public void OnShowPowerUp()
    {
        Debug.Log("show power up");
        if (powerUpPicker.activeInHierarchy)
        {
            powerUpPicker.SetActive(false);
        }
        else
        {
            powerUpPicker.SetActive(true);
        }
    }

    public void OnTakeDamage()
    {
        // Debug.Log("R1 is pressed");
        playerHealth.TakeDamage(1);
    }
    bool teleportpressed = false;
    public void OnTeleport()
    {
        if (!teleportpressed && !isboss)
        {
            player.teleporting.SetActive(true);
            teleportpressed = true;
            Debug.Log("R2 is pressed");
            player.PlayerHit();
            StartCoroutine(PortalCoroutine());
        }
    }

    IEnumerator PortalCoroutine()
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);
        if (player.gameObject.transform.position.y > -1)
        {
            teleport = true;
            player.gameObject.transform.position = new Vector2(player.gameObject.transform.position.x, -4f);
            //Debug.Log(transform.position);

        }
        else if (player.gameObject.transform.position.y < -1)
        {
            teleport = true;
            player.gameObject.transform.position = new Vector2(player.gameObject.transform.position.x, 2f);

        }
        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        teleportpressed = false;
        player.teleporting.SetActive(false);
    }


    public void OnNextScene()
    {
        GameObject.Find("LevelChanger").GetComponent<LevelManager>().FadeToNextLevel();
    }

}