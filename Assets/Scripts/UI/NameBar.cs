using App.Items;
using UnityEngine;
using UnityEngine.UI;

namespace App.Control
{
    public class NameBar : MonoBehaviour
    {
		public Text text = null;

		void Awake()
		{
			if(transform.parent != null)
			{
				text.text = transform.parent.GetComponent<CombatEntity>().entityConfig.nickName;
				text.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2.4f, 0);
			}
		}

        void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}
