using System;
using UnityEngine;
using Inventory.Database;

public class EventsManager : MonoBehaviour
{
    #region Event Actions
    public static Action<string> ToolTipActivated;
    public static Action ToolTipDeactivated;
    public static Action<bool> JournalOpened;
    public static Action<bool> GamePaused;
    public static Action<Item> InventoryItemAdded;
    public static Action<Item> InventoryItemRemoved;
    public static Action<Clue> InventoryClueAdded;
    public static Action<Clue> InventoryClueRemoved;
    public static Action<bool> CameraSwitched;
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

    public static void On_Journal_Opened(bool open)
    {
        JournalOpened?.Invoke(open);
    }

    public static void On_Camera_Switched(bool state)
    {
        CameraSwitched?.Invoke(state);
    }

    public static void On_Inventory_Item_Added(Item item)
    {
        InventoryItemAdded?.Invoke(item);
    }
    public static void On_Inventory_Item_Removed(Item item)
    {
        InventoryItemRemoved?.Invoke(item);
    }
    public static void On_Inventory_Clue_Added(Clue clue)
    {
        InventoryClueAdded?.Invoke(clue);
    }
    public static void On_Inventory_Clue_Removed(Clue clue)
    {
        InventoryClueRemoved?.Invoke(clue);
    }
    #endregion
}





