using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    #region Event Actions
    public static Action<string> ToolTipActivated;
    public static Action ToolTipDeactivated;
    public static Action<Item> InventoryItemAdded;
    public static Action<Item> InventoryItemRemoved;
    public static Action LoadInitiated;
    public static Action SaveInitiated;
    #endregion

    #region Event Calls
    public static void On_Tool_Tip_Activated(string text)
    {
        ToolTipActivated?.Invoke(text);
    }
    public static void On_Tool_Tip_Deactivated()
    {
        ToolTipDeactivated?.Invoke();
    }

    public static void On_Inventory_Item_Added(Item item)
    {
        InventoryItemAdded?.Invoke(item);
    }
    public static void On_Inventory_Item_Removed(Item item)
    {
        InventoryItemRemoved?.Invoke(item);
    }

    public static void On_Load_Initiated()
    {
        LoadInitiated?.Invoke();
    }
    public static void On_Saved_Initiated()
    {
        SaveInitiated?.Invoke();
    }
    #endregion
}





