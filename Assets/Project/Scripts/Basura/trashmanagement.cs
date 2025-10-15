using System.Collections.Generic;
using UnityEngine;

public class trashmanagement : MonoBehaviour
{
    public List<GameObject> trashPrefabs; 
    public float spawnInterval;
    public List <Transform> spawnPoint;

    private levelConfig currentLevel; 
    private float timer;

    public int spawnIndex;

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
            currentLevel.SpawnTrash(this);
            timer = 0f;
        }
    }

}

