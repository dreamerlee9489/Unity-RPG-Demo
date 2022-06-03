using UnityEngine.UI;
using UnityEngine;

public class TextSameAsParentName : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Text>().text = transform.parent.gameObject.name;
    }
}
