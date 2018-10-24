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
        serializedObject.Update();

        // Basic properties of the skill
        EditorGUILayout.LabelField("Basics", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        skill.skillName = EditorGUILayout.TextField("Skill Name:", skill.skillName);
        skill.skillIcon = (Sprite)EditorGUILayout.ObjectField("Skill Icon:", skill.skillIcon, typeof(Sprite), allowSceneObjects: true);
        skill.skillDescription = EditorGUILayout.TextField("Skill Description:", skill.skillDescription);
        skill.skillType = (Skill.SkillType)EditorGUILayout.EnumPopup("Skill Type:", skill.skillType);
        EditorGUILayout.Space();

        // Costs of the skill
        EditorGUILayout.LabelField("Costs", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        skill.cooldown = EditorGUILayout.FloatField("Cooldown:", skill.cooldown);
        skill.manaCost = EditorGUILayout.FloatField("Mana Cost:", skill.manaCost);
        EditorGUILayout.Space();

        // Combat properties of the skill
        EditorGUILayout.LabelField("Combat", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        skill.physicalMultiplier = EditorGUILayout.FloatField("Physical Multiplier:", skill.physicalMultiplier);
        skill.magicMultiplier = EditorGUILayout.FloatField("Magic Multiplier:", skill.magicMultiplier);
        skill.areaRadius = EditorGUILayout.FloatField("Area Radius:", skill.areaRadius);
        skill.healing = EditorGUILayout.FloatField("Healing Amount:", skill.healing);
        EditorGUILayout.Space();

        // Behaviour prefabs attached to the skill
        EditorGUILayout.LabelField("Behaviour", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("skillBehaviours"), true);
        serializedObject.ApplyModifiedProperties(); // Apply changes
    }
    
}
