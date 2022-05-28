using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using App.Manager;
using App.Control;
using App.Data;

namespace App.UI
{
    public class StartPanel : MonoBehaviour
    {
        Transform optionsPanel = null;
        Transform newPanel = null;
        Button btnContinue = null;
        Button btnNew = null;
        Button btnLoad = null;
        Button btnExit = null;
        Button btnCreate = null;
        Button btnBack = null;
        InputField inputField = null;

        void Start()
        {
            optionsPanel = transform.GetChild(1);
            newPanel = transform.GetChild(2);
            btnContinue = optionsPanel.GetChild(0).GetComponent<Button>();
            btnNew = optionsPanel.GetChild(1).GetComponent<Button>();
            btnLoad = optionsPanel.GetChild(2).GetComponent<Button>();
            btnExit = optionsPanel.GetChild(3).GetComponent<Button>();
            inputField = newPanel.GetChild(0).GetComponent<InputField>();
            btnCreate = newPanel.GetChild(1).GetComponent<Button>();
            btnBack = newPanel.GetChild(2).GetComponent<Button>();
            newPanel.gameObject.SetActive(false);
            btnContinue.onClick.AddListener(() => {
                UIManager.Instance.hudPanel.gameObject.SetActive(true);
                UIManager.Instance.actionPanel.gameObject.SetActive(true);
                UIManager.Instance.goldPanel.gameObject.SetActive(true);
                GameManager.Instance.player = Instantiate(Resources.Load<CombatEntity>("Entity/Player/Player"));
                GameManager.Instance.transform.GetChild(0).GetChild(0).GetComponent<CinemachineVirtualCamera>().Follow = GameManager.Instance.player.transform;
                GameManager.Instance.player.gameObject.SetActive(false);
                InventoryManager.Instance.Load(JsonManager.Instance.LoadData<PlayerData>("CurrentPlayerData"));
                gameObject.SetActive(false);
            });
            btnNew.onClick.AddListener(() => {
                optionsPanel.gameObject.SetActive(false);
                newPanel.gameObject.SetActive(true);
            });
            btnCreate.onClick.AddListener(() => {
                UIManager.Instance.hudPanel.gameObject.SetActive(true);
                UIManager.Instance.actionPanel.gameObject.SetActive(true);
                UIManager.Instance.goldPanel.gameObject.SetActive(true);
                GameManager.Instance.player = Instantiate(Resources.Load<CombatEntity>("Entity/Player/Player"));
                GameManager.Instance.player.GetComponent<PlayerController>().playerData.nickName = inputField.text == "" ? "冒险家" : inputField.text;
                GameManager.Instance.transform.GetChild(0).GetChild(0).GetComponent<CinemachineVirtualCamera>().Follow = GameManager.Instance.player.transform;
                GameManager.Instance.player.gameObject.SetActive(false);
                GameManager.Instance.targetPortal = "BirthPoint";
                SceneManager.LoadSceneAsync("Village");
                gameObject.SetActive(false);
            });
            btnBack.onClick.AddListener(() => {
                newPanel.gameObject.SetActive(false);
                optionsPanel.gameObject.SetActive(true);
            });
        }
    }
}
