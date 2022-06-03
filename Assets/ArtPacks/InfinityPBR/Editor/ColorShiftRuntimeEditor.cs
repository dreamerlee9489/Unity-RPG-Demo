using UnityEngine;
using UnityEditor;

namespace InfinityPBR
{
    [CustomEditor(typeof(ColorShiftRuntime))]
    public class ColorShiftRuntimeEditor : Editor 
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            EditorPrefs.SetBool("Show Shifter Code Samples", EditorGUILayout.Foldout(EditorPrefs.GetBool("Show Shifter Code Samples"), "Show code examples"));
            if (EditorPrefs.GetBool("Show Shifter Code Samples"))
            {
                EditorGUILayout.LabelField("Set to color set by name", EditorStyles.boldLabel);
                EditorGUILayout.LabelField(
                    "Once you have named your color sets, you can activate them at run time using the following code to pass the name (string) of the color set:", EditorStyles.wordWrappedLabel);
                
                GUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.LabelField("ColorShiftRuntime.SetColorSet(string);\n" );
                GUILayout.EndVertical();
                
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Set to color set by id", EditorStyles.boldLabel);
                EditorGUILayout.LabelField(
                    "You can activate the color set by id at run time using the following code to pass the index (int) of the color set:", EditorStyles.wordWrappedLabel);
                
                GUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.LabelField("ColorShiftRuntime.SetColorSet(int);\n" );
                GUILayout.EndVertical();
                
                
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Set random color set", EditorStyles.boldLabel);
                EditorGUILayout.LabelField(
                    "Randomly assign a color set:", EditorStyles.wordWrappedLabel);
                
                GUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.LabelField("ColorShiftRuntime.SetRandomColorSet();\n" );
                GUILayout.EndVertical();
                
                
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Reset to active color set", EditorStyles.boldLabel);
                EditorGUILayout.LabelField(
                    "Return to the active color set by calling the following:", EditorStyles.wordWrappedLabel);
                
                GUILayout.BeginHorizontal(EditorStyles.helpBox);
                EditorGUILayout.LabelField("ColorShiftRuntime.SetActiveColorSet();" );
                GUILayout.EndHorizontal();
                
                
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Edit the Color Shifter object", EditorStyles.boldLabel);
                EditorGUILayout.LabelField(
                    "If you'd like to edit the object, use the menu found here:", EditorStyles.wordWrappedLabel);
                
                GUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.LabelField("Window/Infinity PBR/Color Shifter" );
                GUILayout.EndVertical();
                
            }
        }
    }
}
