using UnityEngine;

public class ApareceTelaInfo : MonoBehaviour
{
    [SerializeField]

    GameObject telaInfo,objetoVR;

    private void Start()
    {
        telaInfo.SetActive(false);
    }
    public void ExibirInfo()
    {
        telaInfo.SetActive(true);
        objetoVR.SetActive(false);

    }
    public void EsconderInfo()
    {
        telaInfo.SetActive(false);
        objetoVR.SetActive(true);
    }


}
