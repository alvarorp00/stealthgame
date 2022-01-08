using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Classes.Items
{
    internal class Axe : Item
    {
        public override Sprite GetSprite()
        {
            return ItemAssets.Instance.axeSprite;
        }

        public override bool IsStackable()
        {
            return false;
        }

        public override void Action()
        {
            Debug.Log("Old axe");
        }

        public override bool IsUsable() => false;
    }
}
