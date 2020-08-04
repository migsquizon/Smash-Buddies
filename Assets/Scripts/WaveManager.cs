using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
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

    public static void spawnMiniWave()
    {
        List<List<GameObject>> miniWaves = EnemySpawner.SharedInstance.GetMiniWave(0, 0);
        List<Vector3> miniWavesLocation = EnemySpawner.SharedInstance.GetSpawnLocation(0, 0);
        List<GameObject> enemies = miniWaves[0];
        for (int i = 0; i < 4; i++)
        {
            foreach (GameObject enemy in miniWaves[i])
            {
                enemy.transform.position = miniWavesLocation[i];
                enemy.SetActive(true);
            }
        }
    }

    void Start()
    {
        WaveManager.spawnMiniWave();

    }
}
