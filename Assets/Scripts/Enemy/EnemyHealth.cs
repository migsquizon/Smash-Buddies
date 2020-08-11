using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float m_CurrentHealth;
    public float m_MaxHealth;
    public bool m_Dead;
    public HealthbarScriptAgain healthbar2;
    public EnemyAI moveSpeed;
    bool kennaDmg = false;
    public int inAoe = 0;
    void OnEnable()
    {
        m_Dead = false;
        healthbar2.SetHealth(m_CurrentHealth, m_MaxHealth);
        // Debug.Log(moveSpeed.speed);
    }
    void Update()
    {
        while (inAoe > 0 && !kennaDmg)
        {
            TakeDamage(inAoe);
            kennaDmg = true;
            StartCoroutine(aoedmgcd());
        }
    }
    public void TakeDamage(float amount)
    {
        // tag can be used to determine status. for example, what happen if it takes damage while debuffed. (add multiplier etc)
        m_CurrentHealth -= amount;
        healthbar2.SetHealth(m_CurrentHealth, m_MaxHealth);

        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            StartCoroutine(DeathFX());
        }
    }

    public void TakeStatus(float damageOverTime, float slow, int duration)
    {

        //curSpeed = moveSpeed.speed;
        StartCoroutine(statusDuration(duration, damageOverTime, slow));
    }
    IEnumerator statusDuration(int dur, float dot, float slow)
    {
        //Print the time of when the function is first called.
        //Debug.Log("status duration started at : " + Time.time);
        moveSpeed.speed -= slow;
        for (float i = 0; i < dur; i++)
        {
            TakeDamage(dot);
            yield return new WaitForSeconds(1.0f);

        }
        moveSpeed.speed += slow;
        //yield on a new YieldInstruction that waits for 5 seconds.

        //RoninActive = false;
        //After we have waited 5 seconds print the time again.
        //  Debug.Log("Status duration ended at : " + Time.time);

    }
    IEnumerator aoedmgcd()
    {
        //Print the time of when the function is first called.
        yield return new WaitForSeconds(1.0f);
        kennaDmg = false;
    }

    IEnumerator DeathFX()
    {
        GetComponent<AudioSource>().Play();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
        yield return new WaitForSeconds(1.0f);
        m_Dead = true;
        Destroy(gameObject);
    }
}
