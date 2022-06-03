using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class IPBR_CharacterEquip
{
    public static GameObject target;                                                   // The target to match
    public static string rootBoneName = "BoneRoot";                            // Name of the root bone structure [IMPORTANT AND MAY BE DIFFERENT FOR YOUR MODEL!]
    public static SkinnedMeshRenderer targetRenderer;                          // Renderer we are targetting
    public static string subRootBoneName;                                      // Bone name
    public static GameObject thisBoneRoot;                                     // Current bone root
    
    public static void EquipCharacter(GameObject gameObject)
    {
        TryDebugMessage("Starting Character Equip Workflow.");
        Dictionary<string, Transform> boneMap;                              // Holds all the bones
        boneMap = new Dictionary<string, Transform>();                      // Create the new dictionary
        target = gameObject;                                                // Set the target to the selected object
        
        //TryDebugMessage("Target is " +target.name);

        if (MapBones(boneMap))                                              // If the object is proper, we will find and equip the valid attached equipment.
        {
            Transform[] children = target.GetComponentsInChildren<Transform>(true);
            
            
            //foreach (Transform child in target.transform)                   // For each child of the target
            foreach (Transform child in children)                               // For each child of the target
            {
                if (!child || child.gameObject == target)        // Skip if it's the parent object, or if we've already destroyed this transform.
                    continue;

                SkinnedMeshRenderer subChildRenderer = IsEquipmentObject(child);

                if (subChildRenderer)                  // If this is valid equipment and we have a subChildRenderer...
                {
                    //TryDebugMessage(child.name + " is valid equipment!");
                    Transform[] boneArray = subChildRenderer.bones;         // Set the boneArray to be all the bones from the subChildRenderer
                    for (int i = 0; i < boneArray.Length; i++)              // For each bone...
                    {
                        string boneName = boneArray[i].name;                // Get the bone name
                        if (boneMap.ContainsKey(boneName))                  // if the dictionary for the target bones contains this bone name...
                        {
                            boneArray[i] = boneMap[boneName];               // Set the array to match the bone from the Dictionary
                        }
                    }

                    subChildRenderer.bones = boneArray;                     // Assing the boneArray to the bones
                    foreach (Transform bone in targetRenderer.bones)        // For each bone...
                    {
                        if (bone.name == subRootBoneName)                   // Is the bone name the same as subRootBoneName?
                        {
                            subChildRenderer.rootBone = bone;               // Assign the root bone to this
                        }
                    }

                    if (PrefabUtility.IsAnyPrefabInstanceRoot(child.gameObject))
                        PrefabUtility.UnpackPrefabInstance(child.gameObject,PrefabUnpackMode.Completely,InteractionMode.AutomatedAction);
                    //DestroyImmediate(thisBoneRoot, true);                   // Destroy the bones of the subChildRenderer
#if UNITY_EDITOR
                    UnityEngine.Object.DestroyImmediate(thisBoneRoot);
#else
                    UnityEngine.Object.Destroy(child.thisBoneRoot);
#endif
                }
            }

            // Finally, we will remove "Dummy001" from the objects.
            Transform[] allChildren = target.GetComponentsInChildren<Transform>();   // Get all children of the target
            foreach (Transform child in allChildren)                        // And for each one...
            {
                if (child.gameObject.name == "Dummy001")                    // If the name is Dummy001
                {
                    if (PrefabUtility.IsAnyPrefabInstanceRoot(child.gameObject)) 
                        PrefabUtility.UnpackPrefabInstance(child.gameObject,PrefabUnpackMode.Completely,InteractionMode.AutomatedAction);
                    //DestroyImmediate(child.gameObject);                     // Destroy it
                    #if UNITY_EDITOR
                    UnityEngine.Object.DestroyImmediate(child.gameObject);
                    #else
                    UnityEngine.Object.Destroy(child.gameObject);
                    #endif
                }
            }
        }
        TryDebugMessage("Character Equip Complete!!");
    }

    public static bool MapBones(Dictionary<string, Transform> boneMap)
    {
        // Make sure there is a proper root bone structure
        bool isValidObject = false;                                         // Assume false
        foreach (Transform childCheck in target.transform)                  // For each child transform
        {
            if (childCheck.name == rootBoneName)                            // if this child name is the rootBoneName
                isValidObject = true;                                       // Make this a valid object
            if (childCheck.GetComponent<SkinnedMeshRenderer>())               // If this has a skinnedMeshRenderer
            {
                targetRenderer = childCheck.GetComponent<SkinnedMeshRenderer>();  // Set targetRenderer
                foreach (Transform bone in targetRenderer.bones)            // For each bone...
                {
                    boneMap[bone.name] = bone;                              // Add the bone to the dictionary
                }
            }
        }

        return isValidObject;
    }

    public static SkinnedMeshRenderer IsEquipmentObject(Transform child)
    {
        SkinnedMeshRenderer subChildRenderer = null;                // Renderer of the subChild

        foreach (Transform subChild in child.transform)             // For each child of the child
        {
            if (subChild.name == rootBoneName)                      // If the subChild name is the rootBoneName
                thisBoneRoot = subChild.gameObject;                 // This is the root
            
            if (subChild.GetComponent<SkinnedMeshRenderer>())       // If the subChild has a skinnedMeshRenderer
            {
                // If the subChild has a SkinnedMeshRenderer, assign the renderer, bone name, and target;
                if (subChildRenderer = subChild.gameObject.GetComponent<SkinnedMeshRenderer>())
                {
                    if (subChildRenderer.rootBone)
                    {
                        subRootBoneName = subChildRenderer.rootBone.name;
                        targetRenderer = subChildRenderer;
                    }
                }
                        
            }
        }

        return subChildRenderer;
    }
    
    public static void TryDebugMessage(string message)
    {
        #if UNITY_EDITOR
        Debug.Log(message);
        #endif
    }
}
