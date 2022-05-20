using UnityEngine;
using UnityEngine.UI;

namespace App.UI
{
	public class HUDBar : MonoBehaviour
	{
		float timer = 6;
		public Image foreground = null;

		void Awake()
		{
			if(CompareTag("Enemy"))
				gameObject.SetActive(false);
		}

		void LateUpdate()
		{
			if(gameObject.activeSelf)
			{
				timer -= Time.deltaTime;
				if(timer <= 0)
					gameObject.SetActive(false);
			}
			transform.forward = Camera.main.transform.forward;
		}

		public void UpdateBar(Vector3 vector)
		{
			if(CompareTag("Enemy"))
			{
				timer = 6;
				if(!gameObject.activeSelf)
					gameObject.SetActive(true);
			}
			foreground.transform.localScale = vector;
		}
	}
}
