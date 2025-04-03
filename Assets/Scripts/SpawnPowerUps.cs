using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SpawnPowerUps : MonoBehaviour
{
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private float spawnRangeX = 7.0f;
    [SerializeField] private float spawnposY = -1.5f;
    [SerializeField] private float maxPowerUpcount = 3;

    public float powerUpSpawnInterval = 15.0f;
    public List<GameObject> powerUps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < maxPowerUpcount; i++)
        {
            GameObject powerUp = Instantiate(powerUpPrefab);
            powerUp.SetActive(false);
            powerUps.Add(powerUp);
        }
        InvokeRepeating("SpawnPowerUp", 15.0f, powerUpSpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPowerUp()
    {
        List<GameObject> inactivePowerUps = new List<GameObject>();
        foreach (GameObject powerUp in powerUps)
        {
            if (!powerUp.activeInHierarchy)
            {
                inactivePowerUps.Add(powerUp);
            }
        }
        if (inactivePowerUps.Count == 0)
        {
            return;
        }
        int randomIndex = Random.Range(0, inactivePowerUps.Count);
        GameObject selectedPowerUp = inactivePowerUps[randomIndex];
        selectedPowerUp.transform.position = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnposY, 0f);
        selectedPowerUp.SetActive(true);
    }
}
