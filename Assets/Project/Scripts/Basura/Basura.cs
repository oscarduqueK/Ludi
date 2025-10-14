using JetBrains.Annotations;
using UnityEngine;

public class Basura : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si choca con la pelota
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject); // elimina la basura
        }
    }
}

public class Tipo1 : Basura
{

}

public class Tipo2 : Basura
{

}

public class Tipo3 : Basura
{

}


