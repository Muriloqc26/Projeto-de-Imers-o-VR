using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class Gaveta : MonoBehaviour
{
    [SerializeField]
    private float distanciaMaxima = 0.40f;

    [SerializeField]
    private float sensibilidadePuxada = 1.0f;

    [SerializeField]
    private TMP_Text textoAcaoConcluida;

    [SerializeField]
    private float tempoAntesDoFade = 2f;

    [SerializeField]
    private float duracaoFade = 1f;

    [SerializeField]
    private string mensagemAcaoConcluida;

    [SerializeField]
    private GameObject chaveGaveta;

    [SerializeField]
    private GerenciadorChaves gerenciadorChaves;

    [SerializeField]
    private float tempoChave = 2f;

    [SerializeField]
    private float velocidadeSubida = 0.5f;

    [SerializeField]
    private float velocidadeRotacao = 180f;

    private bool animandoChave = false;
    private float tempoRestanteChave;

    [Header("Comportamento")]
    [SerializeField]
    private bool voltarAutomaticamente = false;

    private bool puxandoGaveta = false;

    private Transform interactorGaveta;

    // Posição original da gaveta, totalmente fechada.
    private Vector3 posicaoFechadaGaveta;

    // Indica se a gaveta já atingiu a distância máxima.
    private bool distanciaMaximaAtingida = false;

    // Posição da mão quando começa a interação.
    private float zInicialMao;

    // Posição da gaveta quando começa a interação.
    private float zInicialGaveta;


    private void Awake()
    {
        // Guarda a posição original da gaveta.
        posicaoFechadaGaveta = transform.localPosition;
    }


    public void GavetaSelecionada(SelectEnterEventArgs args)
    {
        puxandoGaveta = true;

        interactorGaveta = args.interactorObject.transform;

        Vector3 maoLocal =
            transform.parent.InverseTransformPoint(
                interactorGaveta.position
            );

        // Guarda a posição da mão no momento da seleção.
        zInicialMao = maoLocal.z;

        // Guarda a posição atual da gaveta no momento da seleção.
        zInicialGaveta = transform.localPosition.z;
    }


    public void GavetaSolta(SelectExitEventArgs args)
    {
        puxandoGaveta = false;
        interactorGaveta = null;

        if (voltarAutomaticamente)
        {
            transform.localPosition =
                posicaoFechadaGaveta;
        }
    }


    private void Update()
    {
        // =====================================================
        // MOVIMENTO DA GAVETA
        // =====================================================

        if (puxandoGaveta && interactorGaveta != null)
        {
            Vector3 maoLocal =
                transform.parent.InverseTransformPoint(
                    interactorGaveta.position
                );

            float movimentoMao =
                zInicialMao - maoLocal.z;

            float novaPosicaoZ =
                zInicialGaveta -
                movimentoMao * sensibilidadePuxada;

            // Calcula o limite máximo de abertura.
            float limiteZ =
                posicaoFechadaGaveta.z -
                distanciaMaxima;

            // Impede a gaveta de ultrapassar os limites.
            novaPosicaoZ =
                Mathf.Clamp(
                    novaPosicaoZ,
                    limiteZ,
                    posicaoFechadaGaveta.z
                );

            Vector3 novaPosicao =
                transform.localPosition;

            novaPosicao.z = novaPosicaoZ;

            transform.localPosition = novaPosicao;


            // Verifica se a gaveta chegou à abertura máxima.
            if (!distanciaMaximaAtingida &&
                novaPosicaoZ <= limiteZ)
            {
                distanciaMaximaAtingida = true;

                GavetaTotalAberta();
            }
        }


        // =====================================================
        // ANIMAÇÃO DA CHAVE
        // =====================================================

        if (animandoChave)
        {
            AnimarChave();
        }
    }


    public void GavetaTotalAberta()
    {
        Debug.Log("Gaveta totalmente aberta!");

        // Inicia a animação da chave.
        tempoRestanteChave = tempoChave;
        animandoChave = true;

        // Mostra a mensagem.
        textoAcaoConcluida.text =
            mensagemAcaoConcluida;

        textoAcaoConcluida.gameObject.SetActive(true);

        StartCoroutine(FadeTexto());
    }


    private void AnimarChave()
    {
        // Faz a chave subir no eixo Y.
        chaveGaveta.transform.position +=
            Vector3.up *
            velocidadeSubida *
            Time.deltaTime;

        // Faz a chave girar no eixo Z.
        chaveGaveta.transform.Rotate(
            Vector3.forward *
            velocidadeRotacao *
            Time.deltaTime
        );

        // Diminui o tempo restante da animação.
        tempoRestanteChave -=
            Time.deltaTime;

        // Quando o tempo termina, destrói a chave.
        if (tempoRestanteChave <= 0f)
        {
            animandoChave = false;

            gerenciadorChaves.AdicionarChave();

            Destroy(chaveGaveta);
        }
    }


    private IEnumerator FadeTexto()
    {
        yield return new WaitForSeconds(
            tempoAntesDoFade
        );

        float tempo = 0f;

        while (tempo < duracaoFade)
        {
            tempo += Time.deltaTime;

            float alpha =
                1f -
                (tempo / duracaoFade);

            textoAcaoConcluida.alpha =
                alpha;

            yield return null;
        }

        textoAcaoConcluida.alpha = 0f;

        textoAcaoConcluida.gameObject.SetActive(
            false
        );
    }
}