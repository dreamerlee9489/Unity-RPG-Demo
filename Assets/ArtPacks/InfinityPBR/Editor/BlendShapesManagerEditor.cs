using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;
using InfinityPBR;

[CustomEditor(typeof(BlendShapesManager))]
[CanEditMultipleObjects]
[Serializable]
public class BlendShapesManagerEditor : Editor
{

    private bool showHelpBoxes = true;
    private bool showSetup = true;
    private bool showPresets = true;
    private bool showFullInspector = false;
    private bool showBlendShapeObjects = true;
    private bool showRangeFiles = true;
    private bool showPresetFiles = true;
    private Color inactiveColor2 = new Color(0.75f, .75f, 0.75f, 1f);
    private Color activeColor = new Color(0.6f, 1f, 0.6f, 1f);
    private Color activeColor2 = new Color(0.0f, 1f, 0.0f, 1f);
    private Color mixedColor = Color.yellow;
    private Color redColor = new Color(1f, 0.25f, 0.25f, 1f);

    private BlendShapesManager blendShapesManager;
    
    public string exportPath = "";

    //private List<MatchList> matchList = new List<MatchList>();
    //private string[] matchListNames;
    //private int matchListIndex = 0;
    private string rangeFileName;
    private string presetFileName;

    private bool userMatchRespectsLimits = true;

