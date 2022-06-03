using System.Collections.Generic;
using UnityEngine;
using InfinityPBR;
using UnityEditor;
using UnityEngine.Events;

/*
 * Wardrobe Manager is meant to make it a bit easier to set up and manage large amounts of wardrobe options on a
 * character. Attach this to the character. Use the inspector buttons to easily add new wardrobe groups.
 */

namespace InfinityPBR
{
    [System.Serializable]
    public class PrefabChildManager : MonoBehaviour
    {
        public PrefabChildEvent _event;
        
        public List<PrefabGroup> prefabGroups = new List<PrefabGroup>();
        public bool onlyOneGroupActivePerType = true;
        public bool unpackPrefabs = true;
        public bool revertToDefaultGroupByType = true;

        private WardrobePrefabManager wardrobePrefabManager;
        private BlendShapesManager blendShapesManager;
        [HideInInspector] public Transform thisTransform => transform;

        public void Awake()
        {
            wardrobePrefabManager = GetComponent<WardrobePrefabManager>();
            blendShapesManager = GetComponent<BlendShapesManager>();
        }

        public void Start()
        {
            if (_event == null)
                _event = new PrefabChildEvent();
        }
        
        public void CreateNewPrefabGroup()
        {
            prefabGroups.Add(new PrefabGroup());
            int g = prefabGroups.Count - 1;
            prefabGroups[g].name = "Prefab Group " + g;
        }
        

        public void ActivateGroup(string v)
        {
            for (int i = 0; i < prefabGroups.Count; i++)
            {
                if (prefabGroups[i].name == v)
                {
                    ActivateGroup(i);
                    return;
                }
            }

            Debug.LogWarning("Warning: No prefab group found with the name " + v);
        }

