using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Manager
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
				GameManager.Instance.player.transform.forward = point.forward;
				GameManager.Instance.player.gameObject.SetActive(true);
			}
		}

		void OnTriggerEnter(Collider other)
		{
			if(other.CompareTag("Player"))
			{
				if(MapManager.Instance.entities.Count > 0)
				{
					foreach (var entity in MapManager.Instance.entities)
						entity.Value.SaveEntityData();
					MapManager.Instance.entities.Clear();
				}
				InventoryManager.Instance.SaveData();
				GameManager.Instance.targetPortal = targetPortal;
				GameManager.Instance.player.gameObject.SetActive(false);
				SceneManager.LoadSceneAsync(targetScene);
			}
		}
	}
}
