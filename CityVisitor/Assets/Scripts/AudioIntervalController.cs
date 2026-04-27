using UnityEngine;

public class AudioIntervalController : MonoBehaviour {

	private AudioSource _audioSource;
	private float _timer;

	public AudioClip AudioClip;
	public float RepeatInterval = 5f;

	private void Start() {
		_audioSource = GetComponent<AudioSource>();
		_timer = RepeatInterval;
	}

	private void Update() {
		_timer -= Time.deltaTime;

		if (_timer <= 0f) {
			_audioSource.PlayOneShot(AudioClip);
			_timer = RepeatInterval;
		}
	}

}