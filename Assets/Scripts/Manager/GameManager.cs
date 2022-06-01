using System;
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
        public Action onSavingData = null;
        public string targetPortal { get; set; }
        public Entity player { get; set; }
        public CinemachineVirtualCamera virtualCamera { get; set; }
        public static GameManager Instance => instance;

        void Awake()
        {
            instance = this;
            virtualCamera = transform.GetChild(0).GetChild(0).GetComponent<CinemachineVirtualCamera>();
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
