using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Inventory.Database;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI _itemNameText;

    public Item Item { get; private set; }
    public Button ItemButton { get; private set; }


    public void OnClickRemove()
    {
        GameEvents.On_Inventory_Item_Removed(Item);
    }

    public void Setup(Item item)
    {
        Item = item;
        ItemButton = GetComponent<Button>();

        Debug.Log(Item.Description);
        _itemNameText.text = item.Name;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameEvents.On_Tool_Tip_Activated(Item.Description);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        GameEvents.On_Tool_Tip_Deactivated();
    }
}
