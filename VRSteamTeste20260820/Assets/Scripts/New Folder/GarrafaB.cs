using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GarrafaB : MonoBehaviour
{
    [SerializeField]
    Transform tampa;
    bool segurando = false;
    public void GarrafaPegar()
    {
        segurando = true;
    }

    public void GarrafaSoltar()
    {
        segurando = false;
    }
    public void GarrafaDestampar()
    {

        Debug.Log("bla");
        //Aqui a gente faz o que quiser!!!!
        if (segurando)
        {

            tampa.Rotate(0, 15, 0);
        }
    }


    //Tenho que desabilitar e habilitar para 
    //não dar conflito com o objeto principal
    private void Awake()
    {
        StartCoroutine(RemoveChildLista_de_GrabInteractables(tampa.gameObject));
    }

    IEnumerator RemoveChildLista_de_GrabInteractables(GameObject objeto)
    {
        objeto.SetActive(false);
        GetComponent<XRGrabInteractable>().enabled = false;
        yield return new WaitForFixedUpdate();
        objeto.SetActive(true);
        GetComponent<XRGrabInteractable>().enabled = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
