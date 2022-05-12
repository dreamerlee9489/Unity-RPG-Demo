using UnityEngine;

namespace Game.Control
{
	public class GameManager : MonoBehaviour
	{
		static GameManager instance = null;
		public static GameManager Instance => instance;

		void Awake()
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		void Update()
		{
			MessageDispatcher.Instance.DispatchDelay();
		}
	}
}
