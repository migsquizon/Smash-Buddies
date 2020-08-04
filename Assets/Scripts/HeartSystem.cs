using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystem : MonoBehaviour
{
    public GameObject[] hearts;
    private int life;
    private bool dead;

    private void Start()
    {
        life = hearts.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damagePoint)
    {
        if (life >= 1)
        {
            Debug.Log("Damage Taken");
            life -= damagePoint;
            Destroy(hearts[life].gameObject);
            if (life < 1)
            {
                dead = true;
                Debug.Log("<<<DEAD>>>");
            }
        }
    }
}
