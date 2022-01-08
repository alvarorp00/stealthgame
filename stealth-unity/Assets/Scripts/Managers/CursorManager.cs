using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    internal class CursorManager : MonoBehaviour
    {
        public Texture2D cursor;
        public Image virtual_cross_cursor; // simulate centered cursor

        private void Awake()
        {
            GameManager.OnGameStateChanged += CursorOnGameStateChange;
            SetCursor(cursor);
        }

        private void CursorOnGameStateChange(GameState from, GameState to)
        {
            Debug.Log($"Cursor update triggered. Status: {to}");
            switch (to)
            {
                case GameState.OnSkinSelection:
                    UnlockCursor();
                    break;
                case GameState.OnLoad:
                    UnlockCursor();
                    break;
                case GameState.OnConversation:
                    UnlockCursor();
                    break;
                case GameState.OnMenu:
                    UnlockCursor();
                    break;
                case GameState.OnChest:
                    UnlockCursor();
                    break;
                case GameState.OnInventory:
                    UnlockCursor();
                    break;
                case GameState.OnPlay:
                    BlockCursor();
                    break;
                case GameState.OnDefeat:
                    UnlockCursor();
                    break;
                case GameState.OnTeleport:
                    BlockCursor();
                    break;
                default:
                    BlockCursor();
                    break;
            }
        }

        private void BlockCursor()
        {
            Debug.Log("Cursor blocked");
            Cursor.lockState = CursorLockMode.Locked;
            virtual_cross_cursor.enabled = true;
        }

        private void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            virtual_cross_cursor.enabled = false;
            Debug.Log("Cursor released");
        }

        private void SetCursor(Texture2D texture)
        {
            Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
        }

        private static Ray CenterRaycast() => Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        public static bool ColliderWasHit(Collider other)
        {
            Physics.Raycast(CenterRaycast(), out RaycastHit hit);
            Debug.Log("Object hit: " + hit.collider.gameObject.name + " @ Dest: " + other.gameObject.name);
            return hit.collider == other;
        }
    }
}
