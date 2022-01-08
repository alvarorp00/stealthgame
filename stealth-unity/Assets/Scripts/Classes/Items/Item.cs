using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public abstract class Item : MonoBehaviour
    {
        public uint amount = 1;
        [SerializeField] public AudioClip c_sound;

        public string item_name;
        public string item_description;

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == Player.Instance.tag)
            {
                if (Input.GetKeyDown(KeyCode.T) == true)
                {
                    Item child = this.transform.GetComponent<Item>();
                    if (child != null)
                    {
                        Debug.Log("Item: " + child.name);
                        Player.Instance.AddItem(child);
                        gameObject.SetActive(false);
                        child.gameObject.SetActive(false);
                        GameManager.Instance.ReproduceSoundEffect(c_sound);
                    }
                }
            }
        }

        public abstract Sprite GetSprite();

        public abstract bool IsStackable();

        public abstract bool IsUsable();

        public abstract void Action();

        public override int GetHashCode() => base.GetHashCode();
    }
}