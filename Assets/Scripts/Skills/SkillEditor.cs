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
        EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
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

        // Damage properties of the skill
        EditorGUILayout.LabelField("Damage", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.ObjectField(serializedObject.FindProperty("damagePrefab"));
        serializedObject.ApplyModifiedProperties();
        skill.physicalMultiplier = EditorGUILayout.FloatField("Physical Multiplier:", skill.physicalMultiplier);
        skill.magicMultiplier = EditorGUILayout.FloatField("Magic Multiplier:", skill.magicMultiplier);
        skill.rangeMultiplier = EditorGUILayout.FloatField("Range Multiplier:", skill.magicMultiplier);
        skill.areaMultiplier = EditorGUILayout.FloatField("Area Multiplier:", skill.magicMultiplier);
        EditorGUILayout.Space();

        // Healing properties of the skill
        EditorGUILayout.LabelField("Healing", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.ObjectField(serializedObject.FindProperty("healingPrefab"));
        serializedObject.ApplyModifiedProperties();
        skill.healing = EditorGUILayout.FloatField("Heal Amount:", skill.healing);
        skill.healingRadius = EditorGUILayout.FloatField("Heal Radius:", skill.healingRadius);
        EditorGUILayout.Space();

        // Status effects of the skill
        EditorGUILayout.LabelField("Status Effect", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        skill.hasStatusEffects = EditorGUILayout.Toggle("Has status effects:", skill.hasStatusEffects);

        if (skill.hasStatusEffects) {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("statusEffects"), true);
            serializedObject.ApplyModifiedProperties(); // Apply changes
        }
    }
}