    public override void OnInspectorGUI()
    {
        BlendShapesManager blendShapesManager = (BlendShapesManager) target;
        
        if (showHelpBoxes)
        {
            EditorGUILayout.HelpBox("BLEND SHAPES MANAGER\n" +
                                    "Use this script to easily manage the blend shapes attached Infinity PBR models.", MessageType.None);
        }
        
        /* ------------------------------------------------------------------------------------------
         SETUP AND OPTIONS
         ------------------------------------------------------------------------------------------*/
        showSetup = EditorGUILayout.Foldout(showSetup, "Setup & Options");
        if (showSetup)
        {
            // MAIN OPTIONS
            EditorGUI.indentLevel++;
            showHelpBoxes = EditorGUILayout.Toggle("Show Help Boxes", showHelpBoxes);
            showFullInspector = EditorGUILayout.Toggle("Show Full Inspector", showFullInspector);

            EditorGUILayout.Space();
            
            if (showHelpBoxes)
            {
                EditorGUILayout.HelpBox("MATCHED SHAPES RESPECT LIMITS\nIf true, shapes that you choose to control others will use the distance from their " +
                                        "min and max limits to determine the matched shapes final value. If false, the matched shapes " +
                                        "will be set to the raw blend shape value of the controlling shape.", MessageType.None);
            }
            blendShapesManager.userMatchRespectsLimits = EditorGUILayout.Toggle("Matched Shapes Respect Limits", blendShapesManager.userMatchRespectsLimits);
            if (userMatchRespectsLimits != blendShapesManager.userMatchRespectsLimits)
            {
                blendShapesManager.SetAllShapeValues();
                userMatchRespectsLimits = blendShapesManager.userMatchRespectsLimits;
            }

            
           
            /* ------------------------------------------------------------------------------------------
            RELOAD / LOAD OPTIONS
            ------------------------------------------------------------------------------------------*/
            EditorGUILayout.Space();
            if (showHelpBoxes)
            {
                EditorGUILayout.HelpBox("LOAD BLEND SHAPES DATA\nThis will load blend shapes from new objects, and remove any objects from " +
                                        "the list that are no longer in the scene. It will not overwrite or update existing " +
                                        "objects, and will attempt to re-link any missing objects.\n\n" +
                                        "SET ALL VALUES TO 0\nThis will set all shape values to zero; will not change other settings or data.\n\n" +
                                        "RELOAD BLEND SHAPE DATA\nWARNING: This will delete and reload all blend shape data!", MessageType.None);
            }
            
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Load Blend Shape Data", GUILayout.Width(200), GUILayout.Height(50)))
                blendShapesManager.LoadBlendShapeData();
            
            GUI.backgroundColor = mixedColor;
            if (GUILayout.Button("Set All Values to 0", GUILayout.Width(200), GUILayout.Height(50)))
            {
                blendShapesManager.ResetAll();
            }

            GUI.backgroundColor = Color.white;
            
            GUI.backgroundColor = redColor;
            if (GUILayout.Button("Reload Blend Shape Data", GUILayout.Width(200), GUILayout.Height(50)))
            {
                blendShapesManager.blendShapeGameObjects.Clear();
                blendShapesManager.LoadBlendShapeData();
            }

           
            GUI.backgroundColor = Color.white;

            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space();
            if (showHelpBoxes)
            {
                EditorGUILayout.HelpBox("PREFIX STRINGS\nThese are the parts of blend shape names which designate " +
                                        "a shape as a primary shape (i.e. can be seen in this inspector and managed by a user) " +
                                        "and a \"match\" shape. Match shapes copy the values of the primary shape for example a " +
                                        "shirt may have a match shape for the arm size of a character, so when the primary " +
                                        "arm shape is increased, the shirt also increases in size to match.", MessageType.None);
            }

            EditorGUILayout.HelpBox("Do not change these values unless you know what you're doing!", MessageType.Warning);
            blendShapesManager.blendShapePrimaryPrefix = EditorGUILayout.TextField("Primary Prefix", blendShapesManager.blendShapePrimaryPrefix);
            blendShapesManager.blendShapeMatchPrefix = EditorGUILayout.TextField("Match Prefix", blendShapesManager.blendShapeMatchPrefix);
            blendShapesManager.plusMinus.Item1 = EditorGUILayout.TextField("Plus", blendShapesManager.plusMinus.Item1);
            blendShapesManager.plusMinus.Item2 = EditorGUILayout.TextField("Minus", blendShapesManager.plusMinus.Item2);
            
            if (showHelpBoxes)
            {
                EditorGUILayout.HelpBox("SHAPE NAMING CONVENTION\nIf you are creating your own shapes you can either use " +
                                        "our naming convention or your own. However, they need to be set up in this structure. A primary " +
                                        "shape has three parts, and a match shape has four, separated by an underscore:\n\nPRIMARY: SFB_BS_HumanReadableName\n" +
                                        "MATCH: SFB_BSM_SomeText_HumanReadableName (note the last section must match the HumanReadableName of the primary shape.\n\n" +
                                        "PLUS/MINUS: For shapes that affect one part of the mesh in opposite ways, such as making something bigger and smaller, " +
                                        "use \"Plus\" and \"Minus\" at the end of the human readable name. The script will display one slider which affects both " +
                                        "shapes, ensuring only one is active at a time. Here is an example:\n\n" +
                                        "PRIMARY: SFB_BS_HumanReadableNamePlus and SFB_BS_HumanReadableNameMinus\nMATCH: SFB_BSM_SomeText_HumanReadableNamePlus and SFB_BSM_SomeText_HumanReadableNameMinus\n\n" +
                                        "We suggest that you use our naming convention so that your shapes and ours can play nicely together.", MessageType.Warning);
            }
            
            EditorGUI.indentLevel--;
        }
        
        /* ------------------------------------------------------------------------------------------
         PRESETS
         ------------------------------------------------------------------------------------------*/
        
        DrawLine(true);
        
        
        if (showHelpBoxes)
        {
            EditorGUILayout.HelpBox("You are able to save and load \"Range\" files which contain predefined " +
                                    "values for min and max levels\n\nNOTE: For those who used the previous version of the Blend Shapes Manager, (" +
                                    "\"SFB_BlendShapesManager.cs\"), you are able to import range settings exported from that script " +
                                    "to migrate your settings to the new version. Use the new script \"Blend Shape Presets\" to create " +
                                    "and manage groups of preset and randomized blend shape values.", MessageType.None);
        }
        
        showRangeFiles = EditorGUILayout.Foldout(showRangeFiles, "Range Files (" + blendShapesManager.rangeFiles.Count + ")");
        if (showRangeFiles)
        {
            EditorGUI.indentLevel++;
            
            // ADD NEW RANGE FILE

            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Drag & Drop Range File to Add");
            TextAsset newRangeObject = null;
            newRangeObject =
                EditorGUILayout.ObjectField(newRangeObject, typeof(TextAsset), false, GUILayout.Height(30)) as
                    TextAsset;
            if (newRangeObject)
            {
                blendShapesManager.ImportRangeFile(newRangeObject);
                newRangeObject = null;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
            
            // SHOW EXISTING RANGE FILES
            for (int i = 0; i < blendShapesManager.rangeFiles.Count; i++)
            {
                BlendShapePresetFile range = blendShapesManager.rangeFiles[i];
                EditorGUILayout.BeginHorizontal();

                range.name = EditorGUILayout.TextField("Name", range.name);
                EditorGUILayout.ObjectField(range.textAsset, typeof(TextAsset), false);
                if (GUILayout.Button("Load"))
                {
                    blendShapesManager.LoadRangeFile(range.textAsset);
                }

                GUI.backgroundColor = redColor;
                if (GUILayout.Button("X"))
                {
                    blendShapesManager.rangeFiles.RemoveAt(i);
                }

                GUI.backgroundColor = Color.white;

                EditorGUILayout.EndHorizontal();
            }
            
            // EXPORT RANGE FILE
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Export Path: " + exportPath);
            if (GUILayout.Button("Choose Path"))
            {
                exportPath = EditorUtility.SaveFolderPanel("Where should exports be saved?", "", "");
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (exportPath != "")
                presetFileName = EditorGUILayout.TextField("Export Name", rangeFileName);
            else
                EditorGUILayout.LabelField("You must select an export path before exporting.");
            if (GUILayout.Button("Export Range File"))
            {
                ExportRangeFile(blendShapesManager);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        



        /* ------------------------------------------------------------------------------------------
        BLEND SHAPE OBJECTS
         ------------------------------------------------------------------------------------------*/
        DrawLine();
        showBlendShapeObjects = EditorGUILayout.Foldout(showBlendShapeObjects, "Blend Shape Objects (" + blendShapesManager.blendShapeGameObjects.Count + ")");
        if (showBlendShapeObjects)
        {
            EditorGUI.indentLevel++;
            for (int o = 0; o < blendShapesManager.blendShapeGameObjects.Count; o++)
            {
                if (blendShapesManager.blendShapeGameObjects[o].displayableValues > 0)
                    DisplayObject(blendShapesManager, blendShapesManager.blendShapeGameObjects[o], o);
            }


            EditorGUI.indentLevel--;
        }

        /* ------------------------------------------------------------------------------------------
         DEFAULT INSPECTOR
         ------------------------------------------------------------------------------------------*/
        if (showFullInspector)
        {
            DrawLine(true);
            
            DrawDefaultInspector();
        }
    }

    private void DisplayObject(BlendShapesManager blendShapesManager, BlendShapeGameObject obj, int index)
    {
        if (!obj.gameObject)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("This gameObject is missing. Will attempt relink when LoadBlendShapeData() is called.");
            EditorGUI.indentLevel--;
            return;
        }
        obj.showValues = EditorGUILayout.Foldout(obj.showValues, obj.gameObject.name + " (" + obj.displayableValues + ")");
        if (obj.showValues)
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < obj.blendShapeValues.Count; i++)
            {
                if (obj.blendShapeValues[i].display && obj.blendShapeValues[i].matchAnotherValue)
                    DisplayMatchMessage(blendShapesManager, obj, obj.blendShapeValues[i]);
                else if (obj.blendShapeValues[i].display)    
                    DisplayValue(blendShapesManager, obj, obj.blendShapeValues[i], i);
            }
            EditorGUI.indentLevel--;
        }
    }

    private void DisplayMatchMessage(BlendShapesManager blendShapesManager, BlendShapeGameObject obj, BlendShapeValue value)
    {
        GUI.backgroundColor = redColor;
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUI.indentLevel++;
        EditorGUILayout.BeginHorizontal();
        
        BlendShapeGameObject matchedObject = blendShapesManager.blendShapeGameObjects[value.matchThisObjectIndex];
        BlendShapeValue matchedValue = matchedObject.blendShapeValues[value.matchThisValueIndex];
        EditorGUILayout.LabelField(value.triggerName + " is matched to " + matchedObject.gameObject.name + " " + matchedValue.triggerName);

        if (GUILayout.Button("Remove Match", GUILayout.Width(150)))
        {
            RemoveControllingMatch(blendShapesManager, value);
        }
        
        GUI.backgroundColor = Color.white;
        
        EditorGUI.indentLevel--;
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }
    private void DisplayValue(BlendShapesManager blendShapesManager, BlendShapeGameObject obj, BlendShapeValue value, int index)
    {
        
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUI.indentLevel++;
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(value.showValueOptions ? "o" : "=", GUILayout.Width(20)))
        {
            value.showValueOptions = !value.showValueOptions;
            if (!value.showValueOptions)
                value.isOpen = value.showValueOptions;

            if (!value.isOpen && value.showValueOptions)
            {
                value.isOpen = value.showValueOptions;
                BuildMatchLists(blendShapesManager);
            }
            
        }
        EditorGUILayout.LabelField(value.triggerName, GUILayout.Width(200));
        
        EditorGUILayout.BeginVertical(GUILayout.Width(200));
        EditorGUILayout.BeginHorizontal();
        if (value.min < 0)
        {
            if (GUILayout.Button("" + (int)value.min, GUILayout.Width(40)))
            {
                value.value = value.min;
                value.lastValue = value.value;
                blendShapesManager.TriggerShape(obj, value);
            }
        }
        
        if (GUILayout.Button("0", GUILayout.Width(40)))
        {
            Undo.RecordObject(this, "Reset Value to Zero");
            value.value = 0f;
            value.lastValue = value.value;
            blendShapesManager.TriggerShape(obj, value);
        }

        if (GUILayout.Button("" + (int)value.max, GUILayout.Width(40)))
        {
            Undo.RecordObject(this, "Set Max Value");
            value.value = value.max;
            value.lastValue = value.value;
            blendShapesManager.TriggerShape(obj, value);
        }

        if (GUILayout.Button("Random", GUILayout.Width(60)))
        {
            Undo.RecordObject(this, "Set Random Value");
            blendShapesManager.SetRandomShapeValue(value);
            blendShapesManager.TriggerShape(obj, value);
        }
        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        value.value = EditorGUILayout.Slider(value.value, value.min, value.max, GUILayout.Width(250));
        
        EditorGUILayout.EndHorizontal();
        
        if (value.showValueOptions)
        {
            if (showHelpBoxes)
            {
                EditorGUILayout.HelpBox("Limit the min and max values a shape can be set to when the " +
                                        "Random method is called. This is helpful to ensure that combinations of random " +
                                        "values don't make an object look too distorted, as full shape values can " +
                                        "often be extreme.", MessageType.None);
            }
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Limit Random Range", GUILayout.Width(250));

            if (GUILayout.Button("Set Min"))
            {
                value.limitMin = value.value;
                value.lastValue = -value.value;
            }
            value.limitMin = EditorGUILayout.IntField(Mathf.RoundToInt(value.limitMin));
            EditorGUILayout.MinMaxSlider(ref value.limitMin, ref value.limitMax, value.min, value.max);
            value.limitMax = EditorGUILayout.IntField(Mathf.RoundToInt(value.limitMax));
            if (GUILayout.Button("Set Max"))
            {
                value.limitMax = value.value;
                value.lastValue = -value.value;
            }

            //value.value = Mathf.Clamp(value.value, value.limitMin, value.limitMax);

            if (value.limitMin > 0 && value.limitMin >= value.limitMax)
                value.limitMin = value.limitMax - 1f;
            if (value.limitMax < 0 && value.limitMax <= value.limitMin)
                value.limitMax = value.limitMin + 1f;
            
            EditorGUILayout.EndHorizontal();
            
            if (showHelpBoxes)
            {
                EditorGUILayout.HelpBox("Select another shape value which will copy this shape value when this " +
                                        "value changes. This is useful when you know two shapes should always move together, " +
                                        "perhaps the right and left boot of a character, or leg muscles and arm muscles.", MessageType.None);
            }
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Child Match Shapes", GUILayout.Width(250));

            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();

            if (blendShapesManager.matchListNames.Length == 0)
                BuildMatchLists(blendShapesManager);

            if (blendShapesManager.matchListNames.Length == 0)
            {
                EditorGUILayout.LabelField("No Shapes Found");
            }
            else
            {
                blendShapesManager.matchListIndex = EditorGUILayout.Popup(blendShapesManager.matchListIndex, blendShapesManager.matchListNames);
            
                if (GUILayout.Button("Add Match", GUILayout.Width(100)))
                {
                    SetMatchShape(blendShapesManager, value, blendShapesManager.matchList[blendShapesManager.matchListIndex]);
                }
            }
            
            EditorGUILayout.EndHorizontal();

            for (int m = 0; m < value.otherValuesMatchThis.Count; m++)
            {
                BlendShapeGameObject otherObject = blendShapesManager.blendShapeGameObjects[value.otherValuesMatchThis[m].objectIndex];
                BlendShapeValue otherValue = otherObject.blendShapeValues[value.otherValuesMatchThis[m].valueIndex];

                EditorGUILayout.BeginHorizontal();
                GUI.backgroundColor = redColor;
                if (GUILayout.Button("X", GUILayout.Width(25)))
                {
                    RemoveControllingMatch(blendShapesManager, otherValue);
                    GUI.backgroundColor = Color.white;
                }
                else
                {
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.LabelField(otherObject.gameObject.name + " " + otherValue.triggerName, GUILayout.Width(250));
                }
                EditorGUILayout.EndHorizontal();
                
            }
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        } 

        if (value.value != value.lastValue)
        {
            value.lastValue = value.value;
            blendShapesManager.TriggerShape(obj, value);
        }
        
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }
    
