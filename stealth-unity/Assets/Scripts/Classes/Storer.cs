using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storer : MonoBehaviour
{
    [SerializeField] protected UI_Inventory uiInventory;

    protected enum StorerState { OnPlayerInventory, OnChestInventory, OnVoid };

    protected Inventory inventory;
    protected GameManager gameManager;

    // Start is called before the first frame update
    private void Awake()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(Item item)
    {
        inventory.AddItem(item);
    }

    protected virtual void OnItemClicked()
    {
        // pass
    }
}
