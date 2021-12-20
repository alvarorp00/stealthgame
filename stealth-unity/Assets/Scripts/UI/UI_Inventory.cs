using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    //private GameManager gameManager;
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private bool pendingRefresh = false;

    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate  = itemSlotContainer.Find("ItemSlotTemplate");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemSetChanged += Inventory_OnItemSetChanged;

        RefreshInventoryItems();
    }

    public void Show(bool show)
    {
        this.gameObject.SetActive(show);
        if (show && pendingRefresh)
        {
            RefreshInventoryItems();
            pendingRefresh = false;
        }

        //Cursor.visible = show;
    }

    private void Inventory_OnItemSetChanged(object sender, System.EventArgs e)
    {
        pendingRefresh = true;
    }

    private void RefreshInventoryItems()
    {
        if (itemSlotContainer != null)
        {
            foreach (Transform child in itemSlotContainer)
            {
                if (child == itemSlotTemplate) continue;
                Destroy(child.gameObject);
            }
        }
        else
        {
            Debug.Log("Warning: null container");
        }

        int x = 1;
        int y = -1;
        float itemSlotCellSize = 75f;
        foreach (Item item in inventory.GetItemSet())
        {
            RectTransform itemSlotRectTransform =  Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();

            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);

            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("Amount").GetComponent<TextMeshProUGUI>();

            if (item.amount > 1)
                uiText.SetText(item.amount.ToString());

            Item slotItem = itemSlotRectTransform.Find("Item").GetComponent<Item>();
            slotItem = item;

            //Button button = itemSlotRectTransform.Find("Button").GetComponent<Button>();
            //button.onClick.AddListener(() =>
            //{
            //    item.Action();
            //});



            x++;
            if (x > 5) // 5 per row
            {
                x = 1;
                y--;
            }
        }
    }
}
