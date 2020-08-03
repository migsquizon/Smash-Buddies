using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyStats enemyStats;
    public float m_CurrentHealth;
    public bool m_Dead;

    void OnEnable()
    {
        m_CurrentHealth = enemyStats.health;
        m_Dead = false;
    }

    public void TakeDamage(float amount, string tag)
    {
        // tag can be used to determine status. for example, what happen if it takes damage while debuffed. (add multiplier etc)
        m_CurrentHealth -= amount;
        SetHealthUI();
        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            OnDeath();
        }
    }

    private void SetHealthUI()
    {
        // will be for the floating health bar on top of the enemy
    }

    private void OnDeath()
    {
        m_Dead = true;

        // playing death effects

        gameObject.SetActive(false); // can this be destroyed instead?
        Destroy(this);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player")) {
            TakeDamage(1f, "");
        }
    }
}
