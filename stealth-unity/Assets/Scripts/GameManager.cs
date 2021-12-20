using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action<GameState> OnGameStateChanged;

    [SerializeField] private AudioClip _spawnSound;

    public enum RegionSelector { Menu, Forest, Crater };

    /* Positions */
    public Transform spawnPoint;

    /* Dialogue */
    public DialogueManager dialogueManager;

    /* State */
    private GameState State;

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
        }
        else if (Instance != this) // If there is already an instance and it's not `this` instance
        {
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
        }
    }

    void Start()
    {
        StartCoroutine(SetUp());
    }

    void Update()
    {
        
    }

    //private void OnApplicationFocus(bool focusStatus)
    //{
    //    if (focusStatus && !GameManager.Instance.dialogueManager.Active())
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //    }
    //    else
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //    }

    //    Debug.Log("Triggered: " + GameManager.Instance.dialogueManager.Active());
    //}

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
        State = GameState.OnLoad;

        SceneManager.LoadScene(RegionName(currentRegion), LoadSceneMode.Additive); // initial scene
        currentRegion = RegionSelector.Menu;

        yield return null; // wait 1 cycle

        if (spawnPoint == null)
            throw new Exception("Can't access spawnpoint");

        s_sound = GetComponents<AudioSource>()[0]; // second one is loop player

        TeleportPlayer(spawnPoint);
    }

    private string RegionName(RegionSelector region)
    {
        return region switch
        {
            RegionSelector.Forest => "Forest",
            RegionSelector.Crater => "Crater",
            RegionSelector.Menu   => "MainMenu",
            _ => null,
        };
    }
    IEnumerator ChangePos(Transform target)
    {
        Player.Instance.DisablePlayer();
        yield return Waiter(.5f);
        Player.Instance.MoveTo(target);
        yield return Waiter(.5f);
        Player.Instance.EnablePlayer();
    }

    IEnumerator Waiter(float seconds)
    {
        //Wait for given seconds
        yield return new WaitForSeconds(seconds);
    }

    /* PUBLIC */

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch(newState)
        {
            case GameState.OnSkinSelection:
                break;
            case GameState.OnLoad:
                break;
            case GameState.OnPause:
                break;
            case GameState.OnConversation:
                break;
            case GameState.OnMenu:
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

        OnGameStateChanged?.Invoke(newState);
    }

    public void SpawnPlayer()
    {
        ReproduceSoundEffect(_spawnSound);
        SwitchScene(RegionSelector.Forest, spawnPoint, null);
    }

    public void TeleportPlayer(Transform target, AudioClip c_sound = null)
    {
        if (State == GameState.OnPlay || State == GameState.OnLoad)
        {
            Debug.Log("Teleport launched towards: " + target.name + " | at: " + target.position);

            UpdateGameState(GameState.OnTeleport); // trigger event
            StartCoroutine(ChangePos(target));
            ReproduceSoundEffect(c_sound);
            UpdateGameState(GameState.OnPlay); // resume game
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

    public void OpenPlayerInventory()
    {
        Player.Instance.OpenInventory();
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
    OnPause,
    OnConversation,
    OnMenu,
    OnPlay,
    OnDefeat,
    OnTeleport
}