    private void RemoveControllingMatch(BlendShapesManager blendShapesManager, BlendShapeValue value)
    {
        BlendShapeValue matchedValue = blendShapesManager.blendShapeGameObjects[value.matchThisObjectIndex].blendShapeValues[value.matchThisValueIndex];

        for (int i = 0; i < matchedValue.otherValuesMatchThis.Count; i++)
        {
            BlendShapeValue checkThisValue = blendShapesManager
                .blendShapeGameObjects[matchedValue.otherValuesMatchThis[i].objectIndex]
                .blendShapeValues[matchedValue.otherValuesMatchThis[i].valueIndex];

            if (checkThisValue == value)
            {
                matchedValue.otherValuesMatchThis.RemoveAt(i);
                break;
            }
        }

        value.matchThisObjectIndex = 0;
        value.matchThisValueIndex = 0;
        value.matchAnotherValue = false;
        
        BuildMatchLists(blendShapesManager);
    }

    private void SetMatchShape(BlendShapesManager blendShapesManager, BlendShapeValue value, MatchList matchList)
    {
        BlendShapeValue matchedValue = blendShapesManager.blendShapeGameObjects[matchList.objectIndex].blendShapeValues[matchList.valueIndex];

        if (value == matchedValue)
        {
            Debug.Log("Error: A value can't match itself!");
            return;
        }

        if (matchedValue.otherValuesMatchThis.Count > 0)
        {
            Debug.Log("Error: A matched shape can't control other shapes!");
            return;
        }
        
        // Add the match shape to the controlling shape
        value.otherValuesMatchThis.Add(new MatchValue());
        MatchValue newMatchValue = value.otherValuesMatchThis[value.otherValuesMatchThis.Count - 1];
        newMatchValue.objectIndex = matchList.objectIndex;
        newMatchValue.valueIndex = matchList.valueIndex;
        
        // Set the match shape as controlled
        matchedValue.matchThisObjectIndex = value.objectIndex;
        matchedValue.matchThisValueIndex = value.index;
        matchedValue.matchAnotherValue = true;

        BuildMatchLists(blendShapesManager);
        blendShapesManager.TriggerShape(blendShapesManager.blendShapeGameObjects[value.objectIndex], value);
    }
    
