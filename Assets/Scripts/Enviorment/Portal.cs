using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Enviorment
{
	public class Portal : MonoBehaviour
	{
		public string portalName = "";
		public string targetScene = "";
		public string targetPortal = "";
		public Transform point = null;

		void Awake()
		{
			if(GameManager.Instance.targetPortal == portalName)
			{
				GameManager.Instance.player.transform.position = point.position;
				GameManager.Instance.player.gameObject.SetActive(true);
				GameManager.Instance.player.transform.forward = point.forward;
			}
		}

		void OnTriggerEnter(Collider other)
		{
			if(other.CompareTag("Player"))
			{
				GameManager.Instance.targetPortal = targetPortal;
				GameManager.Instance.player.gameObject.SetActive(false);
				SceneManager.LoadScene(targetScene);
			}
		}
	}
}
