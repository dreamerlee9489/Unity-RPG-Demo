using System.Collections.Generic;
using UnityEngine;
using App.Control;
using App.UI;

namespace App.Manager
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance = null;
        public static GameManager Instance => instance;
        public UICanvas canvas = null;
        public Dictionary<string, Transform> entities = new Dictionary<string, Transform>();

        void Awake()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            canvas = GameObject.Find("UICanvas").GetComponent<UICanvas>();
        }

        void Update()
        {
            MessageDispatcher.Instance.DispatchDelay();
        }
    }
}
