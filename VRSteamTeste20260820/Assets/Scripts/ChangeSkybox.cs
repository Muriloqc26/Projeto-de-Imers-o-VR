using UnityEngine;

public class ChangeSkybox : MonoBehaviour
{
    [SerializeField] private Material skybox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        RenderSettings.skybox = skybox;


        
    }
}
