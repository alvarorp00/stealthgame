using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public Sprite coinSprite;
    public Sprite mapSprite;
    public Sprite noteSprite;
    public Sprite axeSprite;
    public Sprite keySprite;

    //public Sprite actionButtonSprite;
    //public Sprite leverSprite;
}