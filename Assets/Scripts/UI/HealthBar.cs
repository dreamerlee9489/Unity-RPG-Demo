using UnityEngine;

namespace App
{
	public class HealthBar : MonoBehaviour
	{
		void LateUpdate()
		{
			transform.forward = Camera.main.transform.forward;
		}
	}
}
