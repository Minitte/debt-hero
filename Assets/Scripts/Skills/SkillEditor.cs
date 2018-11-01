using UnityEditor;
using UnityEngine;

/// <summary>
/// This is a custom editor for Skills.
/// </summary>
[CustomEditor(typeof(Skill))]
public class SkillEditor : Editor {

    /// <summary>
    /// Called when the inspector is opened or changed.
    /// </summary>
    public override void OnInspectorGUI() {
        Skill skill = target as Skill;

        SerializedProperty it = serializedObject.GetIterator();
        if (it.NextVisible(true)) {
            while (it.NextVisible(false)) { // Loop through all the skill variables
                if (it.name == "skillBehaviours") { // Special handling for the skill behaviour array
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(it.name), true);

                    // Create an editor for each skill behaviour
                    for (int i = 0; i < skill.skillBehaviours.Length; i++) {
                        if (skill.skillBehaviours[i] != null) {
                            CreateEditor(skill.skillBehaviours[i].GetComponent<SkillBehaviour>()).OnInspectorGUI();
                        }
                    }
                } else {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(it.name), true);
                    
                }
                serializedObject.ApplyModifiedProperties(); // Save changes
            }
        }
    }
}
