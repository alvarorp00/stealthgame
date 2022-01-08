using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public static event Action<GameState, GameState> OnGameStateChanged;
        public static event Action<RegionSelector> OnSceneChanged;

        [SerializeField] private AudioClip _spawnSound;

        public enum RegionSelector { Menu, Forest, Crater };

        /* Positions */
        public Transform spawnPoint;

        /* Dialogue */
        public DialogueManager dialogueManager;

        /* Event Systems */
        public GameObject gameEventSystem;

        /* ---- */
        /* PRIV */

        /* State */
        private GameState State;
        private bool allow_state_update;

        /* Other */
        private AudioSource s_sound;

        /* Scenes */
        private RegionSelector currentRegion;

        void Awake()
        {
            if (Instance == null) // If there is no instance already
            {
                //DontDestroyOnLoad(gameObject); // Keep the GameObject, this component is attached to, across different scenes
                Instance = this;
                currentRegion = RegionSelector.Menu;
                StartCoroutine(SetUp());
            }
            else if (Instance != this) // If there is already an instance and it's not `this` instance
            {
                Destroy(gameObject); // Destroy the GameObject, this component is attached to
            }
        }

        void Start()
        {
            
        }

        void Update()
        {

        }

        /* PRIVATE */

        private IEnumerator Unload(string sceneName)
        {
            yield return null;
            SceneManager.UnloadSceneAsync(sceneName);
        }

        private IEnumerator Load(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            yield return null; // wait for load cycle
        }

        private IEnumerator SetUp()
        {
            gameEventSystem.GetComponent<EventSystem>().enabled = false;

            SceneManager.LoadScene(RegionName(currentRegion), LoadSceneMode.Additive); // initial scene
            currentRegion = RegionSelector.Menu;

            yield return null; // wait 1 cycle

            if (spawnPoint == null)
                throw new Exception("Can't access spawnpoint");

            s_sound = GetComponents<AudioSource>()[0]; // second one is loop player

            TeleportPlayer(spawnPoint);
            StartCoroutine(Waiter(0.5f));
            allow_state_update = false;
            UpdateGameState(GameState.OnLoad, true);
            allow_state_update = true;
        }

        private string RegionName(RegionSelector region)
        {
            return region switch
            {
                RegionSelector.Forest => "Forest",
                RegionSelector.Crater => "Crater",
                RegionSelector.Menu => "MainMenu",
                _ => null,
            };
        }
        IEnumerator ChangePos(Transform target)
        {
            UpdateGameState(GameState.OnTeleport);
            yield return Waiter(.7f);
            Player.Instance.MoveTo(target);
            yield return Waiter(.7f);
            UpdateGameState(GameState.OnPlay);
        }

        IEnumerator Waiter(float seconds)
        {
            //Wait for given seconds
            yield return new WaitForSeconds(seconds);
        }

        /* PUBLIC */

        public void UpdateGameState(GameState newState, bool force = false)
        {
            if (allow_state_update || force)
            {
                GameState oldState = State;
                State = newState;

                switch (newState)
                {
                    case GameState.OnSkinSelection:
                        break;
                    case GameState.OnLoad:
                        break;
                    case GameState.OnConversation:
                        break;
                    case GameState.OnMenu:
                        break;
                    case GameState.OnChest:
                        break;
                    case GameState.OnInventory:
                        break;
                    case GameState.OnPlay:
                        break;
                    case GameState.OnDefeat:
                        break;
                    case GameState.OnTeleport:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
                }

                OnGameStateChanged?.Invoke(oldState, newState);
            }
        }

        public void SpawnPlayer()
        {
            ReproduceSoundEffect(_spawnSound);
            SwitchScene(RegionSelector.Forest, spawnPoint, null);
            StartCoroutine(Waiter(0.3f));
            gameEventSystem.GetComponent<EventSystem>().enabled = true;
        }

        public void TeleportPlayer(Transform target, AudioClip c_sound = null)
        {
            if (State == GameState.OnPlay || State == GameState.OnLoad)
            {
                Debug.Log("Teleport launched towards: " + target.name + " | at: " + target.position);

                StartCoroutine(ChangePos(target));
                ReproduceSoundEffect(c_sound);
            }
        }

        public void SwitchScene(RegionSelector region, Transform target, AudioClip c_sound)
        {
            if (currentRegion == region)
            {
                Debug.Log("Warning: switching between same region");
            }
            else
            {
                string newRegion = RegionName(region);
                string oldRegion = RegionName(currentRegion);
                StartCoroutine(Load(newRegion));
                TeleportPlayer(target, c_sound);
                StartCoroutine(Waiter(0.5f));
                StartCoroutine(Unload(oldRegion));
                currentRegion = region; // not mixing with permanent scene
                OnSceneChanged?.Invoke(currentRegion);
            }
        }

        public void ReproduceSoundEffect(AudioClip c_sound = null)
        {
            if (c_sound)
            {
                s_sound.clip = c_sound;
                s_sound.Play();
            }
        }

        public void AddItemToStorer(Storer storer, Item item)
        {
            storer.AddItem(item);
        }

        public bool IsPlayerCloseTo(Vector3 pos, float thres)
        {
            return Vector3.Distance(pos, Player.Instance.gameObject.transform.position) < thres;
        }
    }

    public enum GameState
    {
        OnSkinSelection,
        OnLoad,
        OnConversation,
        OnMenu,
        OnChest,
        OnInventory,
        OnPlay,
        OnDefeat,
        OnTeleport
    }
}