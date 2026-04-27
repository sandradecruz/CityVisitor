using UnityEngine;

public class Sonido: MonoBehaviour
{   
    AudioSource altavoz;
    public AudioClip salto;
    public AudioClip bomba;
    
    void Start()
    {
        altavoz = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            altavoz.PlayOneShot(salto);
        }
    }
}
