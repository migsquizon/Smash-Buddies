using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBarPortal healthBarPortal;
    public GameObject gameOverScreen;


    public GameObject winScreen;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBarPortal.SetMaxHealth(maxHealth);
        gameOverScreen.SetActive(false);
    }


    public bool isInvulnerable = false;
    public GameObject[] players;

    private void Update()
    {
        int playerlives = 0;
        players = GameObject.FindGameObjectsWithTag("Player");
        if (currentHealth <= 0)
        {
            GameObject.Find("HUD Canvas/PortalHealthObject").gameObject.SetActive(false);
            winScreen.SetActive(true);
        }
        foreach (GameObject player in players)
        {
            playerlives += player.GetComponent<PlayerHealth>().life;

        }

        if (playerlives == 0)
        {
            GameObject.Find("HUD Canvas/PortalHealthObject").gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
        }




    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.gameObject.CompareTag("Enemy"))
        // {
        //     TakeDamage(30);
        //     Destroy(collision.gameObject);
        // }
    }

    public void TakeStatus(float damageOverTime, int duration)
    {

        //curSpeed = moveSpeed.speed;
        StartCoroutine(statusDuration(duration, damageOverTime));
    }
    IEnumerator statusDuration(int dur, float dot)
    {
        //Print the time of when the function is first called.
        Debug.Log("status duration started at : " + Time.time);

        for (float i = 0; i < dur; i++)
        {
            TakeDamage((int)dot);
            Debug.Log("DOT Taken" + dot);
            yield return new WaitForSeconds(1.0f);

        }
        //yield on a new YieldInstruction that waits for 5 seconds.

        //RoninActive = false;
        //After we have waited 5 seconds print the time again.
        Debug.Log("Status duration ended at : " + Time.time);

    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
        {
            return;
        }

        currentHealth -= damage;

        if (currentHealth < maxHealth * 0.5)
        {
            GetComponent<Animator>().SetBool("Enrage", true);
        }
        healthBarPortal.SetHealth(currentHealth);
    }
}
