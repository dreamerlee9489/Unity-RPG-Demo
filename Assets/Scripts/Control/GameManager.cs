using UnityEngine;

namespace Game.Control
{
    public class GameManager : MonoBehaviour
    {
        GameObject player = null;
        static GameManager instance = null;
        public static GameManager Instance => instance;

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