        public void ActivateGroup(int g)
        {
            if (prefabGroups.Count < g || prefabGroups.Count == 0)
            {
                Debug.LogWarning("Warning: Group index (" + g + ") out of range.");
                return;
            }

            if (onlyOneGroupActivePerType && prefabGroups[g].groupType != "")
            {
                DeactivateGroupsOfType(prefabGroups[g].groupType, false);
            }

            prefabGroups[g].isActive = true;

            for (int i = 0; i < prefabGroups[g].prefabObjects.Count; i++)
            {
                
                PrefabObject prefabObject = prefabGroups[g].prefabObjects[i];
                
                if (prefabObject.prefab.transform.IsChildOf(transform))
                {
                    prefabObject.isPrefab = false;
                }
                else
                {
                    prefabObject.isPrefab = true;
                }

                if (prefabObject.isPrefab && prefabObject.inGameObject == prefabObject.prefab)
                {
                    prefabObject.inGameObject = null;
                }
                
                if (prefabObject.isPrefab && prefabObject.prefab && !prefabObject.inGameObject)
                {
                    prefabObject.inGameObject = Instantiate(prefabObject.prefab, prefabObject.parentTransform.position, prefabObject.parentTransform.rotation, prefabObject.parentTransform);
                    prefabObject.inGameObject.SetActive(true);
                    if (unpackPrefabs && PrefabUtility.IsAnyPrefabInstanceRoot(prefabObject.inGameObject))
                        PrefabUtility.UnpackPrefabInstance(prefabObject.inGameObject, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
                    
                    #if UNITY_EDITOR
                    wardrobePrefabManager = GetComponent<WardrobePrefabManager>();
                    blendShapesManager = GetComponent<BlendShapesManager>();
                    #endif
                    
                    if (wardrobePrefabManager)
                    {
                        if (wardrobePrefabManager.autoRigWhenActivated)
                        {
                            IPBR_CharacterEquip.EquipCharacter(gameObject); 
                        }
                    }
                }
                else if (!prefabObject.isPrefab)
                {
                    if (prefabObject.prefab)
                    {
                        prefabObject.prefab.SetActive(true);
                        prefabObject.inGameObject = prefabObject.prefab;
                    }
                }
            }

            if (wardrobePrefabManager)
            {
                wardrobePrefabManager.OnActivate(prefabGroups[g].name);
            }

            if (blendShapesManager)
            {
                blendShapesManager.LoadBlendShapeData();
            }
        }

        public void DeactivateGroup(string v, bool checkForDefault = true)
        {
            for (int i = 0; i < prefabGroups.Count; i++)
            {
                if (prefabGroups[i].name == v)
                {
                    DeactivateGroup(i, checkForDefault);
                    return;
                }
            }

            Debug.LogWarning("Warning: No prefab group found with the name " + v);
        }

        public void DeactivateGroup(int g, bool checkForDefault = true)
        {
            prefabGroups[g].isActive = false;
            
            if (wardrobePrefabManager)
            {
                wardrobePrefabManager.OnDeactivate(prefabGroups[g].name);
            }
            
            if (prefabGroups.Count > g)
            {

                // ITEMS
                for (int i = 0; i < prefabGroups[g].prefabObjects.Count; i++)
                {
                    if (prefabGroups[g].prefabObjects[i].isPrefab)
                        DestroyObject(prefabGroups[g].prefabObjects[i].inGameObject);
                    else if (prefabGroups[g].prefabObjects[i].prefab)
                    {
                        //Debug.Log("Setting Not Active");
                        prefabGroups[g].prefabObjects[i].prefab.SetActive(false);
                    }
                        
                }
            }
            

            if (checkForDefault && prefabGroups[g].groupType != "")
                CheckForDefaultGroup(prefabGroups[g].groupType);
        }

        public void DeactivateGroupsOfType(string type, bool checkForDefault = true)
        {
            for (int g = 0; g < prefabGroups.Count; g++)
            {
                if (prefabGroups[g].groupType != type)
                    continue;
                //if (GroupIsActive(g) == 0)
                //    continue;
                DeactivateGroup(g, checkForDefault);
            }
        }

        public void CheckForDefaultGroup(string type)
        {
            int defaultGroupIndex = 999999;
            for (int g = 0; g < prefabGroups.Count; g++)
            {
                if (prefabGroups[g].groupType != type)
                    continue;
                if (prefabGroups[g].isDefault)
                    defaultGroupIndex = g;
                if (GroupIsActive(g) > 0)
                    return;
            }
            if (defaultGroupIndex != 999999)
                ActivateGroup(defaultGroupIndex);
        }

        public void DestroyObject(GameObject inGameObject)
        {
            if (inGameObject)
            {
#if UNITY_EDITOR
                DestroyImmediate(inGameObject);
#else
                Destroy(inGameObject);
#endif
            }
        }

        public int GroupIsActive(int g)
        {
            int prefabs = 0;
            int inGameObjects = 0;
            
            for (int i = 0; i < prefabGroups[g].prefabObjects.Count; i++)
            {
                if (!prefabGroups[g].prefabObjects[i].prefab)
                    continue;
                
                prefabs++;
                if (prefabGroups[g].prefabObjects[i].isPrefab && prefabGroups[g].prefabObjects[i].inGameObject)
                    inGameObjects++;
                else if (!prefabGroups[g].prefabObjects[i].isPrefab && prefabGroups[g].prefabObjects[i].prefab.activeSelf)
                    inGameObjects++;
            }

            if (prefabs == inGameObjects && (prefabs > 0 || prefabGroups[g].isActive))
                return 2;
            if (prefabs > inGameObjects && inGameObjects > 0)
                return 1;

            return 0;
        }

        public void AddPrefabToGroup(GameObject prefab, int g)
        {
            prefabGroups[g].prefabObjects.Add(new PrefabObject());
            prefabGroups[g].prefabObjects[prefabGroups[g].prefabObjects.Count - 1].prefab = prefabGroups[g].newPrefab;
            prefabGroups[g].prefabObjects[prefabGroups[g].prefabObjects.Count - 1].parentTransform = thisTransform;
            prefabGroups[g].prefabObjects[prefabGroups[g].prefabObjects.Count - 1].isPrefab = true;

        }
        
        public void AddGameObjectToGroup(GameObject newObject, int g)
        {
            prefabGroups[g].prefabObjects.Add(new PrefabObject());
            prefabGroups[g].prefabObjects[prefabGroups[g].prefabObjects.Count - 1].prefab = prefabGroups[g].newPrefab;
            prefabGroups[g].prefabObjects[prefabGroups[g].prefabObjects.Count - 1].isPrefab = false;
        }

        public List<string> GetGroupsOfType(string type)
        {
            List<string> newList = new List<string>();
            for (int i = 0; i < prefabGroups.Count; i++)
            {
                if (prefabGroups[i].groupType == "Hair")
                {
                    newList.Add(prefabGroups[i].name);
                }
            }

            return newList;
        }
        
        public void MarkPrefabs (PrefabChildManager prefabManager)
        {
            foreach(PrefabGroup prefabGroup in prefabManager.prefabGroups)
            {
                foreach (PrefabObject prefabObject in prefabGroup.prefabObjects)
                {
                    if (!prefabManager)
                        continue;
                    if (!prefabObject.prefab)
                        continue;
                    if (prefabObject.isPrefab && prefabObject.inGameObject == prefabObject.prefab)
                    {
                        prefabObject.inGameObject = null;
                    }
                    if (prefabObject.prefab.transform.IsChildOf(prefabManager.transform))
                    {
                        prefabObject.isPrefab = false;
                    }
                    else
                    {
                        prefabObject.isPrefab = true;
                    }
                    if (prefabObject.isPrefab && prefabObject.inGameObject == prefabObject.prefab)
                    {
                        prefabObject.inGameObject = null;
                    }
                }
            }
        }
    }

    [System.Serializable]
    public class PrefabGroup
    {
        [HideInInspector] public bool showPrefabs = false;
        [HideInInspector] public bool showObjectsOnActivate = false;
        [HideInInspector] public bool showObjectsOnDeactivate = false;
        public bool isDefault = false;
        public string name;
        public string groupType;
       public bool isActive;

        public List<PrefabObject> prefabObjects = new List<PrefabObject>();
        //public List<PrefabObject> objectsOnAdd = new List<PrefabObject>();
        //public List<PrefabObject> objectsOnRemove = new List<PrefabObject>();

        [HideInInspector] public GameObject newPrefab;
        [HideInInspector] public GameObject newGameObject;
    }

    [System.Serializable]
    public class PrefabObject
    {
        public GameObject prefab;
        public Transform parentTransform;
        public GameObject inGameObject;
        public MeshRenderer meshRenderer;
        public SkinnedMeshRenderer skinnedMeshRenderer;

        public bool isPrefab = false;
    }
    
    [System.Serializable]
    public class PrefabChildEvent : UnityEvent<string>
    {
        
    }
}

