using System.Collections.Generic;
using UnityEngine;

public class trashmanagement : MonoBehaviour
{
    public List<GameObject> trashPrefabs;
    public float spawnInterval;
    public List<Transform> spawnPoint;

    public levelConfig currentLevel;
    public int spawnPointIndex;

    private float timer;

    void Start()
    {
        int levelToLoad = PlayerPrefs.GetInt("LevelToLoad", 1);

        currentLevel = AssociateLevel(levelToLoad);
        if (currentLevel != null)
            currentLevel.SetupLevel(this);
        else
            Debug.LogError($"No existe configuración para el nivel {levelToLoad}");
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

    levelConfig AssociateLevel(int number)
    {
        switch (number)
        {
            case 1: return new level1();
            case 2: return new level2();
            case 3: return new level3();
            case 4: return new level4();

            //case 4: return new level4();
            //case 5: return new level5();
            //case 6: return new level6();
            //case 7: return new level7();
            //case 8: return new level8();
            //case 9: return new level9();
            //case 10: return new level10();
            default: return null;
        }
    }
}
