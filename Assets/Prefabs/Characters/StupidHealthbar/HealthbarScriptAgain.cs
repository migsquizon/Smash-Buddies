using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarScriptAgain : MonoBehaviour
{
    void Start()
    {
        transform.localScale = new Vector3(0, 0.1f, 1);
    }
    public void SetHealth(float health, float maxHealth)
    {
        if (health < 0) health = 0;
        transform.localScale = new Vector3(health / maxHealth, 0.1f, 1);

    }
}
