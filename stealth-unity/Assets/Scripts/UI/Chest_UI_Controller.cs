using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class Chest_UI_Controller : MonoBehaviour
    {
        public UI_Inventory chest_inventory_ui;
        public UI_Inventory player_inventory_ui;

        private Inventory chest_inventory;
        private Inventory player_inventory;

        void Awake()
        {
            chest_inventory = chest_inventory_ui.Inventory;
            player_inventory = player_inventory_ui.Inventory;
        }

        private void Start()
        {
            chest_inventory_ui.ItemClickedAction = (Item item) =>
            {
                player_inventory.AddItem(item);
                chest_inventory.RemoveItem(item);

                Repaint();
            };

            player_inventory_ui.ItemClickedAction = (Item item) =>
            {
                chest_inventory.AddItem(item);
                player_inventory.RemoveItem(item);

                Repaint();
            };
        }

        public void SetActive(bool value) => gameObject.SetActive(value);

        private void Repaint()
        {
            chest_inventory_ui.ForceRepaint();
            player_inventory_ui.ForceRepaint();
        }
    }
}
