using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public bool dead;
    public bool isHit;
    public HealthbarBehaviour healthbar;
    public HealthbarScriptAgain healthbar2;
    public Transform child;

    void OnEnable()
    {
        // Debug.Log(transform.rotation.y + " rotation of obs");
        // if (transform.rotation.y < 0) {
        //     child.rotation = Quaternion.Euler(0, 180, 0);
        //     Debug.Log("HERE");
        // }
        currentHealth = maxHealth;
        dead = false;
        healthbar2.SetHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthbar2.SetHealth(currentHealth, maxHealth);
        Debug.Log(currentHealth + " currentHealth");
        if (currentHealth <= 0 && !dead) OnDeath();
    }

    void OnDeath()
    {
        dead = true;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
