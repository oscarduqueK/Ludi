using System.Collections.Generic;
using UnityEngine;

public class trashmanagement : MonoBehaviour
{

    public List<GameObject> trashPrefabs; 
    public float spawnInterval;
    public Transform spawnPoint;

    private levelConfig currentLevel; 
    private float timer;

    void Start()
    {
        currentLevel = new level1();
        currentLevel.SetupLevel(this);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnTrash();
            timer = 0f;
        }
    }

    void SpawnTrash()
    {
        if (trashPrefabs.Count == 0) return;

        int index = Random.Range(0, trashPrefabs.Count);
        GameObject trash = Instantiate(trashPrefabs[index], spawnPoint.position, Quaternion.identity);
    }
}

