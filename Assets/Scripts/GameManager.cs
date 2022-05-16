using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using App.Control;
using App.UI;

namespace App
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance = null;
        public static GameManager Instance => instance;
        public UICanvas canvas = null;
        public Dictionary<string, MoveEntity> entities;
        public List<Quest> ongoingQuests = new List<Quest>();

        void Awake()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            canvas = GameObject.Find("UICanvas").GetComponent<UICanvas>();
            entities = GameObject.FindObjectsOfType<MoveEntity>().ToDictionary(entity => entity.name);
        }

        void Update()
        {
            MessageDispatcher.Instance.DispatchDelay();
        }
    }
}
