using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{

    public int currentWave = 0;
    private GameObject[] respawns;


    public int interval;

    public int numberOfWaves = 2;

    public bool inCombat = false;

    public GameObject preparationScreen;
    public TextMeshPro preparationText;


    public TextMeshPro powerupText;


    public bool stillGotEnemy = false;

    private PersistentManager persistentManager;


    private CoinManager coinManager;

    private PortalHealth portalHealth;

    public int preparation = 15;

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
        // DontDestroyOnLoad(this.gameObject);
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
                enemy.transform.localScale = new Vector3(1.8f, 2.0f, 1f);
                enemy.transform.position = miniWavesLocation[i];
                enemy.SetActive(true);
            }
        }
    }

    public void showPreparationScreen()
    {
        preparationScreen.SetActive(true);
    }

    public void hidePreparationScreen()
    {
        preparationScreen.SetActive(true);
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
        // Debug.Log("Started Coroutine at timestamp : " + Time.time);
        WaitForSeconds wait = new WaitForSeconds(interval);

        for (int i = 0; i < EnemySpawner.SharedInstance.waves[curr].miniWaves.Count; i++)
        {
            // Debug.Log("Finished interval : " + Time.time);
            spawnEnemies(curr, i);
            if (i == EnemySpawner.SharedInstance.waves[curr].miniWaves.Count - 1) break;
            yield return wait;


        }
        inCombat = false;
        // respawns = GameObject.FindGameObjectsWithTag("Enemy");
        // Debug.Log("Started preparation : " + Time.time);
        // can implement reward talent points here
        // preparationText.SetText("Preparation Phase");
        // if(persistentManager!=null)persistentManager.talentPoints += 1;
        // yield return prep; // Preperation duration

        // Debug.Log("Finished preparation : " + Time.time);
    }



    private IEnumerator startRound(int curr)
    {
        WaitForSeconds prep = new WaitForSeconds(preparation);

        preparationText.SetText("Preparation Phase");
        if (persistentManager != null) persistentManager.talentPoints += 4;
        Debug.Log(225 * (int)(portalHealth.currentHealth / portalHealth.maxHealth));
        if (coinManager != null && portalHealth != null) coinManager.coin += (int)(portalHealth.currentHealth * 225 / portalHealth.maxHealth);
        yield return prep;    //Wait for preparation

        spawnWave(curr);



    }

    private void begin()
    {

        if (!inCombat)
        {
            StartCoroutine(startRound(currentWave));
            currentWave += 1;
            inCombat = true;
        }

    }


    private void spawnWave(int curr)
    {
        preparationText.SetText("Combat Phase");
        preparationScreen.SetActive(true);
        StartCoroutine(spawnMiniwaves(curr));


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
        persistentManager = GameObject.Find("PersistentManager").GetComponent<PersistentManager>();
        coinManager = GameObject.Find("CoinManager").GetComponent<CoinManager>();
        portalHealth = GameObject.Find("Portal Destination").GetComponent<PortalHealth>();
        begin();
    }


    void Update()
    {
        powerupText.SetText(persistentManager.talentPoints.ToString());
        respawns = GameObject.FindGameObjectsWithTag("Enemy");
        if (respawns.Length == 0) stillGotEnemy = false;
        else stillGotEnemy = true;

        if (currentWave == numberOfWaves + 1)
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
            if (!stillGotEnemy)
            {
                begin();
            }
        }
    }





}