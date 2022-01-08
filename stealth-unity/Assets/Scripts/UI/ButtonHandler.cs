using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.UI
{

    public class ButtonHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMenuController playerMenuController;

        public void ParentSetInactive()
        {
            UIItemManager.Instance.StopShow(transform.parent.gameObject);
        }

        public void StopMapCamera() => UIItemManager.Instance.StopMapCamera();

        /* Menu Controller Options */
        public void ShowEquipment() => playerMenuController.SelectOption(MenuOption.Equipment);
        public void ShowInventory() => playerMenuController.SelectOption(MenuOption.Inventory);
        public void ShowMap() => playerMenuController.SelectOption(MenuOption.Map);
        public void ShowQuest() => playerMenuController.SelectOption(MenuOption.Quest);
        public void ShowStats() => playerMenuController.SelectOption(MenuOption.Stats);
        public void ShowSettings() => playerMenuController.SelectOption(MenuOption.Settings);
    }
}
