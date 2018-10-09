using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Class for all logic related to keybindings.
/// </summary>
public class Keybinds {

    /// <summary>
    /// Path to the keybinds text file.
    /// </summary>
    private static readonly string KEYBINDS_PATH = "keybinds.txt";

    /// <summary>
    /// Map for keybinds.
    /// </summary>
    private Dictionary<string, KeyCode> _keybinds;

    /// <summary>
    /// Constructor. Instantiates and populates the keybinds map.
    /// </summary>
    public Keybinds() {
        _keybinds = new Dictionary<string, KeyCode>();

        // Check if there are existing keybind settings
        if (File.Exists(KEYBINDS_PATH)) {
            LoadKeybinds();
        } else {
            // Default keybinds
            _keybinds.Add("AttackKeyboard", KeyCode.Mouse0);
            _keybinds.Add("MoveKeyboard", KeyCode.Mouse1);

            _keybinds.Add("AttackController", KeyCode.JoystickButton2);
        }
    }

    /// <summary>
    /// Saves all the keybinds to a file.
    /// Format is: "action=keycode".
    /// </summary>
    public void SaveKeybinds() {
        using (StreamWriter file = new StreamWriter(KEYBINDS_PATH)) {
            foreach (string action in _keybinds.Keys) {
                file.WriteLine(action + "=" + _keybinds[action].ToString());
            }
        }
    }

    /// <summary>
    /// Loads all the keybinds from a file.
    /// </summary>
    public void LoadKeybinds() {
        // Clear the keybinds map before loading from file
        _keybinds.Clear();

        using (StreamReader file = new StreamReader(KEYBINDS_PATH)) {
            // Grab all the lines into a string array
            string[] lines = file.ReadToEnd().Split('\n');

            // Iterate through each line except the last line which will be empty
            for (int i = 0; i < lines.Length - 1; i++) {
                // Split into action and keycode
                string[] split = lines[i].Split('=');
                string action = split[0];
                KeyCode keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), split[1]);

                // Add to the keybinds map
                _keybinds.Add(action, keyCode);
            }
        }
    }

    /// <summary>
    /// Overloads the [] operator for this class to access the keybinds map.
    /// </summary>
    /// <param name="key">String key for the keybinds map</param>
    /// <returns>The KeyCode for that key if it exists</returns>
    public KeyCode this[string key] {
        get {
            return _keybinds[key];
        } set {
            _keybinds.Add(key, value);
        }
    }
}
