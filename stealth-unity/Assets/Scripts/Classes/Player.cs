using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson; // custom import
using Assets.Scripts.Managers;
using Assets.Scripts.Classes;
using Assets.Scripts.UI;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }

        [SerializeField] protected UI_Inventory uiInventory;

        private Transform teleportTransform;
        private FirstPersonController tpsController;

        // Inventory related actions
        public Inventory Inventory { get; private set; }
        private InventoryState inventoryState;
        public Action<InventoryState, InventoryState> OnInventoryStateChanged;
        public Action<Item> OnItemTaken;
        private bool allow_inventory_show;

        // Health
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private AudioClip onDamageSound;
        [SerializeField] private AudioClip onHealSound;

        private void Awake()
        {
            Instance = this;
            teleportTransform = transform.Find(gameObject.name);
            tpsController = teleportTransform.gameObject.GetComponent<FirstPersonController>();

            Debug.Log("Player: " + tpsController.gameObject.name);

            Inventory = new Inventory(uiInventory.maxGameObjects);
            uiInventory.SetInventory(Inventory);
            inventoryState = InventoryState.Closed;

            tag = "Player";
            teleportTransform.tag = "Player";

            // subscribe events!
            OnInventoryStateChanged += PlayerOnInventoryStateChange;
            GameManager.OnGameStateChanged += PlayerOnGameStateChange;

            allow_inventory_show = true;

            UIItemManager.Instance.OnUIItemShow += (state) => allow_inventory_show = state == UIItemManager.UIItemShowState.NotShowing;
        }

        private void PlayerOnInventoryStateChange(InventoryState oldState, InventoryState newState)
        {
            switch (newState)
            {
                case InventoryState.Opened:
                    DisablePlayer();
                    break;
                case InventoryState.Closed:
                    EnablePlayer();
                    break;
                case InventoryState.Wait:
                    EnablePlayer();
                    break;
                case InventoryState.Block:
                    Debug.Log("Player & inventory blocked.");
                    DisablePlayer();
                    break;
                default:
                    break;
            }
        }

        private void PlayerOnGameStateChange(GameState oldState, GameState newState)
        {
            if (oldState == GameState.OnChest && newState == GameState.OnPlay)
            {
                UpdateInventoryState(InventoryState.Wait);
            }
            else if (newState == GameState.OnPlay)
            {
                UpdateInventoryState(InventoryState.Closed);
            }
            else
            {
                UpdateInventoryState(InventoryState.Block);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && allow_inventory_show)
            {
                ShowInventory();
            }

            if (Input.GetKeyDown(KeyCode.J))
                Damage(1);

            if (Input.GetKeyDown(KeyCode.K))
                Heal(1);
        }

        public void MoveTo(Transform target)
        {
            teleportTransform.position = target.position;
            teleportTransform.rotation = target.rotation;
        }

        private void UpdateInventoryState(InventoryState state)
        {
            InventoryState oldState = inventoryState;
            inventoryState = state;

            OnInventoryStateChanged?.Invoke(oldState, state);
        }

        private void ShowInventory()
        {
            switch (inventoryState)
            {
                case InventoryState.Opened:
                    //uiInventory.Show(false);
                    PlayerMenuController.Instance.Show(false);
                    UpdateInventoryState(InventoryState.Closed);
                    GameManager.Instance.UpdateGameState(GameState.OnPlay);
                    break;
                case InventoryState.Closed:
                    //uiInventory.Show(true);
                    PlayerMenuController.Instance.Show(true);
                    GameManager.Instance.UpdateGameState(GameState.OnInventory);
                    UpdateInventoryState(InventoryState.Opened);
                    break;
                case InventoryState.Wait:
                    //uiInventory.Show(false); // just in case
                    PlayerMenuController.Instance.Show(false);
                    GameManager.Instance.UpdateGameState(GameState.OnPlay);
                    UpdateInventoryState(InventoryState.Closed);
                    break;
                case InventoryState.Block:
                    PlayerMenuController.Instance.Show(false);
                    //uiInventory.Show(false);
                    break;
                default:
                    break; // do nothing
            }
        }

        public void EnablePlayer() => tpsController.enabled = true;

        public void DisablePlayer() => tpsController.enabled = false;

        public void AddItem(Item item)
        {
            Inventory.AddItem(item);
            OnItemTaken?.Invoke(item);
        }

        public bool HasItem(Item item) => Inventory.itemSet.Contains(item);

        public bool RemoveItem(Item item) => Inventory.RemoveItem(item);

        public Vector3 GetPosition()
        {
            return tpsController.transform.position;
        }

        public void Damage(ushort damage)
        {
            healthSystem.Damage(damage);
            GameManager.Instance.ReproduceSoundEffect(onDamageSound);
        }

        public void Heal(ushort heal)
        {
            healthSystem.Heal(heal);
            GameManager.Instance.ReproduceSoundEffect(onHealSound);
        }

        public short Health => healthSystem.Health;
        public short MaxHealth => healthSystem.MaxHealth;
    }
}