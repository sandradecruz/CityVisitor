using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class PlayerSpeedController : MonoBehaviour {

	public Slider SpeedSlider;
	public TextMeshProUGUI SpeedValueText;
	public ContinuousMoveProviderBase MoveProvider;

	private void Start() {
		if (SpeedSlider == null || SpeedValueText == null || MoveProvider == null) {
			return;
		}

		float speed = MoveProvider.moveSpeed;
		SpeedSlider.value = speed;
		UpdateSpeedValueText(speed);

		SpeedSlider.onValueChanged.AddListener(OnSliderValueChanged);
	}

	private void OnSliderValueChanged(float newSpeed) {
		MoveProvider.moveSpeed = newSpeed;
		UpdateSpeedValueText(newSpeed);
	}

	private void UpdateSpeedValueText(float value) {
		SpeedValueText.text = $"{value:F2}";
	}

	private void OnDestroy() {
		SpeedSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
	}

}
