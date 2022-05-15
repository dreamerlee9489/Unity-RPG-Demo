using UnityEngine;
using App.Control;

namespace App.Manager
{
    public class GameManager : MonoBehaviour
    {
        GameObject player = null;
        static GameManager instance = null;
        public static GameManager Instance => instance;
        public Canvas canvas = null;

        void Awake()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            player = GameObject.FindWithTag("Player");
            canvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        }

        void Update()
        {
            MessageDispatcher.Instance.DispatchDelay();
        }
    }
}
