using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarScriptAgain : MonoBehaviour
{
    public void SetHealth(float health, float maxHealth)
    {
        if (health == maxHealth) transform.localScale = new Vector3(1, 0.1f, 1);
        else transform.localScale = new Vector3(health / maxHealth, 0.1f, 1);
    }
}
