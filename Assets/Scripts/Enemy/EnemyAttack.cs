using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyStats enemyStats;
    public float m_FireRate = 1f;
    private float nextFireTime;
    private bool m_Fired;

    public void Attack(float fireRate)
    {
        if (Time.time <= nextFireTime) return;

        nextFireTime = Time.time + fireRate;
        m_Fired = true;

        // add vfx
        // add attack animation
    }
}