using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public bool dead;
    public bool isHit;
    public HealthbarScriptAgain healthbar2;
    public AudioSource takeDamage;
    public AudioSource destroyed;

    void OnEnable()
    {
        currentHealth = maxHealth;
        dead = false;
        healthbar2.SetHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        takeDamage.Play();
        currentHealth -= amount;
        healthbar2.SetHealth(currentHealth, maxHealth);
        Debug.Log(currentHealth + " currentHealth");
        if (currentHealth <= 0 && !dead) StartCoroutine(OnDeath());
    }


    IEnumerator OnDeath()
    {
        destroyed.Play();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        dead = true;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
