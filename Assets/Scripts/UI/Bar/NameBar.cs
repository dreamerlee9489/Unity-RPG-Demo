using UnityEngine;
using UnityEngine.UI;
using App.Manager;

namespace App.Control
{
    public class NameBar : MonoBehaviour
    {
		public Text chName { get; set; }
		public Text damage { get; set; }

		void Awake()
		{
			chName = transform.GetChild(0).GetComponent<Text>();
			damage = transform.GetChild(1).GetComponent<Text>();
		}

		void Start()
		{
			if(transform.parent != null)
			{
				if(transform.parent.CompareTag("Player"))
				{
					chName.color = Color.green;
					chName.text = InventoryManager.Instance.playerData.nickName;
				}
				else
				{
					chName.color = transform.parent.CompareTag("Enemy") ? Color.red : Color.yellow;
					chName.text = transform.parent.GetComponent<CombatEntity>().entityConfig.nickName;
				}
				chName.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2.4f, 0);
			}	
		}

        void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}
