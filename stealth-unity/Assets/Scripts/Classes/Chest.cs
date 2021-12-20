using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Storer
{
    private enum ChestState { Opened, Closed };
    private ChestState state;

    private void Awake()
    {
        this.state = ChestState.Closed;
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && state == ChestState.Opened)
        {
            uiInventory.gameObject.SetActive(false);
            this.state = ChestState.Closed;
        }
        //else if (Input.GetMouseButtonDown(1) && state == ChestState.Closed )
        //{
        //    uiInventory.gameObject.SetActive(false);
        //    gameManager.OpenPlayerInventory();
        //    this.state = ChestState.Opened;
        //    print("Activated");
        //}
    }

    void OnMouseDown()
    {
        //if (Input.GetMouseButtonDown(0))
        //    Debug.Log("Left click on this object");
        if (this.state == ChestState.Closed && Input.GetMouseButtonDown(1))
        {
            uiInventory.gameObject.SetActive(true);
            this.state = ChestState.Opened;
            print("Activated");
        }
        //if (Input.GetMouseButtonDown(2))
        //    Debug.Log("Middle click on this object");
    }

    protected override void OnItemClicked()
    {
        throw new System.NotImplementedException();
    }
}
