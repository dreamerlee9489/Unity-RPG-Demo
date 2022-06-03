using UnityEngine;
using UnityEditor;
using System;
using InfinityPBR;
using UnityEditor.PackageManager.Requests;

[CustomEditor(typeof(PrefabChildManager))]
[CanEditMultipleObjects]
[Serializable]
public class PrefabChildManagerEditor : Editor
{
    private bool showHelpBoxes = true;
    private bool showSetup = true;
    private bool showFullInspector = false;
    private bool showPrefabGroups = true;
    private bool instantiatePrefabsAsAdded = true;
    private Color inactiveColor2 = new Color(0.75f, .75f, 0.75f, 1f);
    private Color activeColor = new Color(0.6f, 1f, 0.6f, 1f);
    private Color activeColor2 = new Color(0.0f, 1f, 0.0f, 1f);
    private Color mixedColor = Color.yellow;
    private Color redColor = new Color(1f, 0.25f, 0.25f, 1f);

    public Color ColorSet(PrefabChildManager prefabManager, int g)
    {
        int v = prefabManager.GroupIsActive(g);
        if (v == 2)
            return activeColor2;
        if (v == 1)
            return mixedColor;
        return Color.white;
    }

    public override void OnInspectorGUI()
    {
        PrefabChildManager prefabManager = (PrefabChildManager) target;

        if (prefabManager)
            prefabManager.MarkPrefabs(prefabManager);
        
        if (showHelpBoxes)
        {
            EditorGUILayout.HelpBox("PREFAB CHILD MANAGER\n" +
                                    "This inspector script is intended to make it easier to assign groups of prefabs, and" +
                                    " activate / deactivate them as a group. This could be helpful for managing modular " +
                                    "wardrobe or other objects.", MessageType.None);
        }
        
        showSetup = EditorGUILayout.Foldout(showSetup, "Setup & Options");
        if (showSetup)
        {
            EditorGUI.indentLevel++;
            showHelpBoxes = EditorGUILayout.Toggle("Show Help Boxes", showHelpBoxes);
            showFullInspector = EditorGUILayout.Toggle("Show Full Inspector", showFullInspector);
            instantiatePrefabsAsAdded = EditorGUILayout.Toggle("Instantiate Prefabs when Added to Group", instantiatePrefabsAsAdded);
            prefabManager.onlyOneGroupActivePerType = EditorGUILayout.Toggle("Only one group active per type", prefabManager.onlyOneGroupActivePerType);
            prefabManager.unpackPrefabs = EditorGUILayout.Toggle("Unpack Prefabs when Instantiated", prefabManager.unpackPrefabs);

            EditorGUILayout.Space();
            if (showHelpBoxes)
            {
                EditorGUILayout.HelpBox("Use the option below to set all InGameObject values to null. This is useful " +
                                        "if you've copied the component values from another character, to clean it up.", MessageType.None);
            }
            if (GUILayout.Button("Make all \"In Game Objects\" null"))
            {
                RemoveInGameObjectLinks(prefabManager);
            }
            
            if (showHelpBoxes)
            {
                EditorGUILayout.HelpBox("If you've copied another objects data or added the component from another object " +
                                        "as new to this object, use this option to relink all the available objects to the new object.\n\n " +
                                        "HINT: If you hold shift while you replace the transform in the list, all transforms will be updated to " +
                                        "the new selection.", MessageType.None);
            }
            if (GUILayout.Button("Relink objects to this parent object"))
            {
                RelinkObjects(prefabManager);
            }
            
            EditorGUI.indentLevel--;
            EditorUtility.SetDirty(this);
        }


        

        /* ------------------------------------------------------------------------------------------
         WARDROBE GROUPS
         ------------------------------------------------------------------------------------------*/
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Prefab Groups", EditorStyles.boldLabel);
        if (showHelpBoxes)
        {
            EditorGUILayout.HelpBox("Each \"Prefab Group\" can be named, and then activated or deactivated by " +
                                    "calling the following methods:\n\nTO DO ADD METHODS", MessageType.Info);
        }

        if (GUILayout.Button("Create New Group"))
        {
            prefabManager.CreateNewPrefabGroup();
        }
        
        showPrefabGroups = EditorGUILayout.Foldout(showPrefabGroups, prefabManager.prefabGroups.Count + " Prefab Groups");
        if (showPrefabGroups)
        {
            for (int g = 0; g < prefabManager.prefabGroups.Count; g++)
            {
                GUI.backgroundColor = ColorSet(prefabManager, g);
                
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("", GUILayout.Width(10));
                Undo.RecordObject (prefabManager, "Remove Prefab Group");
                
                GUI.backgroundColor = redColor;
                if (GUILayout.Button("X", GUILayout.Width(25)))
                {
                    EditorGUILayout.EndHorizontal();

                    for (int i = 0; i < prefabManager.prefabGroups[g].prefabObjects.Count; i++)
                    {
                        RemoveObject(prefabManager, prefabManager.prefabGroups[g], prefabManager.prefabGroups[g].prefabObjects[i], i);
                    }
                    
                    prefabManager.prefabGroups.RemoveAt(g);

                }
                else
                {
                    GUI.backgroundColor = Color.white;

                    Undo.RecordObject (prefabManager, "Change Prefab Group Name");
                    prefabManager.prefabGroups[g].name = EditorGUILayout.TextField(prefabManager.prefabGroups[g].name, GUILayout.Width(180));
                    
                    EditorGUILayout.LabelField("Type", GUILayout.Width(50));
                    Undo.RecordObject (prefabManager, "Change Prefab Group Type");
                    prefabManager.prefabGroups[g].groupType = EditorGUILayout.TextField(prefabManager.prefabGroups[g].groupType, GUILayout.Width(100));
                    
                    EditorGUILayout.LabelField("Default", GUILayout.Width(50));
                    Undo.RecordObject (prefabManager, "Change Prefab Group Default");
                    prefabManager.prefabGroups[g].isDefault = EditorGUILayout.Toggle(prefabManager.prefabGroups[g].isDefault);
                    

                    Undo.RecordObject (prefabManager, "Toggle Prefab Group Objects On");
                    GUI.backgroundColor = prefabManager.GroupIsActive(g) == 2 ? inactiveColor2 : Color.white;
                    if (GUILayout.Button("Activate", GUILayout.Width(80)))
                    {
                        prefabManager.ActivateGroup(g);
                    }
                    GUI.backgroundColor = Color.white;

                    Undo.RecordObject (prefabManager, "Toggle Prefab Group Objects Off");
                    //GUI.backgroundColor = !prefabManager.GroupIsActive(g) ? inactiveColor2 : Color.white;
                    GUI.backgroundColor = prefabManager.GroupIsActive(g) == 0 ? inactiveColor2 : Color.white;
                    if (GUILayout.Button("Deactivate", GUILayout.Width(80)))
                    {
                        prefabManager.DeactivateGroup(g);
                    }
                    GUI.backgroundColor = Color.white;

                    if (prefabManager.prefabGroups[g].showPrefabs)
                    {

                        Undo.RecordObject (prefabManager, "Toggle Wardrobe Group Show Objects");
                        if (GUILayout.Button("Objects (" + prefabManager.prefabGroups[g].prefabObjects.Count + ")", GUILayout.Width(80)))
                        {
                            prefabManager.prefabGroups[g].showPrefabs = false;
                        }

                    }
                    else
                    {
                        Undo.RecordObject (prefabManager, "Toggle Wardrobe Group Show Objects");
                        if (GUILayout.Button("Objects (" + prefabManager.prefabGroups[g].prefabObjects.Count + ")", GUILayout.Width(80)))
                        {
                            prefabManager.prefabGroups[g].showPrefabs = true;
                        }
                    }
                    
                    


                    EditorGUILayout.EndHorizontal();

                    if (prefabManager.prefabGroups[g].name == null || prefabManager.prefabGroups[g].name == "")
                    {
                        prefabManager.prefabGroups[g].name = "Prefab Group " + g;
                    }
                    
                    /*
                     * SHOW WARDROBE OBJECTS
                     */
                    if (prefabManager.prefabGroups[g].showPrefabs)
                    {
                        if (showHelpBoxes)
                        {
                            /*
                            EditorGUILayout.HelpBox("Press \"X\" to remove an object from the list. Undo works " +
                                                    "here, but you can also press \"Update Group\" above to repopulate " +
                                                    "the list entirely. You can replace an object and/or texture here " +
                                                    "as well.", MessageType.Info);
                                                    */
                        }
                        for (int i = 0; i < prefabManager.prefabGroups[g].prefabObjects.Count; i++)
                        {
                            PrefabObject prefabObject = prefabManager.prefabGroups[g].prefabObjects[i];
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("", GUILayout.Width(30));
                            Undo.RecordObject (prefabManager, "Remove Prefab Group GameObject");
                            GUI.backgroundColor = redColor;
                            if (GUILayout.Button("X", GUILayout.Width(25)))
                            {
                                EditorGUILayout.EndHorizontal();
                                /*
                                GameObject inGameObject = prefabObject.inGameObject;
                                if (prefabObject.isPrefab && inGameObject)
                                    prefabManager.DestroyObject(inGameObject);
                                else
                                    inGameObject.SetActive(false);
                                prefabManager.prefabGroups[g].prefabObjects.RemoveAt(i);
                                */
                                RemoveObject(prefabManager, prefabManager.prefabGroups[g], prefabObject, i);

                            }
                            else
                            {
                                GUI.backgroundColor = Color.white;

                                Undo.RecordObject (prefabManager, "Update Prefab Object In Group");
                                GameObject oldObject = prefabObject.prefab;
                                prefabObject.prefab = EditorGUILayout.ObjectField(prefabObject.prefab, typeof(GameObject), !prefabObject.isPrefab) as GameObject;
                                if (oldObject != prefabObject.prefab)
                                {
                                    if (prefabObject.isPrefab)
                                    {
                                        if (!PrefabUtility.IsPartOfAnyPrefab(prefabObject.prefab))
                                        {
                                            prefabObject.prefab = oldObject;
                                            Debug.LogError("Error: This isn't a prefab that can be added.");
                                        }
                                        else
                                        {
                                            Event e = Event.current;
                                            if (e.shift)
                                                UpdateAllObjects(prefabManager, oldObject, prefabObject.prefab);
                                        }
                                    }
                                    else
                                    {
                                        if (!prefabObject.prefab.transform.IsChildOf(prefabManager.transform))
                                        {
                                            prefabObject.prefab = oldObject;
                                            Debug.LogError("Error: This isn't a GameObject that can be added.");
                                        }
                                        else
                                        {
                                            Event e = Event.current;
                                            if (e.shift)
                                                UpdateAllObjects(prefabManager, oldObject, prefabObject.prefab);
                                        }
                                    }
                                    
                                    if (instantiatePrefabsAsAdded)
                                    {
                                        prefabManager.DeactivateGroup(g);
                                        prefabManager.ActivateGroup(g);
                                    }
                                        
                                }

                                if (prefabObject.isPrefab)
                                {
                                    Undo.RecordObject (prefabManager, "Update Parent Transform In Group");
                                    Transform oldTransformObject = prefabObject.parentTransform;
                                    prefabObject.parentTransform = EditorGUILayout.ObjectField(prefabObject.parentTransform, typeof(Transform), true) as Transform;
                                    if (oldTransformObject != prefabObject.parentTransform)
                                    {

                                        if (!prefabObject.parentTransform.IsChildOf(prefabManager.thisTransform))
                                        {
                                            prefabObject.parentTransform = oldTransformObject;
                                            Debug.LogError("Error: Transform must be the parent transform or a child of " + prefabManager.thisTransform.name);
                                        }
                                        else
                                        {
                                            Event e = Event.current;
                                            if (e.shift)
                                                UpdateAllTransforms(prefabManager, prefabObject.parentTransform);
                                        }
                                    }
                                }
                                
                                CheckOptionsAreSet(prefabManager, g, i);
                                

                                EditorGUILayout.EndHorizontal();
                            }
                            GUI.backgroundColor = Color.white;
                        }

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.BeginVertical();
                        prefabManager.prefabGroups[g].newPrefab = EditorGUILayout.ObjectField("Add Prefab or Child Object to Group", prefabManager.prefabGroups[g].newPrefab, typeof(GameObject), true) as GameObject;
                        if (prefabManager.prefabGroups[g].newPrefab)
                        {

                            if (prefabManager.prefabGroups[g].newPrefab.transform.IsChildOf(prefabManager.transform))
                                prefabManager.AddGameObjectToGroup(prefabManager.prefabGroups[g].newGameObject, g);
                            else if (PrefabUtility.IsPartOfAnyPrefab(prefabManager.prefabGroups[g].newPrefab))
                                prefabManager.AddPrefabToGroup(prefabManager.prefabGroups[g].newPrefab, g);
                            else
                                Debug.LogError("Error: " + prefabManager.prefabGroups[g].newPrefab.name + " isn't a prefab that can be added, or isn't a child of the parent object.");
                            
                            prefabManager.prefabGroups[g].newPrefab = null;

                            if (instantiatePrefabsAsAdded)
                            {
                                prefabManager.ActivateGroup(g);
                            }
                        }
                        EditorGUILayout.EndVertical();
                        /*EditorGUILayout.BeginVertical();
                        prefabManager.prefabGroups[g].newPrefab = EditorGUILayout.ObjectField("Add ChilD Object to Group", prefabManager.prefabGroups[g].newGameObject, typeof(GameObject), true) as GameObject;
                        if (prefabManager.prefabGroups[g].newGameObject)
                        {

                            if (prefabManager.prefabGroups[g].newGameObject.transform.IsChildOf(prefabManager.transform))
                                prefabManager.AddPrefabToGroup(prefabManager.prefabGroups[g].newGameObject, g);
                            else
                                Debug.LogError("Error: " + prefabManager.prefabGroups[g].newGameObject.name + " isn't a child of the parent object.");
                            
                            prefabManager.prefabGroups[g].newGameObject = null;

                            if (instantiatePrefabsAsAdded)
                            {
                                prefabManager.ActivateGroup(g);
                            }
                        }
                        EditorGUILayout.EndVertical();*/
                        EditorGUILayout.EndHorizontal();
                    }
                }
                GUI.backgroundColor = Color.white;
                EditorGUILayout.EndVertical();
            }
            
            GUI.backgroundColor =  Color.white;
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

    private void RemoveInGameObjectLinks(PrefabChildManager prefabManager)
    {
        foreach (PrefabGroup group in prefabManager.prefabGroups)
        {
            foreach (PrefabObject obj in group.prefabObjects)
            {
                obj.inGameObject = null;
            }
        }
    }

    private void UpdateAllObjects(PrefabChildManager prefabManager, GameObject oldObject, GameObject newObject)
    {
        foreach (PrefabGroup group in prefabManager.prefabGroups)
        {
            foreach (PrefabObject obj in group.prefabObjects)
            {
                if (obj.prefab == oldObject)
                    obj.prefab = newObject;
            }
        }
    }

    private void UpdateAllTransforms(PrefabChildManager prefabManager, Transform transform)
    {
        foreach (PrefabGroup group in prefabManager.prefabGroups)
        {
            foreach (PrefabObject obj in group.prefabObjects)
            {
                obj.parentTransform = transform;
            }
        }
    }
    
    private void RemoveObject(PrefabChildManager prefabManager,PrefabGroup prefabGroup, PrefabObject prefabObject, int i)
    {
        GameObject inGameObject = prefabObject.inGameObject;
        if (prefabObject.isPrefab && inGameObject)
            prefabManager.DestroyObject(inGameObject);
        else if (!prefabObject.isPrefab && inGameObject)
            inGameObject.SetActive(false);
        prefabGroup.prefabObjects.RemoveAt(i);
    }

    private void CheckOptionsAreSet(PrefabChildManager prefabManager, int g, int i)
    {
        PrefabObject prefabObject = prefabManager.prefabGroups[g].prefabObjects[i];
        
        if (prefabObject.parentTransform == null)
        {
            prefabObject.parentTransform = prefabManager.thisTransform;
        }
    }

    private void RelinkObjects(PrefabChildManager prefabManager)
    {
        Debug.Log("Begin Relink");
        for (int g = 0; g < prefabManager.prefabGroups.Count; g++)
        {
            PrefabGroup prefabGroup = prefabManager.prefabGroups[g];
            //Debug.Log("Group " + prefabGroup.name);
            for (int i = 0; i < prefabGroup.prefabObjects.Count; i++)
            {
                PrefabObject prefabObject = prefabGroup.prefabObjects[i];
                //Debug.Log("Object " + prefabObject.prefab.name);
                
                if (prefabObject.prefab.transform.IsChildOf(prefabObject.parentTransform))
                {
                    string name = prefabObject.prefab.name;
                    GameObject foundObject = FindGameObject(name, prefabManager);
                    if (foundObject != null)
                    {
                        Debug.Log("Found the object " + foundObject.name);
                        prefabObject.prefab = foundObject;
                        prefabObject.parentTransform = prefabManager.gameObject.transform;
                    }
                }
            }
        }
    }

    private GameObject FindGameObject(string name, PrefabChildManager prefabManager)
    {
        Transform[] gameObjects = prefabManager.gameObject.GetComponentsInChildren<Transform>(true);
        
        foreach (Transform child in gameObjects)
        {
            if (child.name == name)
                return child.gameObject;
        }

        Debug.Log("Warning: Did not find a child named " + name + "! This re-link will be skipped.");
        return null;
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
