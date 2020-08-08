using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnLoc;
    private GameObject tutorialEnemy;

    public GameObject HUDStateParent;
    public GameObject tutorial_1_HUD;
    public GameObject tutorial_2_HUD;
    public GameObject tutorial_3_HUD;
    public GameObject tutorial_4_HUD;
    public GameObject tutorial_5_HUD;
    public GameObject tutorial_6_HUD;


    public int state = 1;
    public bool enemyIsDead = false;
    public bool enemyChasePortal = false;
    private bool startCoroutine = false;

    public GameObject tutorial_1_target;
    public GameObject tutorial_2_target;
    public GameObject MainPortal;

    void Update()
    {
        if (state == 1)
        {
            if (tutorial_1_target.GetComponentInChildren<Tutorial_1>().hasCollided)
            {
                tutorial_1_target.GetComponentInChildren<Tutorial_1>().hasCollided = false;

                tutorial_1_target.gameObject.SetActive(false);
                tutorial_1_HUD.gameObject.SetActive(false);
                tutorial_2_target.gameObject.SetActive(true);
                tutorial_2_HUD.gameObject.SetActive(true);
                state++;
            }
        }
        else if (state == 2)
        {
            if (tutorial_2_target.GetComponentInChildren<Tutorial_1>().hasCollided)
            {
                tutorial_2_target.GetComponentInChildren<Tutorial_1>().hasCollided = false;
                tutorial_2_target.gameObject.SetActive(false);
                tutorial_2_HUD.gameObject.SetActive(false);
                tutorial_3_HUD.gameObject.SetActive(true);

                SpawnEnemy();
                state++;
            }
        }
        else if (state == 3)
        {
            if (!GameObject.Find("Melee(Clone)")) enemyIsDead = true;
            if (enemyIsDead)
            {
                tutorial_3_HUD.gameObject.SetActive(false);
                tutorial_4_HUD.gameObject.SetActive(true);
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
            if (!startCoroutine)
            {
                StartCoroutine(RotatingTutorial());
                startCoroutine = true;
            }
        }
    }

    void SpawnEnemy()
    {
        tutorialEnemy = (GameObject)Instantiate(enemyPrefab);
        tutorialEnemy.transform.position = spawnLoc.position;
        tutorialEnemy.SetActive(true);
    }

    IEnumerator RotatingTutorial()
    {
        tutorial_4_HUD.gameObject.SetActive(false);
        tutorial_5_HUD.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        tutorial_5_HUD.gameObject.SetActive(false);
        tutorial_6_HUD.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        tutorial_6_HUD.gameObject.SetActive(false);
        tutorial_4_HUD.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        StartCoroutine(RotatingTutorial());
    }
}
