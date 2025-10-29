using UnityEngine;

public class level1 : levelConfig
{
    public override void SetupLevel(trashmanagement generator)
    {
        Debug.Log("Nivel 1 configurado");
    }

    public override int GetFishId()
    {
        Debug.Log("Niga");
        return 0;
    }
    public override void SpawnTrash(trashmanagement generator)
    {
        //Basura necesaria para ganar
        GameManager.Instance.requiredTrashToWin = 1;

        //Intervalo y verificación de funcionamiento
        if (generator.trashPrefabs.Count == 0) return;
        generator.spawnInterval = 2.5f;

        //Gestion de los spawnPoints
        generator.spawnPointIndex = Random.Range(2, generator.spawnPoint.Count -2);
        Transform indexSpanwPoint = generator.spawnPoint[generator.spawnPointIndex]; 

        //Gestión de los Prefabs 
        int index = Random.Range(0, generator.trashPrefabs.Count);
        GameObject trash = Object.Instantiate(generator.trashPrefabs[index], indexSpanwPoint.position, Quaternion.identity);
    }
}
