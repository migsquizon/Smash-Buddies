using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehaviour : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;

    public void SetHealth(float health, float maxHealth) {
        // slider.gameObject.SetActive(health < maxHealth);
        slider.gameObject.SetActive(true);
        slider.minValue = 0;
        slider.value = health;
        slider.maxValue = maxHealth;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
        // slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.green;

    }

    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + new Vector3(0, 0.8f, 0));
    }
}
