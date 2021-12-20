using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson; // custom import

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] protected UI_Inventory uiInventory;

    private Transform teleportTransform;
    private FirstPersonController tpsController;
    private Inventory inventory;

    private void Awake()
    {
        Instance = this;
        teleportTransform = transform.Find(gameObject.name);
        tpsController = teleportTransform.gameObject.GetComponent<FirstPersonController>();

        Debug.Log("Player: " + tpsController.gameObject.name);

        inventory = new Inventory();
        uiInventory.SetInventory(inventory);

        tag = "Player";
        teleportTransform.tag = "Player";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            bool toggle = uiInventory.gameObject.activeSelf ? false : true;
            uiInventory.Show(toggle);
            if (toggle)
                this.DisablePlayer();
            else
                this.EnablePlayer();
        }
    }

    public void MoveTo(Transform target)
    {
        teleportTransform.position = target.position;
        //teleportTransform.rotation = target.rotation;
    }

    public void EnablePlayer()
    {
        this.tpsController.enabled = true;
    }

    public void DisablePlayer()
    {
        this.tpsController.enabled = false;
    }

    public void AddItem(Item item)
    {
        inventory.AddItem(item);
    }

    public void OpenInventory()
    {
        //this.uiInventory.gameObject.SetActive(true);
        uiInventory.Show(true);
    }

    public Vector3 GetPosition()
    {
        return tpsController.transform.position;
    }
}