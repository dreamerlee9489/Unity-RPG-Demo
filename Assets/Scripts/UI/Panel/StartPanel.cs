using UnityEngine;
using UnityEngine.UI;
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
                PlayerData playerData = JsonManager.Instance.LoadData<PlayerData>("CurrentPlayerData");
                if(playerData != null)
                {
                    GameManager.Instance.player = Instantiate(Resources.Load<CombatEntity>("Entity/Player/Player"));
                    GameManager.Instance.virtualCamera.Follow = GameManager.Instance.player.transform;
                    GameManager.Instance.EnterScene(playerData.sceneName, playerData.position);
                    InventoryManager.Instance.playerData = playerData;
                    InventoryManager.Instance.bag = GameManager.Instance.player.GetComponent<PlayerController>().bag;
                    InventoryManager.Instance.skills = GameManager.Instance.player.GetComponent<PlayerController>().skills;
                    UIManager.Instance.hudPanel.gameObject.SetActive(true);
                    UIManager.Instance.actionPanel.gameObject.SetActive(true);
                    UIManager.Instance.goldPanel.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                }
            });
            btnNew.onClick.AddListener(() => {
                optionsPanel.gameObject.SetActive(false);
                newPanel.gameObject.SetActive(true);
            });
            btnCreate.onClick.AddListener(() => {
                GameManager.Instance.player = Instantiate(Resources.Load<CombatEntity>("Entity/Player/Player"));
                GameManager.Instance.virtualCamera.Follow = GameManager.Instance.player.transform;
                GameManager.Instance.EnterScene("Village", "BirthPoint");
                InventoryManager.Instance.playerData = new PlayerData(inputField.text == "" ? "冒险家" : inputField.text, 5000);
                InventoryManager.Instance.bag = GameManager.Instance.player.GetComponent<PlayerController>().bag;
                InventoryManager.Instance.skills = GameManager.Instance.player.GetComponent<PlayerController>().skills;
                UIManager.Instance.hudPanel.gameObject.SetActive(true);
                UIManager.Instance.actionPanel.gameObject.SetActive(true);
                UIManager.Instance.goldPanel.gameObject.SetActive(true);
                gameObject.SetActive(false);
            });
            btnBack.onClick.AddListener(() => {
                newPanel.gameObject.SetActive(false);
                optionsPanel.gameObject.SetActive(true);
            });
        }
    }
}
