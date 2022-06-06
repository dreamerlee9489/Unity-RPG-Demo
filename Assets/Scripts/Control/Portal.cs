using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Manager
{
	public class Portal : MonoBehaviour
	{
		static MapManager mapManager = null;
		public string targetScene = "";
		public string targetPortal = "";
		public Transform point { get; set; }

		void Awake()
		{
			point = transform.GetChild(0);
			if(mapManager == null)
				mapManager = GameObject.FindObjectOfType<MapManager>();
			if(GameManager.Instance.targetPortal == name)
			{
				GameManager.Instance.player.transform.position = point.position;
				GameManager.Instance.player.transform.forward = point.forward;
				GameManager.Instance.player.gameObject.SetActive(true);
			}
		}

		void OnTriggerEnter(Collider other)
		{
			if(other.CompareTag("Player"))
			{
				GameManager.Instance.onSavingData();
				GameManager.Instance.targetPortal = targetPortal;
				GameManager.Instance.player.nameBar.damage.text = "";
				GameManager.Instance.player.gameObject.SetActive(false);
				SceneManager.LoadSceneAsync(targetScene);
			}
		}
	}
}
