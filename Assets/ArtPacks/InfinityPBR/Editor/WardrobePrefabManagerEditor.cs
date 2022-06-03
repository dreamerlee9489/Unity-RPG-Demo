using UnityEngine;
using UnityEditor;
using System;
using InfinityPBR;
using TMPro.EditorUtilities;

[CustomEditor(typeof(WardrobePrefabManager))]
[CanEditMultipleObjects]
[Serializable]
public class WardrobePrefabManagerEditor : Editor
{

    private bool showHelpBoxes = true;
    private bool showFullInspector = false;
    private bool showBlendShapeManagement = false;
    private Color inactiveColor2 = new Color(0.75f, .75f, 0.75f, 1f);
    private Color activeColor = new Color(0.6f, 1f, 0.6f, 1f);
    private Color activeColor2 = new Color(0.0f, 1f, 0.0f, 1f);
    private Color mixedColor = Color.yellow;
    private Color redColor = new Color(1f, 0.25f, 0.25f, 1f);
    private bool showSetup = true;
    
    private string[] actionTypes = new string[] {"Explicit", "Less than", "Greater than"};

    private BlendShapesManager blendShapesManager;
    
    public override void OnInspectorGUI()
    {
        WardrobePrefabManager wardrobePrefabManager = (WardrobePrefabManager) target;
        
        CheckOptionsAreSet(wardrobePrefabManager);
        
        if (showHelpBoxes)
        {
            EditorGUILayout.HelpBox("Wardrobe Prefab Manager\n" +
                                    "TO BE UPDATED", MessageType.None);
        }

        showSetup = EditorGUILayout.Foldout(showSetup, "Setup & Options");
        if (showSetup)
        {
            EditorGUI.indentLevel++;
            showHelpBoxes = EditorGUILayout.Toggle("Show help boxes", showHelpBoxes);
            showFullInspector = EditorGUILayout.Toggle("Show full inspector", showFullInspector);
            wardrobePrefabManager.autoRigWhenActivated = EditorGUILayout.Toggle("Auto rig when activated", wardrobePrefabManager.autoRigWhenActivated);
            if (wardrobePrefabManager.blendShapesManager)
            {
                wardrobePrefabManager.handleBlendShapes = EditorGUILayout.Toggle("Handle blend shapes", wardrobePrefabManager.handleBlendShapes);
            }

            EditorGUI.indentLevel--;
        }

        if (wardrobePrefabManager.handleBlendShapes)
        {
            CheckOptionsAreSet(wardrobePrefabManager);
            
            EditorGUILayout.Space();
            showBlendShapeManagement = EditorGUILayout.Foldout(showBlendShapeManagement, "Blend Shape Management");
            if (showBlendShapeManagement)

            {
                EditorGUILayout.Space();
                if (GUILayout.Button("Update Group List"))
                    wardrobePrefabManager.UpdateGroupList();

                for (int g = 0; g < wardrobePrefabManager.blendShapeGroups.Count; g++)
                {
                    //EditorGUILayout.Space();
                    BlendShapeGroup group = wardrobePrefabManager.blendShapeGroups[g];
                    group.showList = EditorGUILayout.Foldout(group.showList, group.name);
                    if (group.showList)
                    {
                        EditorGUI.indentLevel++;

                        string[] blendShapeNames = group.blendShapeNames.ToArray();

                        // ON ACTIVATE
                        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("On Activate", GUILayout.Width(120));

                        if (group.blendShapeNames.Count == 0)
                        {
                            EditorGUILayout.LabelField("No Blend Shapes Available");
                            EditorGUILayout.EndHorizontal();
                        }
                        else
                        {
                            group.shapeChoiceIndex = EditorGUILayout.Popup(group.shapeChoiceIndex, blendShapeNames);
                            if (GUILayout.Button("Add To List"))
                                AddToList("Activate", group);
                            EditorGUILayout.EndHorizontal();
                        }

                        // ON ACTIVATE GLOBAL
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Global Shapes", GUILayout.Width(120));

                        if (blendShapesManager.matchList.Count == 0)
                        {
                            EditorGUILayout.LabelField("No Global Blend Shapes Available");
                            EditorGUILayout.EndHorizontal();
                        }
                        else
                        {
                            blendShapesManager.matchListIndex = EditorGUILayout.Popup(blendShapesManager.matchListIndex,
                                blendShapesManager.matchListNames);
                            if (GUILayout.Button("Add To List"))
                                AddToList("Activate", blendShapesManager.matchList[blendShapesManager.matchListIndex],
                                    group);
                            EditorGUILayout.EndHorizontal();
                        }

                        // ON ACTIVATE LIST
                        for (int i = 0; i < group.onActivate.Count; i++)
                        {
                            BlendShapeItem item = group.onActivate[i];
                            DisplayItem(group, item, i, "Activate", wardrobePrefabManager);
                        }

                        EditorGUILayout.EndVertical();



                        // ON DEACTIVATE
                        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("On Deactivate", GUILayout.Width(120));

                        if (group.blendShapeNames.Count == 0)
                        {
                            EditorGUILayout.LabelField("No Blend Shapes Available");
                            EditorGUILayout.EndHorizontal();
                        }
                        else
                        {
                            group.shapeChoiceIndex = EditorGUILayout.Popup(group.shapeChoiceIndex, blendShapeNames);
                            if (GUILayout.Button("Add To List"))
                                AddToList("Deactivate", group);
                            if (GUILayout.Button("Revert Back"))
                                AddToList("Revert Back", group);
                            EditorGUILayout.EndHorizontal();
                        }

                        // ON DEACTIVATE GLOBAL
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Global Shapes", GUILayout.Width(120));

                        if (blendShapesManager.matchList.Count == 0)
                        {
                            EditorGUILayout.LabelField("No Global Blend Shapes Available");
                            EditorGUILayout.EndHorizontal();
                        }
                        else
                        {
                            blendShapesManager.matchListIndex = EditorGUILayout.Popup(blendShapesManager.matchListIndex,
                                blendShapesManager.matchListNames);
                            if (GUILayout.Button("Add To List"))
                                AddToList("Deactivate", blendShapesManager.matchList[blendShapesManager.matchListIndex],
                                    group);
                            if (GUILayout.Button("Revert Back"))
                                AddToList("Revert Back",
                                    blendShapesManager.matchList[blendShapesManager.matchListIndex], group);
                            EditorGUILayout.EndHorizontal();
                        }

                        // ON ACTIVATE LIST
                        for (int i = 0; i < group.onDeactivate.Count; i++)
                        {
                            BlendShapeItem item = group.onDeactivate[i];
                            DisplayItem(group, item, i, "Deactivate", wardrobePrefabManager);
                        }

                        EditorGUILayout.EndVertical();

                        EditorGUI.indentLevel--;
                    }
                }
            }
        }

        /* ------------------------------------------------------------------------------------------
         DEFAULT INSPECTOR
         ------------------------------------------------------------------------------------------*/
        if (showFullInspector)
        {
            EditorGUILayout.Space();
            DrawDefaultInspector();
        }
            
        
        
        
        
    }

