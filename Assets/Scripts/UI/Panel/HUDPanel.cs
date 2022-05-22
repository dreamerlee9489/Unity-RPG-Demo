using UnityEngine;

namespace App.UI
{
	public class HUDPanel : MonoBehaviour
	{
		public HUDBar hpBar { get; set; }
		public HUDBar mpBar { get; set; }
		public HUDBar expBar { get; set; }

		void Awake()
		{
			hpBar = transform.GetChild(0).GetComponent<HUDBar>();
			mpBar = transform.GetChild(1).GetComponent<HUDBar>();
			expBar = transform.GetChild(2).GetComponent<HUDBar>();
		}
	}
}
