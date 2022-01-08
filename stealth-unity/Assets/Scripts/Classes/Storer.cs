using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts
{
    public abstract class Storer : MonoBehaviour
    {

        [SerializeField] public AudioClip open_sound;
        [SerializeField] public AudioClip close_sound;

        protected bool block_storer;

        protected Inventory inventory;

        // Start is called before the first frame update
        private void Awake()
        {
            block_storer = false;
            Player.Instance.OnInventoryStateChanged += (oldSt, newSt) => block_storer = newSt == InventoryState.Opened;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddItem(Item item)
        {
            inventory.AddItem(item);
        }

        protected void OnOpen() => GameManager.Instance.ReproduceSoundEffect(open_sound);

        protected void OnClose() => GameManager.Instance.ReproduceSoundEffect(close_sound);
    }
    
}