using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HoverToSelect : MonoBehaviour
{
    private XRBaseInteractor interactor;

    void Awake()
    {
        interactor = GetComponent<XRBaseInteractor>();
    }

    public void OnEnable()
    {
        interactor.hoverEntered.AddListener(OnHoverEnter);
    }

   public void OnDisable()
   {
  //     interactor.hoverExited.RemoveListener(OnHoverEnter);
   }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        Debug.Log("Vaise foelder");
        // Force the interactor to select the hovered object
        IXRSelectInteractable interactable = (IXRSelectInteractable)args.interactableObject;
        interactor.StartManualInteraction(interactable);
    }
}
