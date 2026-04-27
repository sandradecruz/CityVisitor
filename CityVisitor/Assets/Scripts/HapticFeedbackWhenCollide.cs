using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedbackWhenCollide : MonoBehaviour {

	private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable _grabInteractable;
	private UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor _currentInteractor;

	[Range(0, 1)]
	public float Intensity;
	public float Duration;

	private void Start() {
		var _grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

		if (_grabInteractable != null) {
			_grabInteractable.selectEntered.AddListener(OnGrabbed);
			_grabInteractable.selectExited.AddListener(OnReleased);
		}
	}

	private void OnGrabbed(SelectEnterEventArgs args) {
		_currentInteractor = args.interactorObject as UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor;
	}

	private void OnReleased(SelectExitEventArgs args) {
		_currentInteractor = null;
	}

	private void OnCollisionEnter(Collision other) {
		if (_currentInteractor != null && Intensity > 0 && Duration >= 0) {
			_currentInteractor.SendHapticImpulse(Intensity, Duration);
		}
	}

	private void OnDestroy() {
		if (_grabInteractable != null) {
			_grabInteractable.selectEntered.RemoveListener(OnGrabbed);
			_grabInteractable.selectExited.RemoveListener(OnReleased);
		}
	}

}