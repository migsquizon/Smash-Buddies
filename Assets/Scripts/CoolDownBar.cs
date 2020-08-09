using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxTime(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetTime(float health)
    {
        slider.value = health;
    }
}