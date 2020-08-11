using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenShake : MonoBehaviour
{
    private float shakeTimeRemaining, shakePower;
    private string sceneName = "";
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    private void LateUpdate()
    {
        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;
            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0);
        }
        if (shakeTimeRemaining < 0.01 && sceneName == "Round 1")
        {
            transform.position = new Vector3(-2, -0.47f, -10);
        }
        else if (shakeTimeRemaining < 0.01 && sceneName == "Round 2")
        {
            transform.position = new Vector3(-2, 0.5f, -10);
        }

    }

    public void StartShake(float length, float power)
    {
        shakeTimeRemaining = length;
        shakePower = power;
    }
}
