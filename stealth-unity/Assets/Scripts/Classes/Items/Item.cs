using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item: MonoBehaviour
{
    public uint amount = 1;
    [SerializeField] public AudioClip c_sound;

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
                    this.gameObject.SetActive(false);
                    child.gameObject.SetActive(false);
                    GameManager.Instance.ReproduceSoundEffect(c_sound);
                }
            }
        }
    }

    public virtual Sprite GetSprite()
    {
        return null;
    }

    public virtual bool IsStackable()
    {
        return false;
    }

    public virtual void Action()
    {
        // pass;
    }
}