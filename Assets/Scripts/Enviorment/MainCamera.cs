using UnityEngine;
using Cinemachine;

namespace App.Enviorment
{
	public class MainCamera : MonoBehaviour
	{
		CinemachineVirtualCamera virtualCamera = null;
        Portal[] portals = null;

		void Awake()
		{
            portals = GameObject.FindObjectsOfType<Portal>();
			virtualCamera = transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
			virtualCamera.Follow = GameManager.Instance.player.transform;			
		}
	}
}
