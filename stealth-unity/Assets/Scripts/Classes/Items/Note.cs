using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // gameManager.showAlert("...");
        Debug.Log(message);
    }
}
