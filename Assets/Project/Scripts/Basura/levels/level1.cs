using UnityEngine;

public class level1 : levelConfig
{
    public override void SetupLevel(trashmanagement generator)
    {
        Debug.Log("Nivel 1 configurado");
    }

    public override int GetFishId()
    {
        return 0;
    }
    public override void SpawnTrash(trashmanagement generator)
    {

        GameManager.Instance.requiredTrashToWin = 15;

        if (generator.trashPrefabs.Count == 0) return;
        generator.spawnInterval = 2.5f;

        generator.spawnPointIndex = Random.Range(2, generator.spawnPoint.Count -2);
        Transform indexSpanwPoint = generator.spawnPoint[generator.spawnPointIndex]; 

        int index = Random.Range(0, generator.trashPrefabs.Count);
        GameObject trash = Object.Instantiate(generator.trashPrefabs[index], indexSpanwPoint.position, Quaternion.identity);
    }
}
