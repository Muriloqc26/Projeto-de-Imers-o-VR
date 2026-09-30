using UnityEngine;

public class ObjetoTrigger : MonoBehaviour
{
    public void Acionar()
    {
        transform.localScale *= 1.2f;

        Debug.Log("Objeto acionado pelo Trigger!");
    }
}
