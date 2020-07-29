using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletPrefab;
    public int atkRange;



    public float duration = 5.0f;
    private float timeSinceAction = 0.0f;
    void Start()
    {
        //rb2D = GetComponent<Rigidbody2D>();
   
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 weapon;
        if (transform.rotation.y < 0)
        {
            atkRange =-5;
            weapon = transform.position - new Vector3(0.5f, 0, 0);
        }
        else
        {
            Debug.Log(transform.rotation.y);
            weapon = transform.position + new Vector3(0.5f, 0, 0);
        }
        RaycastHit2D hit = Physics2D.Raycast(weapon, new Vector2(atkRange, 0));
        //Debug.Log(hit.collider.CompareTag("Tower"));
        Debug.DrawRay(weapon, new Vector2(atkRange, 0), Color.green);
        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {

            Instantiate(bulletPrefab, weapon, transform.rotation);
        }


        timeSinceAction += Time.deltaTime;

        if (timeSinceAction > duration)
        {
            Destroy(gameObject);

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
