using UnityEngine.UI;
using Manager;

namespace UI
{
    public class GoldPanel : BasePanel
    {
        public Text goldText = null;

        void Start()
        {
            goldText.text = InventoryManager.Instance.playerData.golds.ToString();
        }

        public void UpdatePanel()
        {
            goldText.text = InventoryManager.Instance.playerData.golds.ToString();
        }
    }
}