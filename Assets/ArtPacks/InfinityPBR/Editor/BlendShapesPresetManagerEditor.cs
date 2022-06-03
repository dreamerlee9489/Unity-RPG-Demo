using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using InfinityPBR;

[CustomEditor(typeof(BlendShapesPresetManager))]
[CanEditMultipleObjects]
[Serializable]
public class BlendShapesPresetManagerEditor : Editor
{

    private bool showHelpBoxes = true;
    private bool showFullInspector = false;
    
    private Color inactiveColor2 = new Color(0.75f, .75f, 0.75f, 1f);
    private Color activeColor = new Color(0.6f, 1f, 0.6f, 1f);
    private Color activeColor2 = new Color(0.0f, 1f, 0.0f, 1f);
    private Color mixedColor = Color.yellow;
    private Color redColor = new Color(1f, 0.25f, 0.25f, 1f);
    private bool showSetup = true;

    private BlendShapesManager blendShapesManager;
    
    public override void OnInspectorGUI()
    {
        BlendShapesPresetManager presetManager = (BlendShapesPresetManager) target;
        
        CheckOptionsAreSet(presetManager);
        
        if (showHelpBoxes)
        {
            EditorGUILayout.HelpBox("BLEND SHAPE PRESET MANAGERR\n" +
                                    "Use this script with BlendShapesManager.cs to create groups of preset shapes, which can " +
                                    "be called with a single line of code. For example, you may wish to create a \"Strong\" or " +
                                    "a \"Weak\" version of a character, or have multiple face settings.\n\nYou can also set some " +
                                    "shapes to randomize on load, which will allow you to create random character looks whenever " +
                                    "one is instantiated.", MessageType.None);
        }

        showSetup = EditorGUILayout.Foldout(showSetup, "Setup & Options");
        if (showSetup)
        {
            EditorGUI.indentLevel++;
            showHelpBoxes = EditorGUILayout.Toggle("Show help boxes", showHelpBoxes);
            showFullInspector = EditorGUILayout.Toggle("Show full inspector", showFullInspector);
            
            EditorGUI.indentLevel--;
        }

        
        /* ------------------------------------------------------------------------------------------
         PRESET LIST
         ------------------------------------------------------------------------------------------*/
        DrawLine();

        EditorGUILayout.BeginHorizontal();
        // ADD NEW PRESET
        if (GUILayout.Button("Add new preset group"))
        {
            AddNewPresetGroup(presetManager);
        }

        if (GUILayout.Button("Reload Blendshape List"))
        {
            BuildShapeList(blendShapesManager, presetManager);
        }
        EditorGUILayout.EndHorizontal();
        
        // DISPLAY LIST
        for (int i = 0; i < presetManager.presets.Count; i++)
        {
            BlendShapePreset preset = presetManager.presets[i];
            GUI.backgroundColor = preset.showValues ? activeColor : Color.white;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(preset.showValues ? "o" : "=", GUILayout.Width(20)))
            {
                preset.showValues = !preset.showValues;
            }
            EditorGUILayout.LabelField("Preset Name",GUILayout.Width(150));
            preset.name = EditorGUILayout.TextField(preset.name);
            if (GUILayout.Button("Activate", GUILayout.Width(150)))
            {
                presetManager.ActivatePreset(i);
            }
            if (GUILayout.Button("Copy", GUILayout.Width(150)))
            {
                CopyPreset(presetManager, i);
            }
            
            EditorGUILayout.EndHorizontal();
            
            
            if (preset.showValues)
            {
                EditorGUI.indentLevel++;
                for (int v = 0; v < preset.presetValues.Count; v++)
                {
                    
                    BlendShapePresetValue value = preset.presetValues[v];
                    EditorGUILayout.BeginHorizontal();
                    GUI.backgroundColor = redColor;
                    if (GUILayout.Button("x", GUILayout.Width(20)))
                    {
                        preset.presetValues.RemoveAt(v);
                        GUI.backgroundColor = preset.showValues ? activeColor : Color.white;
                    }
                    else
                    {
                        
                        GUI.backgroundColor = preset.showValues ? activeColor : Color.white;
                        EditorGUILayout.LabelField(value.objectName + " " + value.valueTriggerName);
                        value.onTriggerModeIndex = EditorGUILayout.Popup(value.onTriggerModeIndex, presetManager.onTriggerMode);
                        value.onTriggerMode = presetManager.onTriggerMode[value.onTriggerModeIndex];
                        if (value.onTriggerMode == "Explicit")
                        {
                            value.shapeValue = EditorGUILayout.Slider(value.shapeValue, value.min, value.max);
                        }

                        if (value.onTriggerMode == "Random")
                        {
                            //EditorGUILayout.LabelField("This value will be randomized.");
                        }
                        
                    }

                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Add new shape", GUILayout.Width(150));
                presetManager.shapeListIndex = EditorGUILayout.Popup(presetManager.shapeListIndex, presetManager.shapeListNames);
                if (GUILayout.Button("Add Blendshape"))
                {
                    AddNewPresetValue(presetManager, preset);
                }
                EditorGUILayout.EndHorizontal();

                preset.globalModifier = EditorGUILayout.FloatField("Global Modifier", preset.globalModifier);
                preset.globalModifier = Mathf.Clamp(preset.globalModifier, 0f, 1f);
                
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Set all Explicit"))
                {
                    for (int v = 0; v < preset.presetValues.Count; v++)
                    {
                        preset.presetValues[v].onTriggerModeIndex = 0;
                        preset.presetValues[v].onTriggerMode = "Explicit";
                    }
                }
                if (GUILayout.Button("Set all Random"))
                {
                    for (int v = 0; v < preset.presetValues.Count; v++)
                    {
                        preset.presetValues[v].onTriggerModeIndex = 1;
                        preset.presetValues[v].onTriggerMode = "Random";
                    }
                }
                if (GUILayout.Button("Set values = 0"))
                {
                    for (int v = 0; v < preset.presetValues.Count; v++)
                    {
                        preset.presetValues[v].shapeValue = 0f;
                    }
                }
                EditorGUILayout.EndHorizontal();
                
                DrawLine();
                GUI.backgroundColor = redColor;
                if (GUILayout.Button("Remove This Preset"))
                {
                    presetManager.presets.RemoveAt(i);
                }
                GUI.backgroundColor = preset.showValues ? activeColor : Color.white;
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();
        }

        /* ------------------------------------------------------------------------------------------
         DEFAULT INSPECTOR
         ------------------------------------------------------------------------------------------*/
        if (showFullInspector)
        {
            DrawLine();
            EditorGUILayout.Space();
            DrawDefaultInspector();
        }

        EditorUtility.SetDirty(presetManager);
    }
    
    private void BuildShapeList(BlendShapesManager blendShapesManager, BlendShapesPresetManager presetManager)
    {
        Debug.Log("BuildShapeList");
        presetManager.shapeList.Clear();
        presetManager.shapeListIndex = 0;

        for (int o = 0; o < blendShapesManager.blendShapeGameObjects.Count; o++)
        {
            BlendShapeGameObject obj = blendShapesManager.blendShapeGameObjects[o];
            if (obj.displayableValues == 0)
                continue;
            if (!obj.gameObject)
                continue;
            
            for (int i = 0; i < obj.blendShapeValues.Count; i++)
            {
                BlendShapeValue value = obj.blendShapeValues[i];
                if (!value.display)
                    continue;
                if (value.matchAnotherValue)
                    continue;
                if (value.otherValuesMatchThis.Count > 0)
                    continue;

                presetManager.shapeList.Add(new Shape());
                Shape newShape = presetManager.shapeList[presetManager.shapeList.Count - 1];
                newShape.obj = obj;
                newShape.value = value;
            }
        }

        presetManager.shapeListNames = new string[presetManager.shapeList.Count];
        for (int i = 0; i < presetManager.shapeList.Count; i++)
        {
            presetManager.shapeListNames[i] = presetManager.shapeList[i].obj.gameObjectName + " " +
                                              presetManager.shapeList[i].value.triggerName;
        }

        UpdatePresetLimits(presetManager);
    }

    private void UpdatePresetLimits(BlendShapesPresetManager presetManager)
    {
        Debug.Log("UpdatePresetLimits");
        foreach (BlendShapePreset preset in presetManager.presets)
        {
            foreach (BlendShapePresetValue value in preset.presetValues)
            {
                BlendShapeValue shapeValue = blendShapesManager.GetBlendShapeValue(value.objectName, value.valueTriggerName);
                value.limitMin = shapeValue.limitMin;
                value.limitMax = shapeValue.limitMax;
                
            }
        }
    }

    private void AddNewPresetValue(BlendShapesPresetManager presetManager, BlendShapePreset preset)
    {
        preset.presetValues.Add(new BlendShapePresetValue());
        BlendShapePresetValue newValue = preset.presetValues[preset.presetValues.Count - 1];

        Shape shape = presetManager.shapeList[presetManager.shapeListIndex];

        newValue.objectName = shape.obj.gameObjectName;
        newValue.valueTriggerName = shape.value.triggerName;
        newValue.limitMin = shape.value.limitMin;
        newValue.limitMax = shape.value.limitMax;
        newValue.min = shape.value.min;
        newValue.max = shape.value.max;
    }

    private void AddNewPresetGroup(BlendShapesPresetManager presetManager)
    {
        presetManager.presets.Add(new BlendShapePreset());
    }

    private void CopyPreset(BlendShapesPresetManager presetManager, int presetIndex)
    {
        AddNewPresetGroup(presetManager);
        BlendShapePreset copyFrom = presetManager.presets[presetIndex];
        BlendShapePreset copyTo = presetManager.presets[presetManager.presets.Count - 1];
        copyTo.name = copyFrom.name + " Copy";
        copyTo.presetValues = new List<BlendShapePresetValue>();
        for (int v = 0; v < copyFrom.presetValues.Count; v++)
        {
            copyTo.presetValues.Add(new BlendShapePresetValue());
            copyTo.presetValues[v].max = copyFrom.presetValues[v].max;
            copyTo.presetValues[v].min = copyFrom.presetValues[v].min;
            copyTo.presetValues[v].limitMax = copyFrom.presetValues[v].limitMax;
            copyTo.presetValues[v].limitMin = copyFrom.presetValues[v].limitMin;
            copyTo.presetValues[v].shapeValue = copyFrom.presetValues[v].shapeValue;
            copyTo.presetValues[v].objectName = copyFrom.presetValues[v].objectName;
            copyTo.presetValues[v].onTriggerMode = copyFrom.presetValues[v].onTriggerMode;
            copyTo.presetValues[v].valueTriggerName = copyFrom.presetValues[v].valueTriggerName;
            copyTo.presetValues[v].onTriggerModeIndex = copyFrom.presetValues[v].onTriggerModeIndex;
        }

        copyTo.showValues = copyFrom.showValues;
    }
    
    private void CheckOptionsAreSet(BlendShapesPresetManager presetManager)
    {
        if (!presetManager.blendShapesManager)
        {
            if (presetManager.GetComponent<BlendShapesManager>())
            {
                presetManager.blendShapesManager = presetManager.GetComponent<BlendShapesManager>();
            }
        }
        
        blendShapesManager = presetManager.blendShapesManager;
    }
    
    void DrawLine(bool spacers = true, int height = 1)
    {
        if (spacers)
            EditorGUILayout.Space();
        Rect rect = EditorGUILayout.GetControlRect(false, height );
        rect.height = height;
        EditorGUI.DrawRect(rect, new Color ( 0.5f,0.5f,0.5f, 1 ) );
        if (spacers)
            EditorGUILayout.Space();
    }
}
