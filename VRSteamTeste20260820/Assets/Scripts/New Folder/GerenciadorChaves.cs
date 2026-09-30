using UnityEngine;

public class GerenciadorChaves : MonoBehaviour
{
    [SerializeField]
    private int chavesNecessarias = 2;

    [SerializeField]
    private Bau bau;

    private int quantidadeChaves = 0;

    public void AdicionarChave()
    {
        quantidadeChaves++;

        Debug.Log("Chaves coletadas: " + quantidadeChaves);

        if (quantidadeChaves >= chavesNecessarias)
        {
            TodasAsChavesColetadas();
        }
    }

    private void TodasAsChavesColetadas()
    {
        Debug.Log("Todas as chaves foram coletadas!");

        bau.TodasAsChavesColetadas();
    }
}