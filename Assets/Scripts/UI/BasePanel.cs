using UnityEngine;

namespace Game.UI
{
	public class BasePanel : MonoBehaviour
	{
		public bool isOpened = false;

		public void Open()
		{
			isOpened = true;
			gameObject.SetActive(true);
		}

		public void Close()
		{
			isOpened = false;
			gameObject.SetActive(false);
		}
	}
}
