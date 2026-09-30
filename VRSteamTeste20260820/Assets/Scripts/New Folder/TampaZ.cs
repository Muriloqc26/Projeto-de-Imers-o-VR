using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TampaZ : MonoBehaviour
{
    [SerializeField]
    private Transform tampa;

    [SerializeField]
    private GameObject chaveCaixa;

    [SerializeField]
    private GerenciadorChaves gerenciadorChaves;

    [SerializeField]
    private float distanciaMaxima = 0.20f;

    [SerializeField]
    private float sensibilidadePuxada = 3.0f;

    private bool puxandoTampa = false;
    private Transform interactorTampa;

    private Vector3 posicaoFechadaTampa;
    private float zInicialMao;

    private void Awake()
    {
        posicaoFechadaTampa = tampa.localPosition;
    }

    public void TampaSelecionada(SelectEnterEventArgs args)
    {
        puxandoTampa = true;

        interactorTampa = args.interactorObject.transform;

        Vector3 maoLocal =
            transform.InverseTransformPoint(
                interactorTampa.position
            );

        zInicialMao = maoLocal.z;
    }

    public void TampaSolta(SelectExitEventArgs args)
    {
        puxandoTampa = false;
        interactorTampa = null;
    }

    private void Update()
    {
        if (!puxandoTampa || interactorTampa == null)
            return;

        Vector3 maoLocal =
            transform.InverseTransformPoint(
                interactorTampa.position
            );

        float movimentoMao =
            (maoLocal.z - zInicialMao)
            * sensibilidadePuxada;

        movimentoMao =
            Mathf.Clamp(
                movimentoMao,
                0f,
                distanciaMaxima
            );

        Vector3 novaPosicao =
            posicaoFechadaTampa;

        novaPosicao.z += movimentoMao;

        tampa.localPosition = novaPosicao;

        gerenciadorChaves.AdicionarChave();

        Destroy(chaveCaixa);

    }
}