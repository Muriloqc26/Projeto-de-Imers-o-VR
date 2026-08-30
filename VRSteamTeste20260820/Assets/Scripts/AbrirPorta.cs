using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class AbrirPorta : MonoBehaviour
{

    XRSimpleInteractable interactable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHoverEnter()
    {
        interactable.selectEntered.Invoke(null) ;
        Debug.Log("Entrou a");
    }
    public void OnHoverExit()
    {
        
        Debug.Log("Saiu a");
    }

    public void OnClickPorta()
    {
        Debug.Log("cliquei na porta!!!!");
    }
}
