using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public override Sprite GetSprite()
    {
        return ItemAssets.Instance.coinSprite;
    }

    public override bool IsStackable()
    {
        return true;
    }

    public override void Action()
    {
        Debug.Log("Monedas"); // just this
    }
}
