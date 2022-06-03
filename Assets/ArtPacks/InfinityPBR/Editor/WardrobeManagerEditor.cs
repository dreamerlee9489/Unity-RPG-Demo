using UnityEngine;
using UnityEditor;
using System;
using InfinityPBR;
using UnityEditor.PackageManager.Requests;

[CustomEditor(typeof(WardrobeManager))]
[CanEditMultipleObjects]
[Serializable]
public class WardrobeManagerEditor : Editor
{

    private bool showHelpBoxes = true;
    private bool showSetup = true;
    private bool showFullInspector = false;
    private bool showWardrobeParents = true;
    private bool showOrphanWardrobe = true;
    private bool showWardrobeGroups = true;
    private bool showBlendShapeAdd = true;
    private Color inactiveColor2 = new Color(0.75f, .75f, 0.75f, 1f);
    private Color activeColor = new Color(0.75f, 1f, 0.75f, 1f);
    private Color activeColor2 = new Color(0.25f, 1f, 0.25f, 1f);

    private int blendShapeObjectIndex = 0;
    private int blendShapeItemIndex = 0;
    private string blendShapeObjectName = "";
    private string blendShapeItemName = "";
    private float blendShapeSliderA = 0f;
    private float blendShapeSliderD = 0f;
    
    public override void OnInspectorGUI()
    {
        WardrobeManager wardrobeManager = (WardrobeManager) target;

        // Keep parent objects on
        for (int i = 0; i < wardrobeManager.wardrobeParents.Count; i++)
            wardrobeManager.wardrobeParents[i].SetActive(true);

        showSetup = EditorGUILayout.Foldout(showSetup, "Setup & Options");
        if (showSetup)
        {
            showHelpBoxes = EditorGUILayout.Toggle("Show Help Boxes", showHelpBoxes);
            showFullInspector = EditorGUILayout.Toggle("Show Full Inspector", showFullInspector);
            
            if (showHelpBoxes)
            {
                EditorGUILayout.HelpBox("WARDROBE MANAGER\n" +
                    "Use this script to easily set up and manage large modular wardrobe options for a character.\n\n" +
                    "Use the following methods to manage the wardrobe at runtime:\n\n" +
                    "NextWardrobe(1) - Activate the next wardrobe group\n" +
                    "NextWardrobe(-1) - Activate the previous wardrobe group\n" +
                    "RandomWardrobe() - Activate a random wardrobe group\n" +
                    "ToggleWardrobe(string name) - Turn on a wardrobe group by name\n" +
                    "ToggleWardrobe(int i) - Turn on a wardrobe group by index", MessageType.None);
            }
            
            /* ------------------------------------------------------------------------------------------
             WARDROBE PARENTS
             ------------------------------------------------------------------------------------------*/
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Wardrobe Parents", EditorStyles.boldLabel);
            if (showHelpBoxes)
            {
                EditorGUILayout.HelpBox("Each \"Wardrobe Parent\" is an empty GameObject in the hierarchy of your character " +
                                        "which contains wardrobe objects. Parents are often used for organizing many " +
                                        "wardrobe options under a character. They should not, themselves, be wardrobe.\n\n" +
                                        "Add Wardrobe Parents here, and first generation children will be considered " +
                                        "wardrobe for the purposes of this script.", MessageType.Info);
            }

            GameObject newParentObject = null;
            newParentObject = EditorGUILayout.ObjectField("Add New Wardrobe Parent", newParentObject, typeof(GameObject), true) as GameObject;

            showWardrobeParents = EditorGUILayout.Foldout(showWardrobeParents, wardrobeManager.wardrobeParents.Count + " Wardrobe Parents");
            if (showWardrobeParents)
            {
                for (int p = 0; p < wardrobeManager.wardrobeParents.Count; p++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("", GUILayout.Width(10));
                    Undo.RecordObject (wardrobeManager, "Remove Wardrobe Parent");
                    if (GUILayout.Button("X", GUILayout.Width(25)))
                    {
                        wardrobeManager.wardrobeParents.RemoveAt(p);
                    }
                    else
                    {
                        Undo.RecordObject (wardrobeManager, "Toggle On Wardrobe Parent " + p);
                        EditorGUILayout.LabelField(wardrobeManager.wardrobeParents[p].name);
                        
                        
                        Undo.RecordObject (wardrobeManager, "Toggle On Wardrobe Parent " + p);
                        if (GUILayout.Button("All On", GUILayout.Width(50)))
                            wardrobeManager.ToggleAllWardrobeInParent(wardrobeManager.wardrobeParents[p], true);
                        Undo.RecordObject (wardrobeManager, "Toggle Off Wardrobe Parent " + p);
                        if (GUILayout.Button("All Off", GUILayout.Width(50)))
                            wardrobeManager.ToggleAllWardrobeInParent(wardrobeManager.wardrobeParents[p], false);
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }


            
            /* ------------------------------------------------------------------------------------------
             OPRHAN WARDROBE
             ------------------------------------------------------------------------------------------*/ 
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Orphan Wardrobe", EditorStyles.boldLabel);
            if (showHelpBoxes)
            {
                EditorGUILayout.HelpBox("Wardrobe objects which are not a child of a Wardrobe Parent can be specified here. " +
                                        "Each one is considered wardrobe for the purposes of this script.", MessageType.Info);
            }
            
            GameObject newOrphanWardrobe = null;
            newOrphanWardrobe = EditorGUILayout.ObjectField("Add New Orphan Wardrobe", newOrphanWardrobe, typeof(GameObject), true) as GameObject;

            showOrphanWardrobe = EditorGUILayout.Foldout(showOrphanWardrobe, wardrobeManager.orphanWardrobe.Count + " Orphan Wardrobe GameObjects");
            
             
            if (showOrphanWardrobe)
            {
                for (int o = 0; o < wardrobeManager.orphanWardrobe.Count; o++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("", GUILayout.Width(10));

                    if (wardrobeManager.orphanWardrobe[o].activeSelf)
                    {
                        Undo.RecordObject (wardrobeManager, "Turn Object Off");
                        if (GUILayout.Button("Turn Off", GUILayout.Width(80)))
                            wardrobeManager.orphanWardrobe[o].SetActive(false);
                    }
                    else
                    {
                        Undo.RecordObject (wardrobeManager, "Turn Object On");
                        if (GUILayout.Button("Turn On", GUILayout.Width(80)))
                            wardrobeManager.orphanWardrobe[o].SetActive(true);
                    }
                    
                    
                    Undo.RecordObject (wardrobeManager, "Remove Orphan Wardrobe");
                    if (GUILayout.Button("X", GUILayout.Width(25)))
                    {
                        wardrobeManager.RemoveOrphanObject(wardrobeManager.orphanWardrobe[o]);
                    }
                    else
                    {
                        wardrobeManager.orphanWardrobe[o] = EditorGUILayout.ObjectField(wardrobeManager.orphanWardrobe[o], typeof(GameObject),true) as GameObject;
                    
                        Undo.RecordObject (wardrobeManager, "Add To Wardrobe Groups");
                        if (GUILayout.Button("Add to All", GUILayout.Width(100)))
                            wardrobeManager.AddGameObjectToAll(wardrobeManager.orphanWardrobe[o], true);
                        Undo.RecordObject (wardrobeManager, "Remove From Wardrobe Groups");
                        if (GUILayout.Button("Remove from All", GUILayout.Width(100)))
                            wardrobeManager.AddGameObjectToAll(wardrobeManager.orphanWardrobe[o], false);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            
            /* ------------------------------------------------------------------------------------------
             CODE / MANAGEMENT
             ------------------------------------------------------------------------------------------*/
            if (newParentObject)
            {
                Undo.RecordObject (wardrobeManager, "Add Wardrobe Parent");
                wardrobeManager.AddWardrobeParent(newParentObject);
            }
        
            if (newOrphanWardrobe)
            {
                Undo.RecordObject (wardrobeManager, "Add Orphan Wardrobe");
                wardrobeManager.AddOrphanWardrobe(newOrphanWardrobe);
            }
        }
        
        


        /* ------------------------------------------------------------------------------------------
         WARDROBE GROUPS
         ------------------------------------------------------------------------------------------*/
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Wardrobe Groups", EditorStyles.boldLabel);
        if (showHelpBoxes)
        {
            EditorGUILayout.HelpBox("Each \"Wardrobe Group\" could be considered a single outfit, and can be named. " +
                                    "Add, manage, and delete Wardrobe Groups here. Populate Parent Wardrobe objecets " +
                                    "and Orphan Wardrobe before adding Wardrobe Groups.\n\nTurn on any wardrobe " +
                                    "in the hierarchy that you'd like to include in a new group. Pressing \"Create New Group\" " +
                                    "will create a new Wardrobe Group with all the active wardrobe.\nActivate a wardrobe " +
                                    "with the \"Activate\" button. That wardrobe will show in green. The \"Update\" button " +
                                    "by the active wardrobe will repopulate the wardrobe with the current look of the " +
                                    "character.", MessageType.Info);
        }

        if (GUILayout.Button("Create New Group"))
        {
            wardrobeManager.CreateNewWardrobeGroup();
        }
        
        showWardrobeGroups = EditorGUILayout.Foldout(showWardrobeGroups, wardrobeManager.wardrobeGroups.Count + " Wardrobe Groups");
        if (showWardrobeGroups)
        {
            for (int g = 0; g < wardrobeManager.wardrobeGroups.Count; g++)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.BeginHorizontal();
                if (wardrobeManager.groupIndex == g)
                    GUI.backgroundColor = activeColor;
                EditorGUILayout.LabelField("", GUILayout.Width(10));
                Undo.RecordObject (wardrobeManager, "Remove Wardrobe Group");
                if (GUILayout.Button("X", GUILayout.Width(25)))
                {
                    EditorGUILayout.EndHorizontal();
                    if (g == wardrobeManager.groupIndex)
                    {
                        if (g > 0)
                            wardrobeManager.ActivateWardrobe(g - 1);
                        else if (wardrobeManager.wardrobeGroups.Count > 1)
                        {    
                            wardrobeManager.ActivateWardrobe(1);
                            wardrobeManager.groupIndex = 0;
                        }
                        else
                            wardrobeManager.groupIndex = 9999;
                    }
                    else if (g < wardrobeManager.groupIndex)
                    {
                        wardrobeManager.groupIndex -= 1;
                    }
                    
                    wardrobeManager.wardrobeGroups.RemoveAt(g);
                }
                else
                {
                    Undo.RecordObject (wardrobeManager, "Change Wardrobe Group Name");
                    wardrobeManager.wardrobeGroups[g].name = EditorGUILayout.TextField(wardrobeManager.wardrobeGroups[g].name, GUILayout.Width(180));

                    Undo.RecordObject (wardrobeManager, "Toggle Wardrobe Group Objects On");
                    if (GUILayout.Button("Activate", GUILayout.Width(80)))
                    {
                        wardrobeManager.ActivateWardrobe(g);
                    }

                    if (wardrobeManager.wardrobeGroups[g].showWardrobeObjects)
                    {
                        GUI.backgroundColor = wardrobeManager.groupIndex == g ? activeColor2 : inactiveColor2;
                        Undo.RecordObject (wardrobeManager, "Toggle Wardrobe Group Show Objects");
                        if (GUILayout.Button("Objects", GUILayout.Width(80)))
                        {
                            wardrobeManager.wardrobeGroups[g].showWardrobeObjects = false;
                        }
                        GUI.backgroundColor = wardrobeManager.groupIndex == g ? activeColor : Color.white;
                    }
                    else
                    {
                        Undo.RecordObject (wardrobeManager, "Toggle Wardrobe Group Show Objects");
                        if (GUILayout.Button("Objects", GUILayout.Width(80)))
                        {
                            wardrobeManager.wardrobeGroups[g].showBlendShapes = false;
                            wardrobeManager.wardrobeGroups[g].showWardrobeObjects = true;
                        }
                    }
                    
                    if (wardrobeManager.wardrobeGroups[g].showBlendShapes && wardrobeManager.gameObject.GetComponent<SFB_BlendShapesManager>())
                    {
                        
                        GUI.backgroundColor = wardrobeManager.groupIndex == g ? activeColor2 : inactiveColor2;
                        Undo.RecordObject (wardrobeManager, "Toggle Wardrobe Group Show Blend Shapes");
                        if (GUILayout.Button("Blend Shapes", GUILayout.Width(120)))
                        {
                            wardrobeManager.wardrobeGroups[g].showBlendShapes = false;
                        }
                        GUI.backgroundColor = wardrobeManager.groupIndex == g ? activeColor : Color.white;
                    }
                    else
                    {
                        Undo.RecordObject (wardrobeManager, "Toggle Wardrobe Group Show Blend Shapes");
                        if (GUILayout.Button("Blend Shapes", GUILayout.Width(120)))
                        {
                            wardrobeManager.wardrobeGroups[g].showWardrobeObjects = false;
                            wardrobeManager.wardrobeGroups[g].showBlendShapes = true;
                        }
                    }

                    if (wardrobeManager.groupIndex == g)
                    {
                        Undo.RecordObject (wardrobeManager, "Update Wardrobe Group");
                        if (GUILayout.Button("Update Group", GUILayout.Width(100)))
                        {
                            wardrobeManager.UpdateWardrobeGroup(g);
                        }
                    }

                    EditorGUILayout.EndHorizontal();
                    
                    /*
                     * SHOW WARDROBE OBJECTS
                     */
                    if (wardrobeManager.wardrobeGroups[g].showWardrobeObjects)
                    {
                        if (showHelpBoxes)
                        {
                            EditorGUILayout.HelpBox("Press \"X\" to remove an object from the list. Undo works " +
                                                    "here, but you can also press \"Update Group\" above to repopulate " +
                                                    "the list entirely. You can replace an object and/or texture here " +
                                                    "as well.", MessageType.Info);
                        }
                        for (int i = 0; i < wardrobeManager.wardrobeGroups[g].wardrobe.Count; i++)
                        {
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("", GUILayout.Width(30));
                            Undo.RecordObject (wardrobeManager, "Remove Wardrobe Group GameObject");
                            if (GUILayout.Button("X", GUILayout.Width(25)))
                            {
                                EditorGUILayout.EndHorizontal();
                                wardrobeManager.wardrobeGroups[g].wardrobe.RemoveAt(i);
                            }
                            else
                            {
                                Undo.RecordObject (wardrobeManager, "Update Game Object In Group");

                                GameObject oldObject = wardrobeManager.wardrobeGroups[g].wardrobe[i];
                                wardrobeManager.wardrobeGroups[g].wardrobe[i] = EditorGUILayout.ObjectField(wardrobeManager.wardrobeGroups[g].wardrobe[i], typeof(GameObject), true) as GameObject;
                                if (oldObject != wardrobeManager.wardrobeGroups[g].wardrobe[i])
                                {
                                    if (wardrobeManager.CountThisObjectInGroup(wardrobeManager.wardrobeGroups[g].wardrobe[i],
                                            wardrobeManager.wardrobeGroups[g]) > 1)
                                    {
                                        wardrobeManager.wardrobeGroups[g].wardrobe[i] = oldObject;
                                        Debug.LogWarning("Warning: Objects can only be in the group once. Replacement has been blocked.");
                                    }
                                    else
                                    {
                                        if (wardrobeManager.groupIndex == g)
                                        {
                                            oldObject.SetActive(false);
                                            wardrobeManager.wardrobeGroups[g].wardrobe[i].SetActive(true);
                                        }
                                        
                                        
                                        // Reset SkinnedMeshRenderer & Material
                                        if (wardrobeManager.GetSkinnedMeshRenderer(wardrobeManager.wardrobeGroups[g].wardrobe[i]) != null)
                                        {
                                            wardrobeManager.wardrobeGroups[g].skinnedMeshRenderers[i] = wardrobeManager.GetSkinnedMeshRenderer(wardrobeManager.wardrobeGroups[g].wardrobe[i]);
                                            wardrobeManager.wardrobeGroups[g].materials[i] = wardrobeManager.wardrobeGroups[g].skinnedMeshRenderers[i].sharedMaterial;
                                        } 
                                        else if (wardrobeManager.GetMeshRenderer(wardrobeManager.wardrobeGroups[g].wardrobe[i]) != null)
                                        {
                                            wardrobeManager.wardrobeGroups[g].meshRenderers[i] = wardrobeManager.GetMeshRenderer(wardrobeManager.wardrobeGroups[g].wardrobe[i]);
                                            wardrobeManager.wardrobeGroups[g].materials[i] = wardrobeManager.wardrobeGroups[g].meshRenderers[i].sharedMaterial;
                                        }
                                    }
                                        
                                }
                                
                                
                                Undo.RecordObject (wardrobeManager, "Update Material In Group");
                                wardrobeManager.wardrobeGroups[g].materials[i] = EditorGUILayout.ObjectField(wardrobeManager.wardrobeGroups[g].materials[i], typeof(Material), false) as Material;

                                if (wardrobeManager.groupIndex == g && wardrobeManager.wardrobeGroups[g].showWardrobeObjects)
                                {
                                    
                                    // SkinnedMeshRenderer -- or -- MeshRenderer
                                    if (wardrobeManager.wardrobeGroups[g].skinnedMeshRenderers[i] != null)
                                    {
                                        if (wardrobeManager.wardrobeGroups[g].skinnedMeshRenderers[i].sharedMaterial != wardrobeManager.wardrobeGroups[g].materials[i])
                                        {
                                            wardrobeManager.wardrobeGroups[g].skinnedMeshRenderers[i].sharedMaterial = wardrobeManager.wardrobeGroups[g].materials[i];
                                        }
                                    } 
                                    else if (wardrobeManager.wardrobeGroups[g].meshRenderers[i] != null)
                                    {
                                        if (wardrobeManager.wardrobeGroups[g].meshRenderers[i].sharedMaterial != wardrobeManager.wardrobeGroups[g].materials[i])
                                        {
                                            wardrobeManager.wardrobeGroups[g].meshRenderers[i].sharedMaterial = wardrobeManager.wardrobeGroups[g].materials[i];
                                        }
                                    }
                                    
                                }
                                
                                EditorGUILayout.EndHorizontal();
                            }
                        }
                    }
                    
                    /*
                     * SHOW BLEND SHAPES
                     */
                    if (wardrobeManager.wardrobeGroups[g].showBlendShapes && wardrobeManager.gameObject.GetComponent<SFB_BlendShapesManager>())
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("", GUILayout.Width(10));
                        showBlendShapeAdd = EditorGUILayout.Foldout(showBlendShapeAdd, "Add New Blend Shape");
                        EditorGUILayout.EndHorizontal();
                        if (showBlendShapeAdd)
                        {
                            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                            
                            // Add new shapes
                            int objectCount = wardrobeManager.gameObject.GetComponent<SFB_BlendShapesManager>().blendShapeObjects.Count;
                            string[] blendShapeObjects = new string[objectCount];
                            for (int i = 0; i < objectCount; i++)
                            {
                                blendShapeObjects[i] = wardrobeManager.gameObject.GetComponent<SFB_BlendShapesManager>()
                                    .blendShapeObjects[i].name;
                            }

                            int oldObjIndex = blendShapeObjectIndex;
                            blendShapeObjectIndex = EditorGUILayout.Popup("Blend Shape Object", blendShapeObjectIndex, blendShapeObjects);
                            blendShapeObjectName = wardrobeManager.gameObject.GetComponent<SFB_BlendShapesManager>()
                                .blendShapeObjects[blendShapeObjectIndex].name;
                            if (oldObjIndex != blendShapeObjectIndex)
                            {
                                blendShapeItemIndex = 0;
                            }
                            
                            int itemCount = wardrobeManager.gameObject.GetComponent<SFB_BlendShapesManager>().blendShapeObjects[blendShapeObjectIndex].blendShapes.Count;
                            int primaryIndex = 0;
                            
                            
                            string[] blendShapes = new string[itemCount];
                            for (int i = 0; i < wardrobeManager.gameObject.GetComponent<SFB_BlendShapesManager>().blendShapeObjects[blendShapeObjectIndex].blendShapes.Count; i++)
                            {
                                if (wardrobeManager.gameObject.GetComponent<SFB_BlendShapesManager>()
                                    .blendShapeObjects[blendShapeObjectIndex].blendShapes[i].isVisible)
                                {
                                    var displayName = wardrobeManager.gameObject.GetComponent<SFB_BlendShapesManager>()
                                        .blendShapeObjects[blendShapeObjectIndex].blendShapes[i].name;
                                    if (wardrobeManager.gameObject.GetComponent<SFB_BlendShapesManager>()
                                        .blendShapeObjects[blendShapeObjectIndex].blendShapes[i].isPlus)
                                    {
                                        displayName = displayName.Replace("Plus", "");
                                        displayName = displayName.Replace("plus", "");
                                    }
                                
                                    blendShapes[i] = displayName;
                                    primaryIndex++;
                                }
                            }
                            int oldItemIndex = blendShapeItemIndex;
                            blendShapeItemIndex = EditorGUILayout.Popup("Blend Shape", blendShapeItemIndex, blendShapes);
                            if (oldItemIndex != blendShapeItemIndex)
                            {
                                // Reset values
                                blendShapeSliderA = wardrobeManager.gameObject.GetComponent<SFB_BlendShapesManager>()
                                    .blendShapeObjects[blendShapeObjectIndex].blendShapes[blendShapeItemIndex].value;
                                blendShapeSliderD = wardrobeManager.gameObject.GetComponent<SFB_BlendShapesManager>()
                                    .blendShapeObjects[blendShapeObjectIndex].blendShapes[blendShapeItemIndex].value;
                            }
                            blendShapeItemName = wardrobeManager.gameObject.GetComponent<SFB_BlendShapesManager>()
                                .blendShapeObjects[blendShapeObjectIndex].blendShapes[blendShapeItemIndex].name;

                            if (wardrobeManager.gameObject.GetComponent<SFB_BlendShapesManager>()
                                .blendShapeObjects[blendShapeObjectIndex].blendShapes[blendShapeItemIndex].isPlus)
                            {
                                blendShapeSliderA = EditorGUILayout.Slider("Value on Activate", blendShapeSliderA, -100f, 100f);  
                            }
                            else
                            {
                                blendShapeSliderA = EditorGUILayout.Slider("Value on Activate", blendShapeSliderA, 0f, 100f);
                            }
                            
                            if (wardrobeManager.gameObject.GetComponent<SFB_BlendShapesManager>()
                                .blendShapeObjects[blendShapeObjectIndex].blendShapes[blendShapeItemIndex].isPlus)
                            {
                                blendShapeSliderD = EditorGUILayout.Slider("Value on Deactivate", blendShapeSliderD, -100f, 100f);  
                            }
                            else
                            {
                                blendShapeSliderD = EditorGUILayout.Slider("Value on Deactivate", blendShapeSliderD, 0f, 100f);
                            }
                            
                            if (GUILayout.Button("Add Blend Shape"))
                            {
                                wardrobeManager.wardrobeGroups[g].blendShapes.Add(new WardrobeBlendShapes());
                                WardrobeBlendShapes newShapes = wardrobeManager.wardrobeGroups[g]
                                    .blendShapes[wardrobeManager.wardrobeGroups[g].blendShapes.Count - 1];

                                newShapes.blendShapeObjectIndex = blendShapeObjectIndex;
                                newShapes.blendShapeObjectName = blendShapeObjectName;
                                newShapes.blendShapeIndex = blendShapeItemIndex;
                                newShapes.blendShapeName = blendShapeItemName;
                                newShapes.deactivateValue = blendShapeSliderD;
                                newShapes.activateValue = blendShapeSliderA;
                            }
                            
                            
                            EditorGUILayout.EndVertical();
                        }
                        // Show list of existing shapes
                        for (int b = 0; b < wardrobeManager.wardrobeGroups[g].blendShapes.Count; b++)
                        {
                            WardrobeBlendShapes shape = wardrobeManager.wardrobeGroups[g].blendShapes[b];
                            
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("", GUILayout.Width(30));
                            Undo.RecordObject (wardrobeManager, "Remove BlendShape");
                            if (GUILayout.Button("X", GUILayout.Width(25)))
                            {
                                wardrobeManager.wardrobeGroups[g].blendShapes.RemoveAt(b);
                                EditorGUILayout.EndHorizontal();
                            }
                            else
                            {

                                EditorGUILayout.LabelField(shape.blendShapeObjectName + "." + shape.blendShapeName, GUILayout.Width(200));
                                EditorGUILayout.LabelField("Activate: " + shape.activateValue, GUILayout.Width(100));
                                EditorGUILayout.LabelField("Deactivate: " + shape.deactivateValue, GUILayout.Width(100));

                                EditorGUILayout.EndHorizontal();
                            }
                        }
                    }
                }
                GUI.backgroundColor = Color.white;
                EditorGUILayout.EndVertical();
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
}
