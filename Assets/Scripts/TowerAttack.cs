using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    public int atkRange;
    void Start()
    {
        //rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.rotation.y < 0) atkRange *= -1;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(atkRange, 0));
        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {

            Instantiate(bulletPrefab, transform.position, transform.rotation);


        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Tower")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

    }
}
