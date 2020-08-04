using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehaviour : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public Color low;
    public Color high;

    public void SetHealth(float health, float maxHealth) {
        // slider.gameObject.SetActive(health < maxHealth);
        slider.gameObject.SetActive(true);
        slider.value = health;
        slider.maxValue = maxHealth;
        fill.color = Color.Lerp(low, high, slider.value);
        // slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.green;

    }

    // void Update()
    // {
    //     slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + new Vector3(0, 0.8f, 0));
    // }
}
