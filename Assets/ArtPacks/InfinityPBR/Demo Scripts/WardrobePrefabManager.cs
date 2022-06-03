using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InfinityPBR
{
    [RequireComponent(typeof(PrefabChildManager))]
    [RequireComponent(typeof(BlendShapesManager))]
    [System.Serializable]
    public class WardrobePrefabManager : MonoBehaviour
    {
        public bool autoRigWhenActivated = true;
        public bool handleBlendShapes = true;
        public BlendShapesManager blendShapesManager;
        [HideInInspector] public PrefabChildManager prefabChildManager;
        

        public List<BlendShapeGroup> blendShapeGroups = new List<BlendShapeGroup>();
        public List<BlendShapeObject> blendShapeObjects = new List<BlendShapeObject>();
        
        public void OnActivate(string name)
        {
            SetBlendShapes();
            List<BlendShapeItem> blendShapeItems = GetBlendShapeItems("Activate", name);
            if (blendShapeItems != null)
            {
                for (int i = 0; i < blendShapeItems.Count; i++)
                {
                    var revertResults = RevertOnDeactivate(blendShapeItems[i].triggerName, name);
                    bool hasRevert = revertResults.Item1;
                    int revertIndex = revertResults.Item2;
                    if (hasRevert)
                    {
                        BlendShapeItem revertItem = blendShapeGroups[GetGroupIndex(name)].onDeactivate[revertIndex];
                        revertItem.value = blendShapesManager.GetBlendShapeValue(revertItem.objectName, revertItem.triggerName).value;
                    }

                    float value = blendShapeItems[i].value;
                    float currentValue = blendShapesManager.GetBlendShapeValue(blendShapeItems[i].objectName, blendShapeItems[i].triggerName).value;
                    if (blendShapeItems[i].actionType == "Less than" && currentValue <= value)
                        value = currentValue;
                    if (blendShapeItems[i].actionType == "Greater than" && currentValue >= value)
                        value = currentValue;
                
                    TriggerBlendShape(blendShapeItems[i].triggerName, value);
                }
            }
            
        }

        public void OnDeactivate(string name)
        {
            SetBlendShapes();
            List<BlendShapeItem> blendShapeItems = GetBlendShapeItems("Deactivate", name);
            if (blendShapeItems != null)
            {
                for (int i = 0; i < blendShapeItems.Count; i++)
                {
                    TriggerBlendShape(blendShapeItems[i].triggerName, blendShapeItems[i].value);
                }
            }
            
        }

        private (bool, int) RevertOnDeactivate(string triggerName, string name)
        {
            bool hasRevert = false;
            int revertIndex = 0;
            List<BlendShapeItem> blendShapeItems = GetBlendShapeItems("Deactivate", name);
            for (int i = 0; i < blendShapeItems.Count; i++)
            {
                if (blendShapeItems[i].triggerName == triggerName)
                {
                    if (blendShapeItems[i].revertBack)
                    {
                        hasRevert = true;
                        revertIndex = i;
                    }
                }
            }

            return (hasRevert, revertIndex);
        }

        private bool IsGroupInList(string name)
        {
            for (int i = 0; i < blendShapeGroups.Count; i++)
            {
                if (blendShapeGroups[i].name == name)
                    return true;
            }

            return false;
        }
        
        private bool IsGroupInManager(string name)
        {
            for (int i = 0; i <  prefabChildManager.prefabGroups.Count; i++)
            {
                if (prefabChildManager.prefabGroups[i].name == name)
                    return true;
            }

            return false;
        } 
        
        private List<BlendShapeItem> GetBlendShapeItems(string eventType, string name)
        {
            for (int i = 0; i < blendShapeGroups.Count; i++)
            {
                if (blendShapeGroups[i].name == name)
                {
                    if (eventType == "Activate")
                        return blendShapeGroups[i].onActivate;
                    if (eventType == "Deactivate")
                        return blendShapeGroups[i].onDeactivate;
                }
            }

            return null;
        }

        public void UpdateGroupList()
        {
            // Add groups we don't have yet
            for (int g = 0; g < prefabChildManager.prefabGroups.Count; g++)
            {
                if (!IsGroupInList(prefabChildManager.prefabGroups[g].name))
                {
                    blendShapeGroups.Add(new BlendShapeGroup());
                    blendShapeGroups[blendShapeGroups.Count - 1].name = prefabChildManager.prefabGroups[g].name;
                    
                }
            }

            // Remove orphan groups
            for (int i = blendShapeGroups.Count - 1; i >= 0; i--)
            {
                if (!IsGroupInManager(blendShapeGroups[i].name))
                {
                    blendShapeGroups.RemoveAt(i);
                }
            }

            PopulateGroupBlendShapeNames();
        }

        public void PopulateGroupBlendShapeNames()
        {
            for (int g = blendShapeGroups.Count - 1; g >= 0; g--)
            {
                blendShapeGroups[g].actualBlendShapeNames.Clear();
                blendShapeGroups[g].blendShapeNames.Clear();
                blendShapeGroups[g].blendShapeObjectName.Clear();
                for (int i = 0;
                    i < prefabChildManager.prefabGroups[GetGroupIndex(blendShapeGroups[g].name)].prefabObjects.Count; i++)
                {
                    PrefabObject prefabObject = prefabChildManager.prefabGroups[GetGroupIndex(blendShapeGroups[g].name)].prefabObjects[i];
                    SkinnedMeshRenderer[] renderers = prefabObject.prefab.GetComponentsInChildren<SkinnedMeshRenderer>(true);
                    foreach (SkinnedMeshRenderer renderer in renderers)
                    {
                        for (int b = 0; b < renderer.sharedMesh.blendShapeCount; b++)
                        {
                            string newName = renderer.sharedMesh.GetBlendShapeName(b);

                            if (newName.Contains(blendShapesManager.blendShapePrimaryPrefix))
                            {
                                if (!newName.Contains(blendShapesManager.plusMinus.Item2))
                                {
                                    blendShapeGroups[g].actualBlendShapeNames.Add(newName);
                                    newName = newName.Replace(blendShapesManager.plusMinus.Item1, "");
                                    newName = newName.Replace(blendShapesManager.blendShapePrimaryPrefix, "");
                                    blendShapeGroups[g].blendShapeNames.Add(newName);
                                    blendShapeGroups[g].blendShapeObjectName.Add(renderer.gameObject.name);
                                }
                            }
                        }
                    }
                }
            }
        }

        public int GetGroupIndex(string name)
        {
            for (int g = 0; g < prefabChildManager.prefabGroups.Count; g++)
            {
                if (prefabChildManager.prefabGroups[g].name == name)
                {
                    return g;
                }
            }

            return 999999;
        }

        public void SetBlendShapes()
        {
            blendShapeObjects.Clear();
            
            Transform[] gameObjects = gameObject.GetComponentsInChildren<Transform>(true);
            foreach (Transform transform in gameObjects)
            {
                GameObject gameObject = transform.gameObject;
                if (!gameObject.GetComponent<SkinnedMeshRenderer>())
                    continue;

                SkinnedMeshRenderer smr = gameObject.GetComponent<SkinnedMeshRenderer>();

                if (!smr)
                    continue;
                if (!smr.sharedMesh)
                    continue;
                if (smr.sharedMesh.blendShapeCount == 0)
                    continue;
                if (!blendShapesManager)
                {
                    //Debug.Log("No blendShapesManager");
                    continue;
                }

                blendShapeObjects.Add(new BlendShapeObject());
                BlendShapeObject newObject = blendShapeObjects[blendShapeObjects.Count - 1];

                newObject.gameObjectWithShapes = gameObject;
                for (int i = 0; i < smr.sharedMesh.blendShapeCount; i++)
                {
                    newObject.blendShapeEntries.Add(new BlendShapeEntry());
                    BlendShapeEntry newEntry = newObject.blendShapeEntries[newObject.blendShapeEntries.Count - 1];
                    newEntry.index = i;

                    string triggerName = blendShapesManager.GetHumanName(smr.sharedMesh.GetBlendShapeName(i));

                    if (triggerName.Contains(blendShapesManager.plusMinus.Item2))
                        newEntry.isMinus = true;

                    newEntry.triggerName = triggerName;
                }
            }
        }

        public void TriggerBlendShape(string triggerName, float shapeValue)
        {
            blendShapesManager.TriggerAutoMatches(triggerName, shapeValue);
        }
    }

    [System.Serializable]
    public class BlendShapeGroup
    {
        public string name;
        public List<BlendShapeItem> onActivate = new List<BlendShapeItem>();
        public List<BlendShapeItem> onDeactivate = new List<BlendShapeItem>();
        [HideInInspector] public bool showList = false;
        [HideInInspector] public List<string> blendShapeNames = new List<string>();
        [HideInInspector] public List<string> actualBlendShapeNames = new List<string>();
        [HideInInspector] public List<string> blendShapeObjectName = new List<string>();
        [HideInInspector] public int shapeChoiceIndex = 0;
        //[HideInInspector] public int deactivateChoiceIndex = 0;
    }

    [System.Serializable]
    public class BlendShapeItem
    {
        public string triggerName;
        public string objectName;
        public float value;
        public float min = 0f;
        public float max = 100f;
        public bool revertBack = false;
        public string actionType = "Explicit";
        public int actionTypeIndex = 0;
    }

    [System.Serializable]
    public class BlendShapeObject
    {
        public GameObject gameObjectWithShapes;
        public List<BlendShapeEntry> blendShapeEntries = new List<BlendShapeEntry>();
    }

    [System.Serializable]
    public class BlendShapeEntry
    {
        public int index;
        public string triggerName;
        public bool isMinus = false;
    }
}
