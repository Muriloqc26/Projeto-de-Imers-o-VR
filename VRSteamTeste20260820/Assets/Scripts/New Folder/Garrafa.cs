using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using System.Collections;

public class Garrafa : MonoBehaviour
{
    [SerializeField]
    private Transform tampa;

    [SerializeField]
    private GameObject porta;

    [SerializeField]
    private TMP_Text textoAcaoConcluida;

    [SerializeField]
    private float tempoAntesDoFade = 2f;

    [SerializeField]
    private float duracaoFade = 1f;

    [SerializeField]
    private string mensagemAcaoConcluida;

    [SerializeField]
    private GameObject chaveGarrafa;

    [SerializeField]
    private GerenciadorChaves gerenciadorChaves;

    [SerializeField]
    private float distanciaMaxima = 0.20f;

    [SerializeField]
    private float sensibilidadePuxada = 3.0f;

    [Header("Comportamento")]
    [SerializeField]
    private bool voltarAutomaticamente = true;

    private bool segurandoGarrafa = false;
    private bool puxandoTampa = false;

    private bool aberturaMaximaAtingida = false;

    private Transform interactorTampa;

    // Posição original da tampa, totalmente fechada.
    private Vector3 posicaoFechadaTampa;

    // Posição da mão quando começa a interação.
    private float yInicialMao;

    // Quanto a tampa já estava aberta quando
    // o usuário pressionou o Trigger.
    private float deslocamentoInicialTampa;


    private void Awake()
    {
        // Guarda a posição original da tampa.
        posicaoFechadaTampa = tampa.localPosition;
    }


    public void GarrafaPegar()
    {
        segurandoGarrafa = true;
    }


    public void GarrafaSoltar()
    {
        segurandoGarrafa = false;
    }


    public void TampaSelecionada(SelectEnterEventArgs args)
    {
        if (!segurandoGarrafa)
            return;

        puxandoTampa = true;

        interactorTampa = args.interactorObject.transform;

        Vector3 maoLocal =
            transform.InverseTransformPoint(
                interactorTampa.position
            );

        yInicialMao = maoLocal.y;

        // Guarda quanto a tampa já estava aberta
        // no momento em que começou uma nova interação.
        deslocamentoInicialTampa =
            tampa.localPosition.y -
            posicaoFechadaTampa.y;
    }


    public void TampaSolta(SelectExitEventArgs args)
    {
        puxandoTampa = false;
        interactorTampa = null;

        // Se a tampa não chegou à abertura máxima
        // e a opção estiver marcada no Inspector,
        // volta imediatamente para a posição fechada.
        if (!aberturaMaximaAtingida &&
            voltarAutomaticamente)
        {
            tampa.localPosition =
                posicaoFechadaTampa;
        }
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
            (maoLocal.y - yInicialMao)
            * sensibilidadePuxada;

        float deslocamentoTampa =
            deslocamentoInicialTampa +
            movimentoMao;

        deslocamentoTampa =
            Mathf.Clamp(
                deslocamentoTampa,
                0f,
                distanciaMaxima
            );

        Vector3 novaPosicao =
            posicaoFechadaTampa;

        novaPosicao.y += deslocamentoTampa;

        tampa.localPosition = novaPosicao;


        // Verifica se chegou à abertura máxima.
        if (!aberturaMaximaAtingida &&
            deslocamentoTampa >= distanciaMaxima)
        {
            aberturaMaximaAtingida = true;

            TampaTotalmenteAberta();
        }
    }


    private void TampaTotalmenteAberta()
    {
        // Função chamada uma única vez
        // quando a tampa chega à distância máxima.
        Debug.Log("Chave tem que sumir");
        gerenciadorChaves.AdicionarChave();
        Destroy(chaveGarrafa);
        textoAcaoConcluida.text = mensagemAcaoConcluida;
        textoAcaoConcluida.gameObject.SetActive( true );
        porta.gameObject.SetActive( false );   

        StartCoroutine(FadeTexto());
        // Depois você decide o que fazer aqui.
    }

    private IEnumerator FadeTexto()
    {
        yield return new WaitForSeconds(tempoAntesDoFade);

        float tempo = 0f;

        while (tempo < duracaoFade)
        {
            tempo += Time.deltaTime;

            float alpha = 1f - (tempo / duracaoFade);

            textoAcaoConcluida.alpha = alpha;

            yield return null;
        }

        textoAcaoConcluida.alpha = 0f;
        textoAcaoConcluida.gameObject.SetActive ( false );

    }
}