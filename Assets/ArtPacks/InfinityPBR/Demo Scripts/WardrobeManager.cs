using System.Collections.Generic;
using UnityEngine;

/*
 * Wardrobe Manager is meant to make it a bit easier to set up and manage large amounts of wardrobe options on a
 * character. Attach this to the character. Use the inspector buttons to easily add new wardrobe groups.
 */

namespace InfinityPBR
{
    [System.Serializable]
    public class WardrobeManager : MonoBehaviour
    {
        public List<GameObject> wardrobeParents = new List<GameObject>();
        public List<GameObject> orphanWardrobe = new List<GameObject>();
        public List<WardrobeGroup> wardrobeGroups = new List<WardrobeGroup>();
        [HideInInspector] public int groupIndex = 0;
        

        public void AddWardrobeGroup()
        {
            WardrobeGroup newGroup = new WardrobeGroup();
            wardrobeGroups.Add(newGroup);
            int w = wardrobeGroups.Count;
            for (int o = 0; o < orphanWardrobe.Count; o++)
            {
                if (orphanWardrobe[o].activeSelf)
                    wardrobeGroups[w].wardrobe.Add(orphanWardrobe[o]);
            }
            for (int p = 0; p < wardrobeParents.Count; p++)
            {
                foreach (Transform child in wardrobeParents[p].transform)
                {
                    if (child.gameObject.activeSelf)
                        wardrobeGroups[w].wardrobe.Add(child.gameObject);
                }
            }
        }

        public void NextWardrobe(int i, bool turnturnOffOtherWardrobeFirst = true)
        {
            groupIndex += i;
            if (groupIndex >= wardrobeGroups.Count)
                groupIndex = 0;
            else if (groupIndex < 0)
                groupIndex = wardrobeGroups.Count - 1;
            ActivateWardrobe(groupIndex, turnturnOffOtherWardrobeFirst);
        }

        public void RandomWardrobe()
        {
            ActivateWardrobe(Random.Range(0,wardrobeGroups.Count));
        }

        public void ActivateWardrobe(string v, bool turnturnOffOtherWardrobeFirst = true)
        {
            for (int i = 0; i < wardrobeGroups.Count; i++)
            {
                if (wardrobeGroups[i].name == v)
                {
                    ActivateWardrobe(i, turnturnOffOtherWardrobeFirst);
                    return;
                }
            }

            Debug.LogWarning("Warning: No wardrobe group found with the name " + v);
        }

        public void ActivateWardrobe(int i, bool turnOffOtherWardrobeFirst = true)
        {
            if (wardrobeGroups.Count > 0)
            {
                if (turnOffOtherWardrobeFirst)
                {
                    DeactivateBlendShapes(groupIndex);
                    TurnOffAllWardrobe();
                }
                
                groupIndex = i;

                for (int w = 0; w < wardrobeGroups[i].wardrobe.Count; w++)
                {
                    wardrobeGroups[i].wardrobe[w].SetActive(true);
                    if (wardrobeGroups[i].skinnedMeshRenderers[w] != null)
                    {
                        wardrobeGroups[i].skinnedMeshRenderers[w].sharedMaterial = wardrobeGroups[i].materials[w];
                    }
                    else if (wardrobeGroups[i].meshRenderers[w] != null)
                    {
                        wardrobeGroups[i].meshRenderers[w].sharedMaterial = wardrobeGroups[i].materials[w];
                    }
                }
                ActivateBlendShapes(groupIndex);
            }
            else
                Debug.LogWarning("Warning: There are no wardrobe groups ready");
        }

        public void DeactivateBlendShapes(int i)
        {
            for (int b = 0; b < wardrobeGroups[i].blendShapes.Count; b++)
            {
                GetComponent<SFB_BlendShapesManager>().SetValue(wardrobeGroups[i].blendShapes[b].blendShapeObjectName,
                    wardrobeGroups[i].blendShapes[b].blendShapeName, wardrobeGroups[i].blendShapes[b].deactivateValue);
            }
        }

        public void ActivateBlendShapes(int i)
        {
            for (int b = 0; b < wardrobeGroups[i].blendShapes.Count; b++)
            {
                GetComponent<SFB_BlendShapesManager>().SetValue(wardrobeGroups[i].blendShapes[b].blendShapeObjectName,
                    wardrobeGroups[i].blendShapes[b].blendShapeName, wardrobeGroups[i].blendShapes[b].activateValue);
            }
        }

        public void TurnOffAllWardrobe()
        {
            ToggleAllOrphanWardrobe(false);

            for (int p = 0; p < wardrobeParents.Count; p++)
            {
                ToggleAllWardrobeInParent(wardrobeParents[p], false);
            }
        }

        public void ToggleAllOrphanWardrobe(bool v)
        {
            for (int o = 0; o < orphanWardrobe.Count; o++)
            {
                orphanWardrobe[o].SetActive(v);
            }
        }

        public void ToggleAllWardrobeInParent(GameObject p, bool v = true)
        {
            foreach (Transform child in p.transform)
            {
                child.gameObject.SetActive(v);
            }
        }

        public void AddWardrobeParent(GameObject o)
        {
            if (!wardrobeParents.Contains(o))
                wardrobeParents.Add(o);
        }

        public void AddOrphanWardrobe(GameObject o)
        {
            if (!orphanWardrobe.Contains(o))
                orphanWardrobe.Add(o);
        }