    private void BuildMatchLists(BlendShapesManager blendShapesManager)
    {
        blendShapesManager.matchList.Clear();
        blendShapesManager.matchListIndex = 0;

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

                blendShapesManager.matchList.Add(new MatchList());
                MatchList newMatchlist = blendShapesManager.matchList[blendShapesManager.matchList.Count - 1];
                
                newMatchlist.name = obj.gameObject.name + " " + value.triggerName;
                newMatchlist.objectIndex = o;
                newMatchlist.valueIndex = i;
            }
        }

        blendShapesManager.matchListNames = new string[blendShapesManager.matchList.Count];
        for (int i = 0; i < blendShapesManager.matchList.Count; i++)
        {
            blendShapesManager.matchListNames[i] = blendShapesManager.matchList[i].name;
        }
        
    }

    private void ExportRangeFile(BlendShapesManager blendShapesManager)
    {
        if (exportPath == "")
            return;
        string exportName = GetNewRangeFileName(blendShapesManager, rangeFileName);

        string exportString = "InfinityPBR_BlendShapesManager_RangeFile";

        for (int o = 0; o < blendShapesManager.blendShapeGameObjects.Count; o++)
        {
            BlendShapeGameObject obj = blendShapesManager.blendShapeGameObjects[o];
            for (int i = 0; i < obj.blendShapeValues.Count; i++)
            {
                BlendShapeValue value = obj.blendShapeValues[i];
                if (value.display)
                    exportString = exportString + "," + obj.gameObjectName + "," + value.fullName + "," + value.limitMin + "," + value.limitMax;
            }
        }
        
        // Turns out we can't use Json because the class is a monobehavior.
        /*
        string exportString = JsonUtility.ToJson(blendShapesManager);
        
        Debug.Log("Json: " + exportString);
        */
        if (!Directory.Exists(exportPath))
            System.IO.Directory.CreateDirectory(exportPath);
        
        System.IO.File.WriteAllText(exportPath + "/" + exportName + ".txt", exportString);

#if UNITY_EDITOR
        AssetDatabase.Refresh();
        #endif
    }

    private string GetNewRangeFileName(BlendShapesManager blendShapesManager, string chosenName)
    {
        string newName = string.IsNullOrEmpty(chosenName) ? "Exported Range File" : chosenName;
        string cachedName = newName;
        int count = 1;
        while (File.Exists(exportPath + "/" + newName + ".txt"))
        {
            count++;
            newName = cachedName + "" + count;

            if (count > 100)
                return "Range File";
        }

        return newName;
    }

/*
    [System.Serializable]
    public class MatchList
    {
        public string name;
        public int objectIndex;
        public int valueIndex;
    }*/
    
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
