using UnityEngine;

public class levelConfig
{


    public virtual void SetupLevel(trashmanagement generator)
    {
        Debug.Log("Configuración base de nivel (no definida)");
    }

    public virtual void SpawnTrash(trashmanagement generator)
    {
        Debug.Log("A");
    }
}
