using UnityEngine;

public class level1 : levelConfig
{
    public override void SetupLevel(trashmanagement generator)
    {
        Debug.Log("Nivel 1 configurado");

        
    }

    public override void SpawnTrash(trashmanagement generator)
    {
        //Intervalo y verificación de funcionamiento
        if (generator.trashPrefabs.Count == 0) return;
        generator.spawnInterval = 2f;

        //Gestion de los spawnPoints
        Transform indexSpanwPoint = generator.spawnPoint[generator.spawnPointIndex]; 
        generator.spawnPointIndex = Random.Range(0, generator.spawnPoint.Count); 

        //Gestión de los Prefabs 
        int index = Random.Range(0, generator.trashPrefabs.Count);
        GameObject trash = Object.Instantiate(generator.trashPrefabs[index], indexSpanwPoint.position, Quaternion.identity);
    }
}