    private void DisplayItem(BlendShapeGroup group, BlendShapeItem item, int itemIndex, string type, WardrobePrefabManager wardrobePrefabManager)
    {
        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = redColor;
        if (GUILayout.Button("X", GUILayout.Width(25)))
        {
            EditorGUILayout.EndHorizontal();

            if (type == "Activate")
                group.onActivate.RemoveAt(itemIndex);
            if (type == "Deactivate")
                group.onDeactivate.RemoveAt(itemIndex);

        }
        else
        {
            GUI.backgroundColor = Color.white;
            EditorGUILayout.LabelField(item.objectName + " " + item.triggerName);

            if (item.revertBack)
            {
                EditorGUILayout.LabelField("This value will revert to pre-activation value");
            }
            else
            {
                item.actionTypeIndex = EditorGUILayout.Popup(item.actionTypeIndex, actionTypes);
                item.actionType = actionTypes[item.actionTypeIndex];
                item.value = EditorGUILayout.Slider(item.value, item.min, item.max);
                if (GUILayout.Button("Set"))
                    wardrobePrefabManager.TriggerBlendShape(item.triggerName, item.value);
            }
        }

        EditorGUILayout.EndHorizontal();
    }

    private void AddToList(string eventType, MatchList matchList, BlendShapeGroup group)
    {
        BlendShapeItem newItem = new BlendShapeItem();
        BlendShapeGameObject obj = blendShapesManager.blendShapeGameObjects[matchList.objectIndex];
        BlendShapeValue value = obj.blendShapeValues[matchList.valueIndex];
        string triggerName = value.triggerName;
        
        if (eventType == "Activate")
        {
            // This commented-out loop was intended to ensure only one entry per item, but we now wnat to allow 
            // multiple due to the addition of the "actionType", so you can do less than & greater than. (Between)
            for (int i = 0; i < group.onActivate.Count; i++)
            {
                //if (group.onActivate[i].triggerName == triggerName)
                //    return;
            }
            group.onActivate.Add(new BlendShapeItem());
            newItem = group.onActivate[group.onActivate.Count - 1];
        }
        if (eventType == "Deactivate" || eventType == "Revert Back")
        {
            for (int i = 0; i < group.onDeactivate.Count; i++)
            {
                if (group.onDeactivate[i].triggerName == triggerName)
                    return;
            }
            group.onDeactivate.Add(new BlendShapeItem());
            newItem = group.onDeactivate[group.onDeactivate.Count - 1];
            if (eventType == "Revert Back")
                newItem.revertBack = true;
        }

        newItem.min = value.min;
        newItem.triggerName = triggerName;
        newItem.objectName = obj.gameObjectName;
    }
    
