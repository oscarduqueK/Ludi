using UnityEngine;

public class level1 : levelConfig
{
    public override void SetupLevel(trashmanagement generator)
    {
        Debug.Log("Nivel 1 configurado");

        generator.spawnInterval = 2f;

    }
}
