using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public EnemyStats enemyStats;
    public float m_FireRate = 0.3f;
    private bool hasAttacked;
    public Transform attackPoint;
    public float attackRange;
    //public Animator animator;

    IEnumerator attackCD()
    {
        yield return new WaitForSeconds(m_FireRate);
        hasAttacked = false;
        //animator.SetBool("Attack", false);
    }

    void Update()
    {
        //float moveSpeed = gameObject.GetComponent<BossHealth>().moveSpeed.speed;
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(2.421875f, 6.765625f), 0);
        foreach (Collider2D collision in hitColliders)
        {
            if (collision.gameObject.tag == "Player" && !hasAttacked)
            {
                //animator.SetBool("Attack", true);
                hasAttacked = true;
                Debug.Log("Attacking Portal");
                collision.gameObject.GetComponent<PlayerHealth>().PlayerHit();
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
                StartCoroutine(attackCD());
                //gameObject.GetComponent<BossHealth>().TakeStatus(0, moveSpeed, 1);
            }
            if (collision.gameObject.tag == "Obstacle" && !hasAttacked)
            {
                //animator.SetBool("Attack", true);
                hasAttacked = true;
                collision.gameObject.GetComponent<Obstacle>().TakeDamage(1);
                StartCoroutine(attackCD());
                //gameObject.GetComponent<BossHealth>().TakeStatus(0, moveSpeed, 1);
            }
        }
    }
}
