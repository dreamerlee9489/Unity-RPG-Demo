using UnityEngine;

public class SFB_DemoControlKeys : MonoBehaviour
{

    public GameObject objectC;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            objectC.SetActive(!objectC.activeSelf);
        }
    }
}
