using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnLoc;
    private GameObject tutorialEnemy;

    public GameObject HUDStateParent;
    // private Transform[] HUDState;
    private GameObject tutorial_1_HUD;
    private GameObject tutorial_2_HUD;
    private GameObject tutorial_3_HUD;
    private GameObject tutorial_4_HUD;
    private GameObject tutorial_5_HUD;
    private GameObject tutorial_6_HUD;


    public int state = 1;
    public bool enemyIsDead = false;
    public bool enemyChasePortal = false;

    private GameObject tutorial_1_target;
    private GameObject tutorial_2_target;
    private GameObject MainPortal;


    void Awake()
    {
        // tutorial HUD selectors
        tutorial_1_HUD = GameObject.Find("Tutorial_1_Movement");
        tutorial_2_HUD = GameObject.Find("Tutorial_2_Jumping");
        tutorial_3_HUD = GameObject.Find("Tutorial_3_Shooting");
        tutorial_4_HUD = GameObject.Find("Tutorial_4_Skill");
        tutorial_5_HUD = GameObject.Find("Tutorial_5_Summon");
        tutorial_6_HUD = GameObject.Find("Tutorial_6_BuildTower");

        MainPortal = GameObject.Find("Portal Destination");

        // trials selector
        tutorial_1_target = GameObject.Find("Tutorial_1");
        tutorial_2_target = GameObject.Find("Tutorial_2");

        // activate tutorial_1
        tutorial_1_target.gameObject.SetActive(true);
        tutorial_1_HUD.gameObject.SetActive(true);
    }

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
            if (enemyIsDead)
            {
                // summon enemy
                SpawnEnemy();
                enemyIsDead = false;
            }
            StartCoroutine(RotatingTutorial());
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
    }
}
