using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PlayerMenuController : MonoBehaviour
    {
        public static PlayerMenuController Instance { get; private set;}

        [SerializeField] private GameObject equipment_controller;
        [SerializeField] private UI_Inventory inventory_controller;
        [SerializeField] private GameObject map_controller;
        [SerializeField] private UITaskController quests_controller;
        [SerializeField] private GameObject stats_controller;
        [SerializeField] private GameObject settings_controller;

        [SerializeField] private Map map_required;

        private MenuOption menuOptionLast; // last option selected

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void Start()
        {
            gameObject.SetActive(false); // disable render of this
            menuOptionLast = MenuOption.Equipment; // by default we start with the first one
        }

        public void SelectOption(MenuOption option)
        {
            Debug.Log("Menu option selected: " + option);

            switch (option)
            {
                case MenuOption.Equipment:
                    ShowEquipment();
                    break;
                case MenuOption.Inventory:
                    ShowInventory();
                    break;
                case MenuOption.Map:
                    ShowMap();
                    break;
                case MenuOption.Quest:
                    ShowQuests();
                    break;
                case MenuOption.Stats:
                    ShowStats();
                    break;
                case MenuOption.Settings:
                    ShowSettings();
                    break;
                default:
                    break;
            }
        }

        public void DisableOption(MenuOption option)
        {
            switch (option)
            {
                case MenuOption.Equipment:
                    HideEquipment();
                    break;
                case MenuOption.Inventory:
                    HideInventory();
                    break;
                case MenuOption.Map:
                    HideMap();
                    break;
                case MenuOption.Quest:
                    HideQuests();
                    break;
                case MenuOption.Stats:
                    HideStats();
                    break;
                case MenuOption.Settings:
                    HideSettings();
                    break;
                default:
                    break;
            }
        }

        /* Consistency */

        public void Show(bool show)
        {
            gameObject.SetActive(show);
            Debug.Log("Menu option selected: " + menuOptionLast);
            if (show)
                SelectOption(menuOptionLast);
            else
                DisableOption(menuOptionLast);
        }

        /* Enablers */

        private void ShowEquipment()
        {
            DisableOption(menuOptionLast);
            equipment_controller.SetActive(true);
            menuOptionLast = MenuOption.Equipment;
            Debug.Log("Showing Equipment");
        }

        private void ShowInventory()
        {
            DisableOption(menuOptionLast);
            inventory_controller.Show(true);
            menuOptionLast = MenuOption.Inventory;
        }

        private void ShowMap()
        {
            DisableOption(menuOptionLast);
            map_controller.SetActive(true);
            menuOptionLast = MenuOption.Map;

            GameObject.FindGameObjectWithTag("MapCamera").GetComponent<Camera>().enabled = Player.Instance.HasItem(map_required);
        }

        private void ShowQuests()
        {
            DisableOption(menuOptionLast);
            quests_controller.Show();
            menuOptionLast = MenuOption.Quest;
        }

        private void ShowStats()
        {
            DisableOption(menuOptionLast);
            stats_controller.SetActive(true);
            menuOptionLast = MenuOption.Stats;
        }

        private void ShowSettings()
        {
            DisableOption(menuOptionLast);
            settings_controller.SetActive(true);
            menuOptionLast = MenuOption.Settings;
        }

        /* Disablers */

        private void HideEquipment() => equipment_controller.SetActive(false);

        private void HideInventory() => inventory_controller.Show(false);

        private void HideMap()
        {
            map_controller.SetActive(false);
            GameObject.FindGameObjectWithTag("MapCamera").GetComponent<Camera>().enabled = false;
        }

        private void HideQuests() => quests_controller.Hide();

        private void HideStats() => stats_controller.SetActive(false);

        private void HideSettings() => settings_controller.SetActive(false);
    }

    public enum MenuOption
    {
        Equipment,
        Inventory,
        Map,
        Quest,
        Stats,
        Settings
    }
}