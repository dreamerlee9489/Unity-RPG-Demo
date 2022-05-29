using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using App.Control;
using App.Data;

namespace App.Manager
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance = null;
        public static GameManager Instance => instance;
        public CinemachineVirtualCamera virtualCamera = null;
        public string targetPortal { get; set; }
        public CombatEntity player { get; set; }
        public List<Task> ongoingTasks { get; set; }

        void Awake()
        {
            instance = this;
            ongoingTasks = new List<Task>();
            DontDestroyOnLoad(gameObject);
        }

        public void EnterScene(string sceneName, string portalName)
        {
            player.gameObject.SetActive(false);
            targetPortal = portalName;
            SceneManager.LoadSceneAsync(sceneName);
        }

        public void EnterScene(string sceneName, Vector position)
        {            
            player.gameObject.SetActive(false);
            player.transform.position = new Vector3(position.x, position.y, position.z);
            SceneManager.LoadSceneAsync(sceneName);            
            player.gameObject.SetActive(true);
        }
    }
}
