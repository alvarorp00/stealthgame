using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts
{
    public class Note : Item
    {
        public string message;

        public override Sprite GetSprite()
        {
            return ItemAssets.Instance.noteSprite;
        }

        public override bool IsStackable()
        {
            return false;
        }

        public override void Action()
        {
            Debug.Log(message);
            UIItemManager.Instance.ShowNote(this); // show note
        }

        public override bool IsUsable() => true;
    }
}
