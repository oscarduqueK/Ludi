using System.Collections.Generic;
using UnityEngine;

public class trashmanagement : MonoBehaviour
{
    public List<GameObject> trashPrefabs;

    [HideInInspector]
    public float spawnInterval;

    public List <Transform> spawnPoint;

    public levelConfig currentLevel; //Cambiar más adelante, está público para pruebas

    [Range(1, 10)] public int LevelIndexInspector; //para seleccionar desde el inspector

    [HideInInspector]
    public int spawnPointIndex; 

    private float timer;

    void Start()
    {
        currentLevel = AssociateLevel(LevelIndexInspector); //se crea automáticamente
        if (currentLevel != null) currentLevel.SetupLevel(this);
        else Debug.LogError("No existe configuración para el nivel {LevelIndexInspector}");
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

    levelConfig AssociateLevel (int number)
    {
        switch (number)
        {
            case 1: return new level1();
            case 2: return new level2();
            case 3: return new level3();

            default: return null;
        }
    }
}

