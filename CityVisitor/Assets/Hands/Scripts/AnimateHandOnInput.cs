using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour {

	public Animator HandAnimator;
	public InputActionProperty PinchAnimationAction;
	public InputActionProperty GripAnimationAction;

	private void Update() {
		float triggerValue = PinchAnimationAction.action.ReadValue<float>();
		HandAnimator.SetFloat("Trigger", triggerValue);

		float gripValue = GripAnimationAction.action.ReadValue<float>();
		HandAnimator.SetFloat("Grip", gripValue);
	}

}