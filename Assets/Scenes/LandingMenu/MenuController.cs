using UnityEngine;

using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	/// <summary>
	/// Loads the given scene by it's name
	/// </summary>
	/// <param name="sceneName">the name of the scene</param>
	public void LoadScene(string sceneName) {
		Debug.Log("Changing scene to " + sceneName);
		SceneManager.LoadScene(sceneName);
	}

	/// <summary>
	/// Quits the game
	/// </summary>
	public void QuitGame() {
		Debug.Log("Quit the game from menu");
		Application.Quit();
	}

}
