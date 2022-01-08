using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.UI;

namespace Assets.Scripts
{
    public class Chest : Storer
    {
        public List<Item> items = new List<Item>();
        public Collider object_to_raid; // check what object is going to be hit by raycast

        [SerializeField] public Chest_UI_Controller inventoryUIContainer;
        [SerializeField] private UI_Inventory chestInventory;
        [SerializeField] private UI_Inventory playerInventory;

        private enum ChestState { Opened, Closed };
        private ChestState state;

        private void Awake()
        {
            inventory = new Inventory(chestInventory.maxGameObjects);
        }

        private void Start()
        {
            state = ChestState.Closed;

            chestInventory = inventoryUIContainer.chest_inventory_ui;
            playerInventory = inventoryUIContainer.player_inventory_ui;

            chestInventory.SetInventory(inventory);
            playerInventory.SetInventory(Player.Instance.Inventory);
            foreach (Item item in items)
                inventory.AddItem(item);
        }

        private void OnTriggerStay(Collider other)
        {
            OpenOrClose(other); // separate in this way to possibly create new locked chests and that
        }

        protected void OpenOrClose(Collider other)
        {
            if (Player.Instance.CompareTag(other.tag) && !block_storer)
            {
                if (state == ChestState.Closed && Input.GetMouseButton(1) && CursorManager.ColliderWasHit(object_to_raid))
                {
                    OnOpen();
                    inventoryUIContainer.SetActive(true);
                    chestInventory.Show(true);
                    playerInventory.Show(true);
                    state = ChestState.Opened;
                    GameManager.Instance.UpdateGameState(GameState.OnChest);
                    Debug.Log("Chest opened.");
                }
                else if (state == ChestState.Opened && Input.GetKeyDown(KeyCode.E))
                {
                    OnClose();
                    inventoryUIContainer.SetActive(false);
                    chestInventory.Show(true);
                    playerInventory.Show(true);
                    this.state = ChestState.Closed;
                    GameManager.Instance.UpdateGameState(GameState.OnPlay);
                    Debug.Log("Chest closed.");
                }
            }
        }
    }
}