    private void AddToList(string eventType, BlendShapeGroup group)
    {
        BlendShapeItem newItem = new BlendShapeItem();
        if (group.actualBlendShapeNames[group.shapeChoiceIndex].Contains(blendShapesManager.plusMinus.Item1))
            newItem.min = -100f;
        string triggerName = blendShapesManager.GetHumanName(group.actualBlendShapeNames[group.shapeChoiceIndex]);
        string objectName = "";
        
        if (eventType == "Activate")
        {
            for (int i = 0; i < group.onActivate.Count; i++)
            {
                if (group.onActivate[i].triggerName == triggerName)
                    return;
            }
            group.onActivate.Add(new BlendShapeItem());
            newItem = group.onActivate[group.onActivate.Count - 1];
            objectName = group.blendShapeObjectName[group.shapeChoiceIndex];
        }
        else if (eventType == "Deactivate" || eventType == "Revert Back")
        {
            for (int i = 0; i < group.onDeactivate.Count; i++)
            {
                if (group.onDeactivate[i].triggerName == group.actualBlendShapeNames[group.shapeChoiceIndex])
                    return;
            }
            group.onDeactivate.Add(new BlendShapeItem());
            newItem = group.onDeactivate[group.onDeactivate.Count - 1];
            objectName = group.blendShapeObjectName[group.shapeChoiceIndex];
            if (eventType == "Revert Back")
                newItem.revertBack = true;
        }

        newItem.triggerName = triggerName;
        newItem.objectName = objectName;

    }

    private void CheckOptionsAreSet(WardrobePrefabManager wardrobePrefabManager)
    {
        if (!wardrobePrefabManager.blendShapesManager)
        {
            if (wardrobePrefabManager.GetComponent<BlendShapesManager>())
            {
                wardrobePrefabManager.blendShapesManager = wardrobePrefabManager.GetComponent<BlendShapesManager>();
            }
        }

        if (!wardrobePrefabManager.prefabChildManager)
        {
            if (wardrobePrefabManager.GetComponent<PrefabChildManager>())
            {
                wardrobePrefabManager.prefabChildManager = wardrobePrefabManager.GetComponent<PrefabChildManager>();
            }
        }

        blendShapesManager = wardrobePrefabManager.blendShapesManager;
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
