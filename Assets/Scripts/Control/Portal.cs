using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Control
{
	public class Portal : MonoBehaviour
	{
		public string portalName = "";
		public string targetScene = "";
		public string targetPortal = "";
		public Transform point { get; set; }

		void Awake()
		{
			point = transform.GetChild(0);
		}

		void OnTriggerEnter(Collider other)
		{
			if(other.CompareTag("Player"))
			{
				SceneManager.LoadSceneAsync(targetScene);
			}
		}
	}
}
