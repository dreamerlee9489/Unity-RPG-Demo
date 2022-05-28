using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Manager
{
	public class Portal : MonoBehaviour
	{
		Transform point = null;
		static MapManager mapManager = null;
		public string portalName = "";
		public string targetScene = "";
		public string targetPortal = "";

		void Awake()
		{
			point = transform.GetChild(0);
			if(mapManager == null)
				mapManager = GameObject.FindObjectOfType<MapManager>();
			if(GameManager.Instance.targetPortal == portalName)
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
				foreach (var entity in mapManager.entities)
					entity.Value.SaveEntityData();
				InventoryManager.Instance.Save();
				GameManager.Instance.targetPortal = targetPortal;
				GameManager.Instance.player.gameObject.SetActive(false);
				SceneManager.LoadSceneAsync(targetScene);
			}
		}
	}
}
