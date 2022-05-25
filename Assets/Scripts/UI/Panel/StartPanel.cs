using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using App.Manager;
using App.Control;

namespace App.UI
{
	public class StartPanel : MonoBehaviour
	{
		public Button btnStart = null;

		void Start()
		{
			GameManager.Instance.player.GetComponent<PlayerController>().enabled = false;
			UIManager.Instance.hudPanel.gameObject.SetActive(false);
            UIManager.Instance.actionPanel.gameObject.SetActive(false);
			UIManager.Instance.goldPanel.gameObject.SetActive(false);
			btnStart.onClick.AddListener(() => {
				SceneManager.LoadSceneAsync(1);
				GameManager.Instance.player.GetComponent<PlayerController>().enabled = true;
				UIManager.Instance.hudPanel.gameObject.SetActive(true);
				UIManager.Instance.actionPanel.gameObject.SetActive(true);
				UIManager.Instance.goldPanel.gameObject.SetActive(true);
				gameObject.SetActive(false);
			});
		}
	}
}