        public void CreateNewWardrobeGroup()
        {
            wardrobeGroups.Add(new WardrobeGroup());
            int g = wardrobeGroups.Count - 1;
            groupIndex = g;
            wardrobeGroups[g].name = "Wardrobe " + g;
            PopulateWardrobeGroup(g);
        }

        public void UpdateWardrobeGroup(int g)
        {
            wardrobeGroups[g].wardrobe.Clear();
            PopulateWardrobeGroup(g);
        }

        public SkinnedMeshRenderer GetSkinnedMeshRenderer(GameObject go)
        {
            if (go.GetComponent<SkinnedMeshRenderer>())
            {
                return go.GetComponent<SkinnedMeshRenderer>();
            }
            foreach (Transform child in go.transform)
            {
                if (child.gameObject.GetComponent<SkinnedMeshRenderer>())
                {
                    return child.gameObject.GetComponent<SkinnedMeshRenderer>();
                }
            }

            return null;
        }

        public Renderer GetMeshRenderer(GameObject go)
        {
            if (go.GetComponent<Renderer>())
            {
                return go.GetComponent<Renderer>();
            }
            foreach (Transform child in go.transform)
            {
                if (child.gameObject.GetComponent<Renderer>())
                {
                    return child.gameObject.GetComponent<Renderer>();
                }
            }

            return null;
        }

        public void PopulateRenderersAndMaterials(int g, GameObject go)
        {
            wardrobeGroups[g].wardrobe.Add(go);
            SkinnedMeshRenderer newSkinnedMeshRenderer = GetSkinnedMeshRenderer(go);
            Renderer newMeshRenderer = GetMeshRenderer(go);
            if (newSkinnedMeshRenderer)
            {
                wardrobeGroups[g].skinnedMeshRenderers.Add(newSkinnedMeshRenderer);
                wardrobeGroups[g].materials.Add(newSkinnedMeshRenderer.sharedMaterial);
            } else if (newMeshRenderer)
            {
                wardrobeGroups[g].meshRenderers.Add(newMeshRenderer);
                wardrobeGroups[g].materials.Add(newMeshRenderer.sharedMaterial);
            }
        }
        
        public void PopulateWardrobeGroup(int g)
        {
            for (int i = 0; i < orphanWardrobe.Count; i++)
            {
                if (orphanWardrobe[i].activeSelf)
                    PopulateRenderersAndMaterials(g, orphanWardrobe[i]);
            }

            for (int p = 0; p < wardrobeParents.Count; p++)
            {
                foreach (Transform child in wardrobeParents[p].transform)
                {
                    if (child.gameObject.activeSelf)
                        PopulateRenderersAndMaterials(g, child.gameObject);
                }
            }
        }

        public void AddGameObjectToAll(GameObject o, bool add = true)
        {
            for (int i = 0; i < wardrobeGroups.Count; i++)
            {
                if (!wardrobeGroups[i].wardrobe.Contains(o) && add)
                {
                    wardrobeGroups[i].wardrobe.Add(o);
                }
                else if (wardrobeGroups[i].wardrobe.Contains(o) && !add)
                {
                    for (int v = 0; v < wardrobeGroups[i].wardrobe.Count; v++)
                    {
                        if (wardrobeGroups[i].wardrobe[v] == o)
                        {
                            wardrobeGroups[i].wardrobe.RemoveAt(v);
                        }
                    }
                }
            }
        }

        public void RemoveOrphanObject(GameObject obj)
        {
            // Remove from Groups
            for (int g = 0; g < wardrobeGroups.Count; g++)
            {
                for (int gi = 0; gi < wardrobeGroups[g].wardrobe.Count; gi++)
                {
                    if (wardrobeGroups[g].wardrobe[gi] == obj)
                    {
                        wardrobeGroups[g].wardrobe.RemoveAt(gi);
                    }
                }
            }
            
            // Remove from Orphans
            for (int o = 0; o < orphanWardrobe.Count; o++)
            {
                if (orphanWardrobe[o] == obj)
                {
                    orphanWardrobe.RemoveAt(o);
                }
            }
        }

        public int CountThisObjectInGroup(GameObject go, WardrobeGroup g)
        {
            int count = 0;
            for (int i = 0; i < g.wardrobe.Count; i++)
            {
                if (g.wardrobe[i].gameObject == go)
                {
                    count++;
                }
            }

            return count;
        }
    }

    [System.Serializable]
    public class WardrobeGroup
    {
        [HideInInspector] public bool showWardrobeObjects = false;
        [HideInInspector] public bool showBlendShapes = false;
        public string name;
        public List<GameObject> wardrobe = new List<GameObject>();    // All wardrobe here will be "on"
        public List<SkinnedMeshRenderer> skinnedMeshRenderers = new List<SkinnedMeshRenderer>();
        public List<Renderer> meshRenderers = new List<Renderer>();
        public List<Material> materials = new List<Material>();
        public List<WardrobeBlendShapes> blendShapes = new List<WardrobeBlendShapes>();
    }

    [System.Serializable]
    public class WardrobeBlendShapes
    {
        public int blendShapeObjectIndex;
        public string blendShapeObjectName;
        public int blendShapeIndex;
        public string blendShapeName;
        public float activateValue;
        public float deactivateValue;
    }
    
}

