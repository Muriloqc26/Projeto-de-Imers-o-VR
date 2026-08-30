using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TeamHoverFilter : MonoBehaviour, IXRHoverFilter
{
    // Determines if the filter is currently active
    public bool canProcess => isActiveAndEnabled;

    public bool Process(IXRHoverInteractor interactor, IXRHoverInteractable interactable)
    {

        Debug.Log("fois");
        // Add your custom logic here
        // Example: Only allow hovering if the player has a specific component or state
        if (interactor.transform.CompareTag("PlayerTeamA"))
        {
            return true; // Hover allowed
        }

        return false; // Hover blocked
    }
}
