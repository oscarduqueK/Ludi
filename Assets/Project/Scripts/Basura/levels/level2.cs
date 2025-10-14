using UnityEngine;

public class level2 : levelConfig
{
    public override void SetupLevel(trashmanagement generator)
    {
        Debug.Log("Nivel 1 configurado");

        generator.spawnInterval = 1f;

    }
}