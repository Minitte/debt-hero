using UnityEngine;
using UnityEngine.UI;

public class FloatingTextController : MonoBehaviour {

    /// <summary>
    /// Singleton object for FloatingTextController.
    /// </summary>
    public static FloatingTextController instance;

    /// <summary>
    /// Reference to the damage text prefab.
    /// </summary>
    public GameObject damageText;

    /// <summary>
    /// An offset to the damage text's position.
    /// </summary>
    public Vector3 textOffset = new Vector3(0f, 0f, 0f);

    /// <summary>
    /// Reference to the game's camera.
    /// </summary>
    private Camera _camera;

    /// <summary>
    /// Reference to the game's UI canvas.
    /// </summary>
    private GameObject _canvas;

    // Use this for initialization
    private void Start() {
        instance = this;
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _canvas = GameObject.Find("Canvas");
    }

    /// <summary>
    /// Creates a text object on the canvas showing a damage taken number.
    /// </summary>
    /// <param name="netDamage">The net amount of damage taken</param>
    /// <param name="victim">The gameobject that took damage</param>
    public void CreateDamageNumber(float netDamage, GameObject victim) {
        GameObject textObject = Instantiate(damageText, transform);
        textObject.transform.position = _camera.WorldToScreenPoint(victim.transform.position + textOffset);

        textObject.GetComponent<Text>().text = netDamage.ToString();
    }
}
