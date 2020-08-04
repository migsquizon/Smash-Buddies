using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public int currentWave = 0;
    private GameObject[] respawns;


    public int interval;

    public int numberOfWaves = 3;

    public bool inCombat = false;

    private static WaveManager _instance;
    public static WaveManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public static void spawnEnemies(int big, int mini)
    {
        //Debug.Log("Spawning Mini Wave");
        List<List<GameObject>> miniWaves = EnemySpawner.SharedInstance.GetMiniWave(big, mini);
        List<Vector3> miniWavesLocation = EnemySpawner.SharedInstance.GetSpawnLocation(big, mini);
        //List<GameObject> enemies = miniWaves[0];
        for (int i = 0; i < 4; i++)
        {
            foreach (GameObject enemy in miniWaves[i])
            {
                enemy.transform.position = miniWavesLocation[i];
                enemy.SetActive(true);
            }
        }
    }


    //public  void spawnWave(int curr)
    //{
    //    //for(int i=0;i<numb)
    //    //Debug.Log(EnemySpawner.SharedInstance.waves.Count);
    //    for(int i=0;i< EnemySpawner.SharedInstance.waves[curr].miniWaves.Count; i++)
    //    {
    //        Debug.Log(i);
    //        spawnEnemies(curr, i);
    //        StartCoroutine(WaitForNextInterval());
    //        // start interval here.

    //    }
       

    //}


    private IEnumerator spawnMiniwaves(int curr)
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        WaitForSeconds wait = new WaitForSeconds(interval);
        for (int i = 0; i < EnemySpawner.SharedInstance.waves[curr].miniWaves.Count; i++)
        {
            Debug.Log("Finished interval : " + Time.time);
            spawnEnemies(curr, i);
            //if (i == EnemySpawner.SharedInstance.waves[curr].miniWaves.Count - 1) inCombat = false;
            yield return wait;
           
           
        }
        Debug.Log("Finished miniwave : " + Time.time);
        inCombat = false;
    }


    private void spawnWave()
    {
        if (!inCombat)
        {
            StartCoroutine(spawnMiniwaves(currentWave));
            currentWave += 1;
            inCombat = true;
        }
    }

    // ...



    //private IEnumerator WaitForNextInterval()
    //{
    //    Debug.Log("Started Coroutine at timestamp : " + Time.time);

    //    //yield on a new YieldInstruction that waits for 5 seconds.
    //    yield return new WaitForSeconds(interval);

    //    //After we have waited 5 seconds print the time again.
    //    Debug.Log("Finished Coroutine at timestamp : " + Time.time);
   

    //}


    void Start()
    {
        //rb2D = GetComponent<Rigidbody2D>();
        spawnWave();
    }


    void Update()
    {
        respawns = GameObject.FindGameObjectsWithTag("Enemy");

        //if (!inCombat)
        //{
        //    if (respawns.Length == 0)
        //    {
        //        spawnWave();
        //    }
        //}
        //else
        //{
        if (currentWave == 2)
        {

            if (respawns.Length == 0)
            {
                LevelManager levelchanger = GameObject.Find("LevelChanger").GetComponent<LevelManager>();
                levelchanger.FadeToNextLevel();
                return;
            }


        }
        else
        {
            if (respawns.Length == 0)
            {
                spawnWave();
            }
        }
    }

    
        //    if (currentWave == 2)
        //    {

        //        if (respawns.Length == 0)
        //        {
        //            LevelManager levelchanger = GameObject.Find("LevelChanger").GetComponent<LevelManager>();
        //            levelchanger.FadeToNextLevel();
        //            return;
        //        }


        //    }
        //}
    


}
