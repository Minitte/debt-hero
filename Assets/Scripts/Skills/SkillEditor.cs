using UnityEditor;
using UnityEngine;

/// <summary>
/// This is a custom editor for Skills.
/// </summary>
[CustomEditor(typeof(Skill))]
public class SkillEditor : Editor {

    /// <summary>
    /// List of all skill behaviour classes as strings.
    /// </summary>
    private string[] options;

    /// <summary>
    /// The currently selected behaviour type.
    /// </summary>
    int selectedOption;

    /// <summary>
    /// Called when the editor for a skill is opened.
    /// Used for initialization.
    /// </summary>
    public void Awake() {
        options = new string[] {
            "Heal", "Test"
        };
    }

    /// <summary>
    /// Called when the inspector is opened or changed.
    /// </summary>
    public override void OnInspectorGUI() {
        Skill skill = target as Skill;

        SerializedProperty it = serializedObject.GetIterator();
        if (it.NextVisible(true)) {
            while (it.NextVisible(false)) { // Loop through all the skill variables
                // Special handling for the skill behaviour array
                if (it.name == "skillBehaviours") { 
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(it.name), true);

                    // Create an editor for each skill behaviour
                    for (int i = 0; i < skill.skillBehaviours.Count; i++) {
                        if (skill.skillBehaviours[i] != null) {
                            //CreateEditor(skill.skillBehaviours[i]).OnInspectorGUI();
                        }
                    }
                } else {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(it.name), true);
                }
                // Save changes
                serializedObject.ApplyModifiedProperties();
            }
        }
        // Section for adding new behaviours
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        selectedOption = EditorGUILayout.Popup("Behaviour Type: ", selectedOption, options);

        // Check if the add behaviour button was clicked
        if (GUILayout.Button("Add Behaviour")) { 
            // Add it to the list of behaviours
            SkillBehaviour behaviour = CreateInstance(options[selectedOption]) as SkillBehaviour;
            AssetDatabase.AddObjectToAsset(behaviour, skill);
            skill.skillBehaviours.Add(behaviour);
        }
    }
}
