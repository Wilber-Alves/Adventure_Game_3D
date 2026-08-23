using UnityEngine;
using static Items.ItemManager;
using UnityEngine.UI;
using TMPro;

namespace Items
{
    public class ItemLayout : MonoBehaviour
    {

        private ItemSetup _currentSetup;

        public Image uiIcon;
        public TextMeshProUGUI uiValue;

        public Image uiIcone;
        public void Load(ItemSetup setup)
        {
            _currentSetup = setup;
            UpdateUI();
        }

        private void UpdateUI()
        {
            uiIcon.sprite = _currentSetup.icon;
        
        }
        private void Update()
        {
            uiValue.text = _currentSetup.soInt.valueInt.ToString();
        }
    }
}