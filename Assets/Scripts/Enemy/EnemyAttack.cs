using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float m_FireRate = 0.3f;
    private bool hasAttacked;
    public Transform attackPoint;
    public int attackRange;
    public Animator animator;

    public Transform launchoffset;
    public GameObject rangedProjectile;
    public Transform playerpos;
    public AudioSource attackSound;


    IEnumerator attackCD()
    {
        yield return new WaitForSeconds(m_FireRate);
        hasAttacked = false;
        animator.SetBool("Attack", false);
    }

    void Start()
    {
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
                attackSound.Play();
                hasAttacked = true;
                // Debug.Log("Attacking player");
                if (attackRange > 1)
                {
                    playerpos = collision.gameObject.transform;
                    GameObject fire = Instantiate(rangedProjectile, launchoffset.position, launchoffset.rotation);
                    fire.GetComponent<Rigidbody2D>().velocity = (playerpos.position - launchoffset.position).normalized * 6f;

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
                attackSound.Play();
                hasAttacked = true;
                //Debug.Log("Attacking obstacle");
                if (attackRange > 1)
                {
                    GameObject fire = Instantiate(rangedProjectile, launchoffset.position, launchoffset.rotation);
                    fire.GetComponent<Rigidbody2D>().velocity = (collision.gameObject.transform.position - launchoffset.position).normalized * 6f;
                    gameObject.GetComponent<EnemyHealth>().TakeStatus(0, moveSpeed, 2);
                }
                else
                {
                    //fire.GetComponent<Fire>().AtkRange = attackRange;
                    collision.gameObject.GetComponent<Obstacle>().TakeDamage(1);
                    gameObject.GetComponent<EnemyHealth>().TakeStatus(0, moveSpeed, 2);

                }
                //fire.GetComponent<Fire>().AtkRange = attackRange;

                StartCoroutine(attackCD());
            }
        }
    }
}