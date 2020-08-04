using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawningEnemy
{
    public int amount;
    public GameObject enemyPrefab;
}

[System.Serializable]
public class MiniWave
{
    public List<SpawningEnemy> enemyTypesInLane1;
    public Transform spawnLocation1;
    public List<SpawningEnemy> enemyTypesInLane2;
    public Transform spawnLocation2;
    public List<SpawningEnemy> enemyTypesInLane3;
    public Transform spawnLocation3;
    public List<SpawningEnemy> enemyTypesInLane4;
    public Transform spawnLocation4;
}

[System.Serializable]
public class Wave
{
    public List<MiniWave> miniWaves;
}

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner SharedInstance;
    public int nextWave = 0;
    public int nextMiniWave = 0;
    public List<Wave> waves;

    void Awake() // Run only once when first instantiated.
    {
        SharedInstance = this; // Instantiate a static EnemySpawner that is accessible from anywhere
    }

    public List<List<GameObject>> GetMiniWave(int wave_i, int miniWave_i)
    {
        List<GameObject> lane1Enemy = new List<GameObject>();
        List<GameObject> lane2Enemy = new List<GameObject>();
        List<GameObject> lane3Enemy = new List<GameObject>();
        List<GameObject> lane4Enemy = new List<GameObject>();

        if (waves[wave_i].miniWaves[miniWave_i].enemyTypesInLane1.Count > 0)
        {
            for (int i = 0; i < waves[wave_i].miniWaves[miniWave_i].enemyTypesInLane1.Count; i++)
            { // Looping through enemy type to spawn. e.g, if miniWave 1 has [2 melee, 1 range], it will loop the melee type first, followed by the range type
                for (int _ = 0; _ < waves[wave_i].miniWaves[miniWave_i].enemyTypesInLane1[i].amount; _++)
                { // it will then loop through the number of 'type' to spawn
                    GameObject enemy = (GameObject)Instantiate(waves[wave_i].miniWaves[miniWave_i].enemyTypesInLane1[i].enemyPrefab);
                    enemy.SetActive(false);
                    lane1Enemy.Add(enemy); // adding the enemyprefab gameobject into the pool
                }
            }
        }
        if (waves[wave_i].miniWaves[miniWave_i].enemyTypesInLane2.Count > 0)
        {
            for (int i = 0; i < waves[wave_i].miniWaves[miniWave_i].enemyTypesInLane2.Count; i++)
            { // Looping through enemy type to spawn. e.g, if miniWave 1 has [2 melee, 1 range], it will loop the melee type first, followed by the range type
                for (int _ = 0; _ < waves[wave_i].miniWaves[miniWave_i].enemyTypesInLane2[i].amount; _++)
                { // it will then loop through the number of 'type' to spawn
                    GameObject enemy = (GameObject)Instantiate(waves[wave_i].miniWaves[miniWave_i].enemyTypesInLane2[i].enemyPrefab);
                    enemy.SetActive(false);
                    lane2Enemy.Add(enemy); // adding the enemyprefab gameobject into the pool
                }
            }
        }
        if (waves[wave_i].miniWaves[miniWave_i].enemyTypesInLane3.Count > 0)
        {
            for (int i = 0; i < waves[wave_i].miniWaves[miniWave_i].enemyTypesInLane3.Count; i++)
            { // Looping through enemy type to spawn. e.g, if miniWave 1 has [2 melee, 1 range], it will loop the melee type first, followed by the range type
                for (int _ = 0; _ < waves[wave_i].miniWaves[miniWave_i].enemyTypesInLane3[i].amount; _++)
                { // it will then loop through the number of 'type' to spawn
                    GameObject enemy = (GameObject)Instantiate(waves[wave_i].miniWaves[miniWave_i].enemyTypesInLane3[i].enemyPrefab);
                    enemy.SetActive(false);
                    lane3Enemy.Add(enemy); // adding the enemyprefab gameobject into the pool
                }
            }
        }
        if (waves[wave_i].miniWaves[miniWave_i].enemyTypesInLane4.Count > 0)
        {
            for (int i = 0; i < waves[wave_i].miniWaves[miniWave_i].enemyTypesInLane4.Count; i++)
            { // Looping through enemy type to spawn. e.g, if miniWave 1 has [2 melee, 1 range], it will loop the melee type first, followed by the range type
                for (int _ = 0; _ < waves[wave_i].miniWaves[miniWave_i].enemyTypesInLane4[i].amount; _++)
                { // it will then loop through the number of 'type' to spawn
                    GameObject enemy = (GameObject)Instantiate(waves[wave_i].miniWaves[miniWave_i].enemyTypesInLane4[i].enemyPrefab);
                    enemy.SetActive(false);
                    lane4Enemy.Add(enemy); // adding the enemyprefab gameobject into the pool
                }
            }
        }

        List<List<GameObject>> enemyPool = new List<List<GameObject>>()
        {
            lane1Enemy,lane2Enemy,lane3Enemy,lane4Enemy
        };

        return enemyPool;
    }

    public List<Vector3> GetSpawnLocation(int wave_i, int miniWave_i)
    {
        List<Vector3> enemySpawnLocations = new List<Vector3>()
        {
            waves[wave_i].miniWaves[miniWave_i].spawnLocation1.position,
            waves[wave_i].miniWaves[miniWave_i].spawnLocation2.position,
            waves[wave_i].miniWaves[miniWave_i].spawnLocation3.position,
            waves[wave_i].miniWaves[miniWave_i].spawnLocation4.position,
        };
        return enemySpawnLocations;
    }
}
