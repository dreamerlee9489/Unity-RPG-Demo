using UnityEditor;
using UnityEngine;

public class IPBR_EnvironmentWarningLP : MonoBehaviour
{
    private string folderPath = "assets/PolygonDungeon";
    public GameObject panel;
    #if UNITY_EDITOR

    // Start is called before the first frame update
    void Awake()
    {
        if (AssetDatabase.IsValidFolder(folderPath))
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
    }

    public void GoToAssetStore()
    {
        Application.OpenURL("https://assetstore.unity.com/packages/3d/environments/dungeons/polygon-dungeons-pack-102677?aid=1100lxWw&pubref=environmentcheck");
    }
    
    #endif
}
