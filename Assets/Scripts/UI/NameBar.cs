using UnityEngine;
using UnityEngine.UI;

namespace App.Control
{
    public class NameBar : MonoBehaviour
    {
		public Text chName { get; set; }

		void Awake()
		{
			chName = transform.GetChild(0).GetComponent<Text>();
			if(transform.parent != null)
			{
				chName.text = transform.parent.GetComponent<CombatEntity>().entityConfig.nickName;
				chName.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2.4f, 0);
			}
		}

        void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}
