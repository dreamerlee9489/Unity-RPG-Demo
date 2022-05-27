using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using App.Manager;
using App.Control;

namespace App.UI
{
    public class StartPanel : MonoBehaviour
    {
        public string targetScene = "Village";
        public string targetPortal = "Entrance";
        Button btnStart = null;

        void Start()
        {
            btnStart = transform.GetChild(0).GetComponent<Button>();
            btnStart.onClick.AddListener(() =>
            {
                UIManager.Instance.hudPanel.gameObject.SetActive(true);
                UIManager.Instance.actionPanel.gameObject.SetActive(true);
                UIManager.Instance.goldPanel.gameObject.SetActive(true);
                GameManager.Instance.targetPortal = targetPortal;
                GameManager.Instance.player = Instantiate(Resources.Load<CombatEntity>("Entity/Player/Player"));
                GameManager.Instance.transform.GetChild(0).GetChild(0).GetComponent<CinemachineVirtualCamera>().Follow = GameManager.Instance.player.transform;
                GameManager.Instance.player.gameObject.SetActive(false);
                SceneManager.LoadSceneAsync(targetScene);
                gameObject.SetActive(false);
            });
        }
    }
}
