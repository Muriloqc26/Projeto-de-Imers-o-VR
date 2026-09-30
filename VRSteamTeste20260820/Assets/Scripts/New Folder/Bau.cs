using UnityEngine;

public class Bau : MonoBehaviour
{
    [SerializeField]
    private GameObject tampaBau;

    [SerializeField]
    private float tempoTampaBau;

    [SerializeField]
    private float velocidadeAbertura;

    private bool animandoTampaBau = false;
    private float tempoRestanteBau;


    private void Awake()
    {

    }


    public void TodasAsChavesColetadas()
    {
        Debug.Log("O baú pode ser aberto!");

        // Fazer o objeto tampaBau rotacionar em Y
        tempoRestanteBau = tempoTampaBau;
        animandoTampaBau = true;
    }

    private void Update()
    {
        if (animandoTampaBau){
            AnimarTampaBau();
        }
    }

    private void AnimarTampaBau()
    {
        tampaBau.transform.Rotate(
            Vector3.up *
            velocidadeAbertura *
            Time.deltaTime
        );

        tempoRestanteBau -= Time.deltaTime;

        if (tempoRestanteBau <= 0f)
        {
            animandoTampaBau = false;
        }
    }
}