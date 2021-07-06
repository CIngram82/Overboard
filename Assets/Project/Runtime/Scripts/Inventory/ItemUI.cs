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


        public void OnClick_Remove()
        {
            EventsManager.On_Inventory_Item_Removed(Item);
        }
        public void GetDropdownOption(int value)
        {
            switch (value)
            {
                case 0: 
                    Player.inspect.Inspect(Item.Prefab);
                    break;
                case 1:
                    Player.inventory.DropItem(Item);
                    break;
                case 2:
                    // Use: calls check if in range and use function
                    break;
                case 3:
                    //combine: auto combines with all combinable items in inventory.
                    break;
                default: break;
            }
        }
        public void Setup(Item item)
        {
            Item = item;
            ItemButton = GetComponent<Button>();

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
