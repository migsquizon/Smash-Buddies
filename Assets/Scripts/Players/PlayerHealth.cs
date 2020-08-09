using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject[] hearts;
    private int life;
    public bool dead;
    private bool isHit = false;
    private int nextHit = 2;

    private void Start()
    {
        life = hearts.Length;
    }

    public void TakeDamage(int damagePoint)
    {
        if (life >= 1)
        {
            Debug.Log("Damage Taken");
            life -= damagePoint;
            hearts[life].SetActive(false);
            if (life < 1)
            {
                OnDeath();
                Debug.Log("<<<DEAD>>>");
            }
        }
    }

    public void OnDeath()
    {
        dead = true;
        GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(WaitForPlayerToRevive());
    }

    public void Heal()
    {
        if (life < 3)
        {
            life += 1;
            hearts[life - 1].SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Enemy" && !isHit)
        {
            Debug.Log("Collide with enemy. Player takes damage");
            PlayerHit();
            TakeDamage(1);

        };


    }


    public void PlayerHit()
    {
        // If we are already stun, quit; we don't want to be hit again when we are stun
        if (isHit)
        {
            Debug.Log("player is stunned, cannot stun again");
            return;
        }

        //DamageTaken();
        isHit = true;


        StartCoroutine(WaitForPlayerToRecover());
    }

    private IEnumerator WaitForPlayerToRecover()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(nextHit);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        isHit = false;
    }

    private IEnumerator WaitForPlayerToRevive()
    {
        yield return new WaitForSeconds(5);
        dead = false;
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().MovePosition(new Vector2(0, 0));
        Heal();
        Heal();
        Heal();
        transform.position = new Vector2(0, 20);
    }
}
