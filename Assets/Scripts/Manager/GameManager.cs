using UnityEngine;
using App.Control;
using App.UI;

namespace App.Manager
{
    public class GameManager : MonoBehaviour
    {
        GameObject player = null;
        static GameManager instance = null;
        public static GameManager Instance => instance;
        public UICanvas canvas = null;

        void Awake()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            player = GameObject.FindWithTag("Player");
            canvas = GameObject.Find("UICanvas").GetComponent<UICanvas>();
        }

        void Update()
        {
            MessageDispatcher.Instance.DispatchDelay();
        }
    }
}