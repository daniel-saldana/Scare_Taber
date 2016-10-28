using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SonarFx)), CanEditMultipleObjects]
public class SonarFxEditor : Editor
{
    SerializedProperty propMode;
    SerializedProperty propOrigin;
    SerializedProperty propDirection;
    SerializedProperty propBaseColor;
    SerializedProperty propWaveColor;
    SerializedProperty propWaveAmplitude;
    SerializedProperty propWaveExponent;
    SerializedProperty propWaveInterval;
    SerializedProperty propWaveSpeed;
    SerializedProperty propAddColor;

    void OnEnable()
    {
        propMode          = serializedObject.FindProperty("_mode");
        propOrigin        = serializedObject.FindProperty("_origin");
        propDirection     = serializedObject.FindProperty("_direction");
        propBaseColor     = serializedObject.FindProperty("_baseColor");
        propWaveColor     = serializedObject.FindProperty("_waveColor");
        propWaveAmplitude = serializedObject.FindProperty("_waveAmplitude");
        propWaveExponent  = serializedObject.FindProperty("_waveExponent");
        propWaveInterval  = serializedObject.FindProperty("_waveInterval");
        propWaveSpeed     = serializedObject.FindProperty("_waveSpeed");
        propAddColor      = serializedObject.FindProperty("_addColor");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(propMode);

        EditorGUI.indentLevel++;

        if (propMode.hasMultipleDifferentValues ||
            propMode.enumValueIndex == (int)SonarFx.SonarMode.Directional)
            EditorGUILayout.PropertyField(propDirection);

        if (propMode.hasMultipleDifferentValues ||
            propMode.enumValueIndex == (int)SonarFx.SonarMode.Spherical)
            EditorGUILayout.PropertyField(propOrigin);

        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField("Base Color");
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(propBaseColor, new GUIContent("Albedo"));
        EditorGUILayout.PropertyField(propAddColor, new GUIContent("Emission"));
        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField("Wave Parameters");
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(propWaveColor, new GUIContent("Color"));
        EditorGUILayout.PropertyField(propWaveAmplitude, new GUIContent("Amplitude"));
        EditorGUILayout.PropertyField(propWaveExponent, new GUIContent("Exponent"));
        EditorGUILayout.PropertyField(propWaveInterval, new GUIContent("Interval"));
        EditorGUILayout.PropertyField(propWaveSpeed, new GUIContent("Speed"));
        EditorGUI.indentLevel--;
        
        serializedObject.ApplyModifiedProperties();
    }
}
