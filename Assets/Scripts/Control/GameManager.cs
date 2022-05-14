using App.UI;
using UnityEngine;

namespace App.Control
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
        }

        void Update()
        {
            MessageDispatcher.Instance.DispatchDelay();
        }
    }
}
