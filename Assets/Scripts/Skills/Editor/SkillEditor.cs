using UnityEditor;
using UnityEngine;

/// <summary>
/// This is a custom editor for Skills.
/// </summary>
[CustomEditor(typeof(Skill))]
public class SkillEditor : Editor {

    /// <summary>
    /// Enum for behaviour type.
    /// </summary>
    public enum BehaviourType { Instant, OnDamage };

    /// <summary>
    /// List of instant behaviours
    /// </summary>
    private readonly string[] instantBehaviours = { "MoveCaster", "BuffStat", "Heal" };

    /// <summary>
    /// List of on damage behaviours.
    /// </summary>
    private readonly string[] damageBehaviours = { "DamageOverTime", "Knockback" };

    /// <summary>
    /// List of all skill behaviour classes as strings.
    /// </summary>
    private string[] options;

    /// <summary>
    /// The currently selected behaviour type.
    /// </summary>
    private BehaviourType behaviourType;

    /// <summary>
    /// The currently selected behaviour type.
    /// </summary>
    int selectedOption;

    /// <summary>
    /// Called when the editor for a skill is opened.
    /// Used for initialization.
    /// </summary>
    public void Awake() {
        behaviourType = BehaviourType.Instant;
        options = instantBehaviours;
    }

    /// <summary>
    /// Called when the inspector is opened or changed.
    /// </summary>
    public override void OnInspectorGUI() {
        Skill skill = target as Skill;

        // Iterate through all of the skill variables
        SerializedProperty it = serializedObject.GetIterator();
        if (it.NextVisible(true)) {
            while (it.NextVisible(false)) { // Loop through all the skill variables
                if (it.name == "instantBehaviours") { // Special handling for the instant behaviours list
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                    EditorGUILayout.LabelField("Instant Behaviours", EditorStyles.boldLabel);

                    // Create an editor for each skill behaviour
                    for (int i = 0; i < skill.instantBehaviours.Count; i++) {
                        if (i != 0) {
                            DrawHorizontalLine(Color.gray);
                        }
                        if (skill.instantBehaviours[i] != null) {
                            CreateEditor(skill.instantBehaviours[i]).OnInspectorGUI();

                            // Button for removing this behaviour
                            if (GUILayout.Button("Remove behaviour", EditorStyles.miniButton)) {
                                DestroyImmediate(skill.instantBehaviours[i], true);
                                skill.instantBehaviours.RemoveAt(i);
                            }
                        }
                    }

                } else if (it.name == "damageBehaviours") { // Special handling for the damage behaviours list
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                    EditorGUILayout.LabelField("Damage Behaviours", EditorStyles.boldLabel);

                    // Create an editor for each skill behaviour
                    for (int i = 0; i < skill.damageBehaviours.Count; i++) {
                        if (i != 0) {
                            DrawHorizontalLine(Color.gray);
                        }
                        if (skill.damageBehaviours[i] != null) {
                            CreateEditor(skill.damageBehaviours[i]).OnInspectorGUI();

                            // Button for removing this behaviour
                            if (GUILayout.Button("Remove behaviour", EditorStyles.miniButton)) {
                                DestroyImmediate(skill.damageBehaviours[i], true);
                                skill.damageBehaviours.RemoveAt(i);
                            }
                        }
                    }
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                } else {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(it.name), true); // Default inspector
                }
            }
        }

        // Section for adding new behaviours
        EditorGUILayout.LabelField("Add a new behaviour", EditorStyles.boldLabel);
        behaviourType = (BehaviourType)EditorGUILayout.EnumPopup("Behaviour Type:", behaviourType);
        UpdateBehaviourSelections();
        selectedOption = EditorGUILayout.Popup("Behaviour: ", selectedOption, options);

        // Button for adding a new behaviour
        if (GUILayout.Button("Add new behaviour")) {
            if (behaviourType == BehaviourType.Instant) {
                // Add it to the list of behaviours
                InstantBehaviour behaviour = CreateInstance(options[selectedOption]) as InstantBehaviour;
                AssetDatabase.AddObjectToAsset(behaviour, skill);
                skill.instantBehaviours.Add(behaviour);
            } else {
                // Add it to the list of behaviours
                DamageBehaviour behaviour = CreateInstance(options[selectedOption]) as DamageBehaviour;
                AssetDatabase.AddObjectToAsset(behaviour, skill);
                skill.damageBehaviours.Add(behaviour);
            }
        }

        // Save changes
        serializedObject.ApplyModifiedProperties();
    }

    /// <summary>
    /// Draws a horizontal line.
    /// </summary>
    /// <param name="color">Color of the line</param>
    /// <param name="thickness">Thickness of the line, default is 2</param>
    /// <param name="padding">Amount of padding for the line, default is 10</param>
    private void DrawHorizontalLine(Color color, int thickness = 2, int padding = 10) {
        Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
        r.height = thickness;
        r.y += padding / 2;
        r.x -= 2;
        r.width += 6;
        EditorGUI.DrawRect(r, color);
    }


    /// <summary>
    /// Updates the behaviour selections.
    /// </summary>
    private void UpdateBehaviourSelections() {
        if (behaviourType == BehaviourType.Instant) {
            options = instantBehaviours;
        } else {
            options = damageBehaviours;
        }
    }
}

