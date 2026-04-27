using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioClip audioClip;
    public GameObject boton;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void reproducirSonido()
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position);

    }  
}
