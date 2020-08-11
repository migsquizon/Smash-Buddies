using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public float timeRemaining = 10;
    TextMeshPro textMeshPro;
    bool timerIsRunning = false;

    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
        textMeshPro = GetComponent<TextMeshPro>();
        DisplayTime(timeRemaining);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                if (timeRemaining > 60)
                {
                    timeRemaining -= Time.deltaTime;
                    DisplayTime(timeRemaining);
                }
                else
                {
                    timeRemaining -= Time.deltaTime;
                    textMeshPro.color = new Color(255, 0, 0);
                    textMeshPro.SetText(System.Math.Round(timeRemaining, 2).ToString());
                }

            }
            else
            {
                timerIsRunning = false;
                Debug.Log("Timer Ended!!");
                timeRemaining = 0;
                //textMeshPro.SetText(timeRemaining.ToString());
                DisplayTime(timeRemaining);
                gameOverScreen.SetActive(true);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        textMeshPro.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }
}
