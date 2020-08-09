using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBarPortal healthBarPortal;
    public GameObject gameOverScreen;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBarPortal.SetMaxHealth(maxHealth);
        gameOverScreen.SetActive(false);
    }


    public bool isInvulnerable = false;

    private void Update()
    {
        if (currentHealth <= 0)
        {
            gameOverScreen.SetActive(true);
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(30);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
        {
            return;
        }

        currentHealth -= damage;

        if (currentHealth < 50)
        {
            GetComponent<Animator>().SetBool("Enrage", true);
        }
        healthBarPortal.SetHealth(currentHealth);
    }
}
