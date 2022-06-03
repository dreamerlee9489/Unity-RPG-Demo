using UnityEngine;
using UnityEditor;

namespace InfinityPBR
{
    [CustomEditor(typeof(ColorShifterObject))]
    public class ColorShifterObjectEditor : Editor 
    {
        private ColorShifterObject ThisObject;

        void OnEnable() => ThisObject = (ColorShifterObject) target;
        private void SetThisObject(ColorShifterObject newObject) => ThisObject = newObject;
        
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("To edit this Color Shifter Object, select \"Window/Infinity PBR/Color Shifter\", and add this object to the appropriate field in that Editor window.\n\n...or just click the button below....",
                MessageType.Warning);

            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Open Color Shifter Editor", GUILayout.Height(40)))
            {
                var newWindow = EditorWindow.GetWindow<ColorShifter>(false, "Color Shifter", true);
                newWindow.colorShifterObject = (ColorShifterObject) target;
            }
            GUI.backgroundColor = Color.white;

            EditorGUILayout.Space();
            EditorPrefs.SetBool("Shifter Object Draw Default Inspector", EditorGUILayout.ToggleLeft("Draw default inspector", EditorPrefs.GetBool("Shifter Object Draw Default Inspector")));

            
            if (!EditorPrefs.GetBool("Shifter Object Draw Default Inspector")) return;

            GUI.contentColor = Color.red;
            EditorGUILayout.HelpBox("Please do not edit this object directly unless you know what you're doing :)",
                MessageType.Warning);
            GUI.contentColor = Color.white;
            DrawDefaultInspector();
        }
    }
}
