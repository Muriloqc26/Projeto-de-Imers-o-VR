using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ApareceTelaInfoControles : MonoBehaviour
{
    [SerializeField]
    InputActionReference rotacao;
    [SerializeField]
    GameObject quemRotacionar;
    InputAction controleRotacao;
    bool rotacionar = false;

    private void Awake()
    {
        Debug.Log("info awake");
        controleRotacao=rotacao.action;
        
    }
    private void OnEnable()
    {
        Debug.Log("info enable");
        controleRotacao.performed += OnStartRotacao;
        controleRotacao.canceled += OnCancelRotacao;
        controleRotacao.Enable();
    }

    private void OnCancelRotacao(InputAction.CallbackContext context)
    {
        
       rotacionar=false;
        Debug.Log(rotacionar);
    }

    private void OnStartRotacao(InputAction.CallbackContext context)
    {
        rotacionar = true;
        Debug.Log(rotacionar);
    }

    private void OnDisable()
    {
        controleRotacao.performed -= OnStartRotacao;
        controleRotacao.canceled -= OnCancelRotacao;
        controleRotacao.Disable();
    }
    private void Update()
    {
        if(rotacionar)
             quemRotacionar.transform.Rotate(0, 45 * Time.deltaTime, 0);
    }
}
