using System.Net;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using App.Manager;
using App.Control;
using App.Data;

namespace App.UI
{
    public class StartPanel : MonoBehaviour
    {
        public Transform optionsPanel = null;
        public Transform createPanel = null;
        public ScrollRect recordPanel = null;
        public Button btnContinue = null;
        public Button btnCreate = null;
        public Button btnLoad = null;
        public Button btnExit = null;
        InputField inputField = null;
        Button btnCreateSure = null;
        Button btnCreateBack = null;
        Button btnRecordBack = null;
        Button btnRecord = null;

        void Start()
        {
            btnRecord = Resources.Load<Button>("UI/Button/BtnOption");
            btnContinue = optionsPanel.GetChild(0).GetComponent<Button>();
            btnCreate = optionsPanel.GetChild(1).GetComponent<Button>();
            btnLoad = optionsPanel.GetChild(2).GetComponent<Button>();
            btnExit = optionsPanel.GetChild(3).GetComponent<Button>();
            inputField = createPanel.transform.GetChild(0).GetComponent<InputField>();
            btnCreateSure = createPanel.transform.GetChild(1).GetComponent<Button>();
            btnCreateBack = createPanel.transform.GetChild(2).GetComponent<Button>();
            btnRecordBack = recordPanel.transform.GetChild(2).GetComponent<Button>();
            btnContinue.onClick.AddListener(() =>
            {
                PlayerData playerData = JsonManager.Instance.LoadData<PlayerData>("CurrentPlayerData");
                if (playerData != null)
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
            btnCreate.onClick.AddListener(() =>
            {
                optionsPanel.gameObject.SetActive(false);
                createPanel.gameObject.SetActive(true);
            });
            btnLoad.onClick.AddListener(() =>
            {
                if(File.Exists(InventoryManager.PLAYER_NAMES_PATH))
                {
                    using(StreamReader reader = File.OpenText(InventoryManager.PLAYER_NAMES_PATH))
                    {
                        string name = "";
                        while((name = reader.ReadLine()) != null)
                        {
                            Button btn = Instantiate(btnRecord, recordPanel.content);
                            btn.transform.GetChild(0).GetComponent<Text>().text = name;
                            btn.onClick.AddListener(() => {
                                PlayerData playerData = JsonManager.Instance.LoadData<PlayerData>(btn.transform.GetChild(0).GetComponent<Text>().text + "_PlayerData");
                                GameManager.Instance.player = Instantiate(Resources.Load<CombatEntity>("Entity/Player/Player"));
                                GameManager.Instance.virtualCamera.Follow = GameManager.Instance.player.transform;
                                GameManager.Instance.EnterScene(playerData.sceneName, playerData.position);
                                InventoryManager.Instance.playerData = playerData;
                                InventoryManager.Instance.bag = GameManager.Instance.player.GetComponent<PlayerController>().bag;
                                InventoryManager.Instance.skills = GameManager.Instance.player.GetComponent<PlayerController>().skills;
                                UIManager.Instance.hudPanel.gameObject.SetActive(true);
                                UIManager.Instance.actionPanel.gameObject.SetActive(true);
                                UIManager.Instance.goldPanel.gameObject.SetActive(true);
                                UIManager.Instance.startPanel.gameObject.SetActive(false);
                            });
                        }
                    }
                }
                optionsPanel.gameObject.SetActive(false);
                recordPanel.gameObject.SetActive(true);
            });
            btnExit.onClick.AddListener(() =>
            {
                Application.Quit();
            });
            btnCreateSure.onClick.AddListener(() =>
            {
                string playerName = inputField.text == "" ? "冒险家" : inputField.text;
                if(File.Exists(InventoryManager.PLAYER_NAMES_PATH))
                {
                    using(StreamReader reader = File.OpenText(InventoryManager.PLAYER_NAMES_PATH))
                    {
                        string name = "";
                        while((name = reader.ReadLine()) == playerName)
                        {
                            Debug.Log("用户名已存在!");
                            return;
                        }
                    }
                } 
                InventoryManager.Instance.playerData = new PlayerData(playerName, 5000);
                GameManager.Instance.player = Instantiate(Resources.Load<CombatEntity>("Entity/Player/Player"));
                GameManager.Instance.virtualCamera.Follow = GameManager.Instance.player.transform;
                GameManager.Instance.EnterScene("Village", "BirthPoint");
                InventoryManager.Instance.bag = GameManager.Instance.player.GetComponent<PlayerController>().bag;
                InventoryManager.Instance.skills = GameManager.Instance.player.GetComponent<PlayerController>().skills;
                UIManager.Instance.hudPanel.gameObject.SetActive(true);
                UIManager.Instance.actionPanel.gameObject.SetActive(true);
                UIManager.Instance.goldPanel.gameObject.SetActive(true);
                gameObject.SetActive(false);
            });
            btnCreateBack.onClick.AddListener(() =>
            {
                createPanel.gameObject.SetActive(false);
                optionsPanel.gameObject.SetActive(true);
            });
            btnRecordBack.onClick.AddListener(() =>
            {
                recordPanel.gameObject.SetActive(false);
                optionsPanel.gameObject.SetActive(true);
            });
            createPanel.gameObject.SetActive(false);
            recordPanel.gameObject.SetActive(false);
        }
    }
}
