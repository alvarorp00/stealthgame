using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemSetChanged;

    public HashSet<Item> itemSet;

    public Inventory()
    {
        itemSet = new HashSet<Item>();
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemInInventory = false;
            foreach(Item invItem in itemSet)
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

    public HashSet<Item> GetItemSet()
    {
        return this.itemSet;
    }
}