using JetBrains.Annotations;
using UnityEngine;

public class Basura : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(this.gameObject); 
            GameManager.Instance.AddTrash(1);
        }
        else if ( collision.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
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


