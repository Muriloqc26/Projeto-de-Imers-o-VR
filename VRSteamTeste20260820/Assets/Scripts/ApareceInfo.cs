using UnityEngine;

public class ApareceInfo : MonoBehaviour
{

    [SerializeField]
    GameObject info;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        info.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ExibeInfo()
    {
        info.SetActive(true);
    }
    public void EscondeInfo()
    {
        info.SetActive(false);
    }
}
