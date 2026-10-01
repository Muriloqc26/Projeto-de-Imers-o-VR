using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Caixa : MonoBehaviour
{
    [SerializeField]
    private GameObject caixaTampa;

    [SerializeField]
    private float tempoTampa = 2f;

    [SerializeField]
    private float velocidadeAbertura = 90f;

    private bool animandoTampa = false;
    private float tempoRestanteTampa;
    private bool caixaAberta = false;

    //Chave Subir
    [SerializeField]
    private GameObject chaveCaixa;

    [SerializeField]
    private float tempoChave = 2f;

    [SerializeField]
    private float velocidadeSubida = 0.5f;

    [SerializeField]
    private float velocidadeRotacao = 180f;

    private bool animandoChave = false;
    private float tempoRestanteChave;

    //Texto
    [SerializeField]
    private TMP_Text textoAcaoConcluida;

    [SerializeField]
    private float tempoAntesDoFade = 2f;

    [SerializeField]
    private float duracaoFade = 1f;

    [SerializeField]
    private string mensagemAcaoConcluida;

    //Voltar para posição
    [SerializeField]
    private Transform pontoRetornoCaixa;

    private Rigidbody rb;

    //Porta
    [SerializeField]
    private GameObject porta;

    [SerializeField]
    private float tempoPorta = 2f;

    [SerializeField]
    private float velocidadePorta = 45f;

    private bool animandoPorta = false;
    private float tempoRestantePorta;

    //Som de Abertura
    [SerializeField]
    private AudioSource somPegarCaixa;
    [SerializeField]
    private AudioSource somPorta;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void CaixaSolta()
    {
        Debug.Log("A caixa foi solta!");

        StartCoroutine(RetornarCaixa());
    }

    public void TampaSelecionada(SelectEnterEventArgs args)
    {
        if (caixaAberta)
            return;

        caixaAberta = true;

        Debug.Log("Tampa selecionada!");

        tempoRestanteTampa = tempoTampa;
        animandoTampa = true;

        tempoRestanteChave = tempoChave;
        animandoChave = true;

        tempoRestantePorta = tempoPorta;
        animandoPorta = true;

        somPorta.Play();

        textoAcaoConcluida.alpha = 1f;
        textoAcaoConcluida.text = mensagemAcaoConcluida;
        textoAcaoConcluida.gameObject.SetActive(true);

        StartCoroutine(FadeTexto());
    }

    private void Update()
    {
        if (animandoTampa)
        {
            AnimarTampa();
        }

        if (animandoChave)
        {
            AnimarChave();
        }

        if (animandoPorta)
        {
            AnimarPorta();
        }
    }

    private void AnimarTampa()
    {
        caixaTampa.transform.Rotate(
            Vector3.up *
            velocidadeAbertura *
            Time.deltaTime
        );

        tempoRestanteTampa -= Time.deltaTime;

        if (tempoRestanteTampa <= 0f)
        {
            animandoTampa = false;
        }
    }

    private void AnimarChave()
    {
        chaveCaixa.transform.position +=
            Vector3.up *
            velocidadeSubida *
            Time.deltaTime;

        chaveCaixa.transform.Rotate(
            Vector3.forward *
            velocidadeRotacao *
            Time.deltaTime
        );

        tempoRestanteChave -= Time.deltaTime;

        if (tempoRestanteChave <= 0f)
        {
            animandoChave = false;
        }
    }

    private void AnimarPorta()
    {
        porta.transform.Rotate(
            Vector3.forward *
            velocidadePorta *
            Time.deltaTime
        );

        tempoRestantePorta -= Time.deltaTime;

        if (tempoRestantePorta <= 0f)
        {
            animandoPorta = false;
        }
    }

    private IEnumerator FadeTexto()
    {
        yield return new WaitForSeconds(tempoAntesDoFade);

        float tempo = 0f;

        while (tempo < duracaoFade)
        {
            tempo += Time.deltaTime;

            float alpha =
                1f - (tempo / duracaoFade);

            textoAcaoConcluida.alpha = alpha;

            yield return null;
        }

        textoAcaoConcluida.alpha = 0f;
        textoAcaoConcluida.gameObject.SetActive(false);
    }

    private IEnumerator RetornarCaixa()
    {
        yield return new WaitForFixedUpdate();

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.position = pontoRetornoCaixa.position;
        rb.rotation = pontoRetornoCaixa.rotation;

        Debug.Log("Caixa reposicionada!");
    }

    public void CaixaPega()
{
    somPegarCaixa.Play();
}
}