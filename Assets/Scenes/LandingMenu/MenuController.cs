using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    /// <summary>
    /// Used to load the game.
    /// </summary>
    public GameObject saveSlotPanel;
    public void Start() {
        SoundManager.instance.PlayMusic(0);
    }
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

    public void OpenLoadSlots() {
        saveSlotPanel.SetActive(true);
        
    }

}
