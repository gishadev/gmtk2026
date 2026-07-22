using gishadev.walkingSimulator.Interactions.InventoryManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace gishadev.walkingSimulator.UI
{
    public class InventorySlotGUIHandler : MonoBehaviour
    {
        [SerializeField] private GameObject content;

        [SerializeField] private TMP_Text slotTMP;
        [SerializeField] private Image slotImage;

        public void UpdateSlotContent(InventoryItemDataSO itemData)
        {
            slotTMP.text = itemData.ItemName.GetLocalizedString();
            slotImage.sprite = itemData.ItemIcon;
            ShowContent();
        }

        public void SetEquipped(bool isEquipped)
        {
            transform.localScale = isEquipped ? Vector3.one * 1.1f : Vector3.one * 1f;
        }

        public void HideContent()
        {
            content.SetActive(false);
        }

        private void ShowContent()
        {
            content.SetActive(true);
        }
    }
}