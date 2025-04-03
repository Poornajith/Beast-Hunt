using NUnit.Framework;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject normalEnemyPrefab;
    [SerializeField] private GameObject speedEnemyPrefab;
    [SerializeField] private GameObject strongEnemyPrefab;

    [SerializeField] private float enemypawnInterval = 1f;

    [SerializeField] private int maxNormalEnemies = 10;
    [SerializeField] private int maxFastEnemies = 5;
    [SerializeField] private int maxStrongEnemies = 3;

    private List<GameObject> enemyPrefabs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyPrefabs = new List<GameObject>();

        for (int i = 0; i < maxNormalEnemies; i++)
        {
            GameObject nEnemy =  Instantiate(normalEnemyPrefab);
            nEnemy.SetActive(false);
            enemyPrefabs.Add(nEnemy);
        }
        for (int i = 0; i < maxFastEnemies; i++)
        {
            GameObject spEnemy = Instantiate(speedEnemyPrefab);
            spEnemy.SetActive(false);
            enemyPrefabs.Add(spEnemy);
        }
        for (int i = 0; i < maxStrongEnemies; i++)
        {
            GameObject stEnemy = Instantiate(strongEnemyPrefab);
            stEnemy.SetActive(false);
            enemyPrefabs.Add(stEnemy);
        }
        InvokeRepeating("SpawnEnemy", 2f, enemypawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SpawnEnemy()
    {
        List<GameObject> inactiveEnemies = new List<GameObject>();

        foreach (GameObject enemy in enemyPrefabs)
        {
            if (!enemy.activeInHierarchy)
            {
                inactiveEnemies.Add(enemy);
            }
        }

        if (inactiveEnemies.Count > 0)
        {
            int randomIndex = Random.Range(0, inactiveEnemies.Count);
            GameObject enemyToSpawn = inactiveEnemies[randomIndex];
            enemyToSpawn.SetActive(true);
            enemyToSpawn.transform.position = transform.position;
            enemyToSpawn.transform.rotation = transform.rotation;
        }
    }

}
