using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator animator;

    private int levelToLoad;

    private float LevelTimer;



    // Update is called once per frame
    void Update()
    {

        LevelTimer += Time.deltaTime;
        //Debug.Log(LevelTimer);
        if (LevelTimer > 10.0f && SceneManager.GetActiveScene().buildIndex==0)
        {
            FadeToNextLevel();
        }
    }

    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
