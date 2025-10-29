using UnityEngine;

public class levelConfig
{
    public int fishToUnlock = -1;

    public virtual int GetFishId()
    {
        return -1;
    }

    public virtual void SetupLevel(trashmanagement generator)
    {
        Debug.Log("Configuración base de nivel (no definida)");
    }

    public virtual void SpawnTrash(trashmanagement generator)
    {
        Debug.Log("A");
    }
}

