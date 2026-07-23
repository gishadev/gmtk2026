using System.Collections.Generic;
using gishadev.gmtk.Interactions.InventoryManager.Usables;
using UnityEngine;

namespace gishadev.walkingSimulator.EventsManager
{
    public abstract class GameEvent
    {
    }

    public class HintRequestedEvent : GameEvent
    {
        public string Text { get; }
        public Transform TargetTrans { get; }

        public HintRequestedEvent(string text, Transform targetTrans)
        {
            Text = text;
            TargetTrans = targetTrans;
        }
    }

    public class HintDismissedEvent : GameEvent
    {
    }

    public class InventoryUpdatedEvent : GameEvent
    {
        public IEnumerable<IUsable> InventoryUsables { get; }
        public int EquippedIndex { get; }

        public InventoryUpdatedEvent(IEnumerable<IUsable> inventoryUsables, int equippedIndex)
        {
            InventoryUsables = inventoryUsables;
            EquippedIndex = equippedIndex;
        }
    }
}