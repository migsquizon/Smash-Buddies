using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnLoc;
    private GameObject tutorialEnemy;

    public GameObject HUDStateParent;

    public GameObject XConnect;
    public GameObject StartStart;


    public int state = 0;
    public bool enemyIsDead = false;
    public bool enemyChasePortal = false;

    public GameObject tutorial_1_target;
    public GameObject tutorial_2_target;
    public GameObject MainPortal;


    void Update()
    {
        if (GameObject.Find("PlayerInput(Clone)"))
        {
            XConnect.gameObject.SetActive(false);
            StartStart.gameObject.SetActive(true);
        }
        if (state == 1)
        {
            if (tutorial_1_target.GetComponentInChildren<Tutorial_1>().hasCollided)
            {
                tutorial_1_target.GetComponentInChildren<Tutorial_1>().hasCollided = false;

                tutorial_1_target.gameObject.SetActive(false);
                tutorial_2_target.gameObject.SetActive(true);
                state++;
            }
        }
        else if (state == 2)
        {
            if (tutorial_2_target.GetComponentInChildren<Tutorial_1>().hasCollided)
            {
                tutorial_2_target.GetComponentInChildren<Tutorial_1>().hasCollided = false;
                tutorial_2_target.gameObject.SetActive(false);
                SpawnEnemy();
                state++;
            }
        }
        else if (state == 3)
        {
            if (!GameObject.Find("Melee(Clone)")) enemyIsDead = true;
            if (enemyIsDead)
            {
                MainPortal.gameObject.SetActive(true);
                enemyChasePortal = true;
                state++;
            }
        }
        else if (state == 4)
        {
            if (!GameObject.Find("Melee(Clone)")) enemyIsDead = true;
            if (enemyIsDead)
            {
                // summon enemy
                SpawnEnemy();
                enemyIsDead = false;
            }
        }
    }

    void SpawnEnemy()
    {
        tutorialEnemy = (GameObject)Instantiate(enemyPrefab);
        tutorialEnemy.transform.position = spawnLoc.position;
        tutorialEnemy.SetActive(true);
    }


}
