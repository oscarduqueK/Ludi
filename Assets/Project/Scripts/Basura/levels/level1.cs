using UnityEngine;

public class level1 : levelConfig
{
    public override void SetupLevel(trashmanagement generator)
    {
        Debug.Log("Nivel 1 configurado");

        
    }

    public override void SpawnTrash(trashmanagement generator)
    {
        if (generator.trashPrefabs.Count == 0) return;

        Transform indexSpanwPoint = generator.spawnPoint[generator.spawnIndex];

        int index = Random.Range(0, generator.trashPrefabs.Count);
        GameObject trash = Object.Instantiate(generator.trashPrefabs[index], indexSpanwPoint.position, Quaternion.identity);

        generator.spawnInterval = 2f;
        generator.spawnIndex = Random.Range(0, generator.spawnPoint.Count);
    }
}
