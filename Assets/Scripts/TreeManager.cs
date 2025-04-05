using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    [SerializeField] private GameObject[] treePrefabs;
    [SerializeField] private int treeCount;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float spawnLocationX;
    [SerializeField] private float spawnLocationY;
    [SerializeField] private float spawnLocationLowerZPositive;
    [SerializeField] private float spawnLocationUpperZPositive;
    [SerializeField] private float spawnLocationLowerZNegative;
    [SerializeField] private float spawnLocationUpperZNegative;

    private List<GameObject> trees;
    private List<GameObject> inactiveTrees;
    private void Awake()
    {
        trees = new List<GameObject>();
        inactiveTrees = new List<GameObject>();
    }

    private void Start()
    {
        // Instantiate the initial number of trees randomly
        for (int i = 0; i < treeCount; i++)
        {
            for (int j = 0; j < treePrefabs.Length; j++)
            {
                GameObject prefab = treePrefabs[j];
                createTree(prefab);
            }
        }
        InvokeRepeating("SpawnTree", 2f, spawnInterval);
    }

    private void SpawnTree()
    {
        GameObject selectedTree = GetRandomInactiveTree();
        selectedTree.transform.position = GetRandomSpawnPosition();
        selectedTree.SetActive(true);
    }

    private void createTree(GameObject prefab)
    {

        if (prefab != null)
        {
            GameObject tree = Instantiate(prefab);
            trees.Add(tree);
            tree.SetActive(false); // Initially keep them inactive
            inactiveTrees.Add(tree); // Add to the inactive list
        }
    }


    private Vector3 GetRandomSpawnPosition()
    {
        float randomZ = spawnLocationZ();
        return new Vector3(spawnLocationX, spawnLocationY, randomZ);
    }

    private float spawnLocationZ()
    {
        int randomSide = Random.Range(0, 2);
        if (randomSide == 0)
        {
            return Random.Range(spawnLocationLowerZPositive, spawnLocationUpperZPositive);
        }
        else
        {
            return Random.Range(spawnLocationLowerZNegative, spawnLocationUpperZNegative);
        }
    }

    private GameObject GetRandomInactiveTree()
    {
        if (inactiveTrees.Count == 0)
        {
            foreach (GameObject tree in trees)
            {
                if (!tree.activeInHierarchy)
                {
                    inactiveTrees.Add(tree);
                }
            }
            GetRandomInactiveTree();
        }
        int randomIndex = Random.Range(0, inactiveTrees.Count);
        GameObject selectedTree = inactiveTrees[randomIndex];
        inactiveTrees.RemoveAt(randomIndex); // Remove from inactive list
        return selectedTree;
    }
}
