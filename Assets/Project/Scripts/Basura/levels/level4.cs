using UnityEngine;

public class level3 : levelConfig
{
    public override void SetupLevel(trashmanagement generator)
    {
        Debug.Log("Nivel 3 configurado");
    }

    public override int GetFishId()
    {
        return 2;
    }

    public override void SpawnTrash(trashmanagement generator)
    {
        GameManager.Instance.requiredTrashToWin = 8;

        if (generator.trashPrefabs.Count == 0) return;
        generator.spawnInterval = 3f;

        Transform indexSpanwPoint = generator.spawnPoint[generator.spawnPointIndex];
        generator.spawnPointIndex = Random.Range(1, generator.spawnPoint.Count -1);

        int index = Random.Range(0, generator.trashPrefabs.Count);
        GameObject trash = Object.Instantiate(generator.trashPrefabs[index], indexSpanwPoint.position, Quaternion.identity);
    }
}