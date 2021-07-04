using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Inventory.Database;

namespace Inventory.UI
{
    public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] TextMeshProUGUI _itemNameText;

        public Item Item { get; private set; }
        public Button ItemButton { get; private set; }


        public void OnClickRemove()
        {
            EventsManager.On_Inventory_Item_Removed(Item);
        }

        public void Setup(Item item)
        {
            GetComponent<Image>().sprite = item.Sprite;
            Item = item;
            ItemButton = GetComponent<Button>();

            Debug.Log(Item.Description);
            _itemNameText.text = item.Name;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            EventsManager.On_Tool_Tip_Activated(Item.Description);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            EventsManager.On_Tool_Tip_Deactivated();
        }
    } 
}
