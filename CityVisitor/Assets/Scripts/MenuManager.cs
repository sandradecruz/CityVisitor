using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public GameObject Menu;
	public InputActionProperty ShowMenuButton;
	public Transform Head;
	public float SpawnDistance = 2f;

	private void Update() {
		if (ShowMenuButton.action.WasPressedThisFrame()) {
			Menu.SetActive(!Menu.activeSelf);

			Vector3 headForward = new Vector3(Head.forward.x, 0, Head.forward.z).normalized;
			Vector3 spawnPosition = Head.position + headForward * SpawnDistance;
			Menu.transform.position = spawnPosition;

			Vector3 lookAtPosition = new Vector3(Head.position.x, Menu.transform.position.y, Head.position.z);
			Menu.transform.LookAt(lookAtPosition);
			Menu.transform.forward *= -1;
		}
	}

	// This method is triggered by the On Click() event of the Reset button.
	public void RestartGame() {
		string activeSceneName = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene(activeSceneName);
	}

	// This method is triggered by the On Click() event of the Quit button.
	public void QuitGame() {
		#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}

}
