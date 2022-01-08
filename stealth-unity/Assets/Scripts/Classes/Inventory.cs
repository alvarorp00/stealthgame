using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Inventory
    {
        public event EventHandler OnItemSetChanged;

        public HashSet<Item> itemSet;

        public long MaxItems { get; private set; }
        public long CurrentItems { get; private set; }

        public Inventory(long MaxItems)
        {
            itemSet = new HashSet<Item>();
            this.MaxItems = MaxItems;
        }

        public void AddItem(Item item)
        {
            if (itemSet.Count >= MaxItems)
            {
                Debug.Log("Inventory is full!");
            }
            else
            {
                Debug.Log($"adding item {item}");
                if (item.IsStackable())
                {
                    bool itemInInventory = false;
                    foreach (Item invItem in itemSet)
                    {
                        if (invItem.GetType().Name == item.GetType().Name)
                        {
                            invItem.amount += item.amount;
                            itemInInventory = true;
                        }
                    }
                    if (itemInInventory == false)
                        itemSet.Add(item);
                }
                else
                {
                    itemSet.Add(item);
                }
                OnItemSetChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool RemoveItem(Item item)
        {
            bool ret = itemSet.Remove(item);
            if (ret)
                OnItemSetChanged?.Invoke(this,EventArgs.Empty);
            return ret;
        }

        public HashSet<Item> GetItemSet()
        {
            return this.itemSet;
        }
    }

    public enum InventoryState { Opened, Closed, Wait, Block };
}