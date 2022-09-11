using UnityEngine;
using UnityEngine.UI;
using Manager;

namespace UI
{
    public class PausePanel : BasePanel
    {
        Button btnClose = null;
        Button btnSetting = null;
        Button btnSave = null;
        Button btnQuit = null;

        void Awake()
        {
            btnClose = transform.GetChild(0).GetComponent<Button>();
            btnSetting = transform.GetChild(1).GetComponent<Button>();
            btnSave = transform.GetChild(2).GetComponent<Button>();
            btnQuit = transform.GetChild(3).GetComponent<Button>();
            btnClose.onClick.AddListener(() => { gameObject.SetActive(false); });
            btnSetting.onClick.AddListener(() => { });
            btnSave.onClick.AddListener(() =>
            {
                GameManager.Instance.onSavingData();
                gameObject.SetActive(false);
            });
            btnQuit.onClick.AddListener(() => { Application.Quit(); });
        }
    }
}