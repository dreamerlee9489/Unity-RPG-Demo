using UnityEngine;
using UnityEngine.UI;

namespace App.UI
{
	public class HUDBar : MonoBehaviour
	{
		bool isShow = false;
		float timer = 6;
		public Image foreground { get; set; }

		void Awake()
		{
			foreground = transform.GetChild(0).GetChild(0).GetComponent<Image>();
			if(transform.parent.CompareTag("Enemy") || transform.parent.CompareTag("NPC"))
				gameObject.SetActive(false);
		}

		void LateUpdate()
		{
			if(isShow)
			{
				timer -= Time.deltaTime;
				if(timer <= 0)
					gameObject.SetActive(isShow = false);
			}
			transform.forward = Camera.main.transform.forward;
		}

		public void UpdateBar(Vector3 scale)
		{
			if(transform.parent.CompareTag("Enemy"))
			{
				timer = 6;
				if(!isShow)
					gameObject.SetActive(isShow = true);
			}
			foreground.transform.localScale = scale;
		}
	}
}
