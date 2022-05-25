using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using App.Enviorment;
using App.AI;

namespace App.UI
{
	public class StartPanel : MonoBehaviour
	{
		public string targetPortal = "Entrance";
		Button btnStart { get; set; }

		void Start()
		{
			btnStart = transform.GetChild(0).GetComponent<Button>();
			btnStart.onClick.AddListener(() => {
				UIManager.Instance.hudPanel.gameObject.SetActive(true);
				UIManager.Instance.actionPanel.gameObject.SetActive(true);
				UIManager.Instance.goldPanel.gameObject.SetActive(true);
				GameManager.Instance.targetPortal = targetPortal;
				GameManager.Instance.player = Instantiate(Resources.Load<CombatEntity>("Entity/Player"));
				GameManager.Instance.player.gameObject.SetActive(false);
				SceneManager.LoadScene(1);
				gameObject.SetActive(false);
			});
		}
	}
}
