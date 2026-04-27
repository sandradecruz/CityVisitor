using UnityEngine;

public class SoundOnCollision : MonoBehaviour {

	private AudioSource _audioSource;

	public AudioClip AudioClip;

	private void Start() {
		_audioSource = GetComponent<AudioSource>();
	}

	private void OnCollisionEnter(Collision collision) {
		_audioSource.PlayOneShot(AudioClip);
	}

}