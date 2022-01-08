using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Classes.Items
{
    internal class Key : Item
    {
        public override void Action()
        {
            // pass
        }

        public override Sprite GetSprite()
        {
            return ItemAssets.Instance.keySprite;
        }

        public override bool IsStackable()
        {
            return false;
        }

        public override bool IsUsable()
        {
            return false; // not going to use this
        }
    }
}
