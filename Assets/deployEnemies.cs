using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployEnemies : MonoBehaviour
{
	public GameObject enemyPrefab;
	public float respawnTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(enemyWave());
    }

    private void spawnEnemy()
    {
    	GameObject a = Instantiate(enemyPrefab) as GameObject;
    	a.transform.position = new Vector2((Random.Range(0,2)*2-1) * 6, 1.1f);
    }

    IEnumerator enemyWave() 
    {
    	while(true)
    	{
    		yield return new WaitForSeconds(respawnTime);
    		spawnEnemy();
    	}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
