using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public override void Action()
    {
        Debug.Log("Mostrar mapa");
    }
}
