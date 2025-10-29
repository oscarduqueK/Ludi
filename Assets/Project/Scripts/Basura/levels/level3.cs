using UnityEngine;

public class level4 : levelConfig
{
    public override void SetupLevel(trashmanagement generator)
    {
        Debug.Log("Nivel 3 configurado");
    }

    public override int GetFishId()
    {
        return 3;
    }

    public override void SpawnTrash(trashmanagement generator)
    {
        GameManager.Instance.requiredTrashToWin = 10;

        if (generator.trashPrefabs.Count == 0) return;
        generator.spawnInterval = 3f;

        Transform indexSpanwPoint = generator.spawnPoint[generator.spawnPointIndex];
        generator.spawnPointIndex = Random.Range(0, generator.spawnPoint.Count);

        int index = Random.Range(0, generator.trashPrefabs.Count);
        GameObject trash = Object.Instantiate(generator.trashPrefabs[index], indexSpanwPoint.position, Quaternion.identity);
    }
}