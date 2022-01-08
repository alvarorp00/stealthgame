using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace Assets.Scripts.UI
{
    public class UI_Inventory : MonoBehaviour
    {
        //public int rows_per_column;
        //public int max_columns;
        public int maxGameObjects;
        public UnityAction<Item> ItemClickedAction { get; set; }
        public Inventory Inventory { get; private set; }

        private Transform itemSlotContainer;
        private Transform itemInfo;

        private Transform[] itemSlots;
        private long MaxItems;

        private bool pendingRefresh = false;

        private void Awake()
        {
            itemSlotContainer = transform.Find("ItemSlotContainer");
            itemInfo = transform.Find("ItemInfo");
            if (itemInfo != null)
                itemInfo.gameObject.SetActive(false);

            itemSlots = new Transform[maxGameObjects];
            Transform itemSlotsBox = itemSlotContainer.Find("Slots");

            uint i = 0;
            foreach (Transform row in itemSlotsBox)
            {
                foreach (Transform slot in row)
                {
                    slot.gameObject.AddComponent<Button>();
                    itemSlots[i++] = slot; // now we have the slots here
                }
            }
            MaxItems = i;
            //itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
            //ItemClickedAction = (Item item) => item.Action(); // by default, item action
            ItemClickedAction = (Item item) =>
            {
                itemInfo.gameObject.SetActive(true);
                itemInfo.Find("Name").GetComponent<Text>().text = item.item_name;
                itemInfo.Find("Description").GetComponent<Text>().text = item.item_description;
                item.Action();
            };
        }

        public void SetInventory(Inventory inventory)
        {
            this.Inventory = inventory;

            inventory.OnItemSetChanged += Inventory_OnItemSetChanged;

            //RefreshInventoryItems();
            pendingRefresh = true;
        }

        public void Show(bool show)
        {
            gameObject.SetActive(show);
            if (show && pendingRefresh)
            {
                RefreshInventoryItems();
                pendingRefresh = false;
            }

            if (!show && itemInfo)
            {
                itemInfo.gameObject.SetActive(false);
            }
        }

        private void Inventory_OnItemSetChanged(object sender, EventArgs e)
        {
            pendingRefresh = true;
        }

        private void RefreshInventoryItems()
        {
            Debug.Log("Drawing: " + gameObject.name + "@ Max items: " + MaxItems);
            UnityAction partializedAction;

            uint draw_pos = 0;
            foreach (Item item in Inventory.GetItemSet())
            {
                Image image = itemSlots[draw_pos].Find("Image").GetComponent<Image>();
                image.sprite = item.GetSprite();

                Button button = itemSlots[draw_pos].gameObject.GetComponent<Button>();
                //button?.onClick.AddListener(item.Action);
                partializedAction = () => ItemClickedAction.Invoke(item);
                button?.onClick.AddListener(partializedAction);

                TextMeshProUGUI uiText = itemSlots[draw_pos].Find("Amount").GetComponent<TextMeshProUGUI>();

                Item slotItem = itemSlots[draw_pos].Find("Item").GetComponent<Item>();
                slotItem = item;

                draw_pos++;
            }
            for (; draw_pos < MaxItems; draw_pos++)
            { // clear sprites
                Image image = itemSlots[draw_pos].Find("Image").GetComponent<Image>();
                image.sprite = null;

                Button button = itemSlots[draw_pos].gameObject.GetComponent<Button>();
                button?.onClick.RemoveAllListeners();

                TextMeshProUGUI uiText = itemSlots[draw_pos].Find("Amount").GetComponent<TextMeshProUGUI>();
                uiText?.SetText(" ");
                _ = itemSlots[draw_pos].Find("Item").GetComponent<Item>();
            }
        }

        public void ForceRepaint() => RefreshInventoryItems();
    }
}