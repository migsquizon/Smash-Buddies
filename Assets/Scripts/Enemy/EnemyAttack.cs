using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyStats enemyStats;
    public float m_FireRate = 0.3f;
    private bool hasAttacked;
    public Transform attackPoint;
    public int attackRange;
    public Animator animator;

    public Transform launchoffset;
    public GameObject rangedProjectile;


    IEnumerator attackCD()
    {
        yield return new WaitForSeconds(m_FireRate);
        hasAttacked = false;
        animator.SetBool("Attack", false);
    }

    void Update()
    {
        float moveSpeed = gameObject.GetComponent<EnemyHealth>().moveSpeed.speed;
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2((attackRange - 0.3f) * 1f, 2f), 0);
        foreach (Collider2D collision in hitColliders)
        {
            if (collision.gameObject.tag == "Player" && !hasAttacked)
            {
                animator.SetBool("Attack", true);
                hasAttacked = true;
                Debug.Log("Attacking player");
                if (attackRange > 1)
                {
                    GameObject fire = Instantiate(rangedProjectile, launchoffset.position, launchoffset.rotation);
                    gameObject.GetComponent<EnemyHealth>().TakeStatus(0, moveSpeed, 2);
                }
                else
                {
                    //fire.GetComponent<Fire>().AtkRange = attackRange;
                    collision.gameObject.GetComponent<PlayerHealth>().PlayerHit();
                    collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
                }
                StartCoroutine(attackCD());
            }
            if (collision.gameObject.tag == "Obstacle" && !hasAttacked)
            {
                animator.SetBool("Attack", true);
                hasAttacked = true;
                Debug.Log("Attacking obstacle");
                if (attackRange > 1)
                {
                    GameObject fire = Instantiate(rangedProjectile, launchoffset.position, launchoffset.rotation);
                    gameObject.GetComponent<EnemyHealth>().TakeStatus(0, moveSpeed, 2);
                }
                else
                {
                    //fire.GetComponent<Fire>().AtkRange = attackRange;
                    collision.gameObject.GetComponent<Obstacle>().TakeDamage(1);
                }
                //fire.GetComponent<Fire>().AtkRange = attackRange;

                StartCoroutine(attackCD());
                gameObject.GetComponent<EnemyHealth>().TakeStatus(0, moveSpeed, 2);
            }
        }
    }
}