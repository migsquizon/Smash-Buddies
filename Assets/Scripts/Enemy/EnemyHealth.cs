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
    public EnemyAI moveSpeed;

    void OnEnable()
    {
        m_MaxHealth = enemyStats.health;
        m_CurrentHealth = enemyStats.health;
        m_Dead = false;
        healthbar.SetHealth(m_CurrentHealth, m_MaxHealth);
        // Debug.Log(moveSpeed.speed);
    }

    public void TakeDamage(float amount)
    {
        // tag can be used to determine status. for example, what happen if it takes damage while debuffed. (add multiplier etc)
        m_CurrentHealth -= amount;
        healthbar.SetHealth(m_CurrentHealth, m_MaxHealth);

        if (m_CurrentHealth <= 0f && !m_Dead) OnDeath();
    }

    public void TakeStatus(float damageOverTime, float slow, int duration){

        //curSpeed = moveSpeed.speed;
        StartCoroutine(statusDuration(duration, damageOverTime, slow));
    }
     IEnumerator statusDuration(int dur,float dot, float slow)
    {
        //Print the time of when the function is first called.
        Debug.Log("status duration started at : " + Time.time);
        moveSpeed.speed -= slow;
        for (float i = 0; i < dur; i++)
        {
            TakeDamage(dot);
            Debug.Log("DOT Taken"+ dot);
            yield return new WaitForSeconds(1.0f);

        }
        moveSpeed.speed += slow;
        //yield on a new YieldInstruction that waits for 5 seconds.
        
        //RoninActive = false;
        //After we have waited 5 seconds print the time again.
        Debug.Log("Status duration ended at : " + Time.time);
    
    }

    private void OnDeath()
    {
        m_Dead = true;

        // playing death effects

        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
