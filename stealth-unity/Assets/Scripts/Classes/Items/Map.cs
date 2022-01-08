using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts
{
    public class Map : Item
    {
        public override Sprite GetSprite()
        {
            return ItemAssets.Instance.mapSprite;
        }

        public override bool IsStackable()
        {
            return false;
        }

        public override void Action() {}

        public override bool IsUsable() => true;

    }

}