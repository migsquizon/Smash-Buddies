using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class DayNightManager : MonoBehaviour
{
    public Light2D sun;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Sunset());
    }

    IEnumerator Sunset()
    {
        while (sun.intensity > 0.5f)
        {
            sun.intensity -= 0.01f;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(10f);
        StartCoroutine(Sunrise());
        StopCoroutine(Sunset());

    }

    IEnumerator Sunrise()
    {
        while (sun.intensity < 1f)
        {
            sun.intensity += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(10f);
        StartCoroutine(Sunset());
        StopCoroutine(Sunrise());

    }

    // Update is called once per frame
    void Update()
    {

    }
}
