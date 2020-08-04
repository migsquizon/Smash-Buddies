using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyStats enemyStats;
    public float m_CurrentHealth;
    public float m_MaxHealth;
    public bool m_Dead;
    public HealthbarBehaviour healthbar;

    void OnEnable()
    {
        m_MaxHealth = enemyStats.health;
        m_CurrentHealth = enemyStats.health;
        m_Dead = false;
        healthbar.SetHealth(80, 100);
    }

    public void TakeDamage(float amount)
    {
        // tag can be used to determine status. for example, what happen if it takes damage while debuffed. (add multiplier etc)
        m_CurrentHealth -= amount;
        SetHealthUI();

        if (m_CurrentHealth <= 0f && !m_Dead) OnDeath();
    }

    private void SetHealthUI()
    {
        healthbar.SetHealth(m_CurrentHealth, m_MaxHealth);
        // will be for the floating health bar on top of the enemy
    }

    private void OnDeath()
    {
        m_Dead = true;

        // playing death effects

        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            // TakeDamage(1f, "");
        }
    }
}
