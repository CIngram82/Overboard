using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI _itemNameText;

    public Item Item { get; private set; }


    public void Setup(Item item)
    {
        Item = item;
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
