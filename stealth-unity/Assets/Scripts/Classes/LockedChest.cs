using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Classes.Items;

namespace Assets.Scripts.Classes
{
    internal class LockedChest : Chest
    {

        [SerializeField] private Key required_key;

        private void OnTriggerStay(Collider other)
        {
            if (Player.Instance.CompareTag(other.tag))
            {
                if (Player.Instance.HasItem(required_key))
                    OpenOrClose(other);
                else
                {
                    Debug.LogWarning($"Key {required_key.item_name} is mandatory");
                }
            }
        }

    }
}
