using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Characters.FirstPerson; // custom import

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action<GameState> OnGameStateChanged;
    public GameState State;

    public Transform startPoint;

    //private Transform lastCheckpoint;
    //private Transform[] checkPoints;
    //private bool isRunning = false;
    //private bool isFinished = false;

    public GameObject player;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (player == null)
            throw new System.Exception("Player can't be null");
        player.transform.position = startPoint.position;
        player.transform.rotation = startPoint.rotation;
        UpdateGameState(GameState.OnPlay);
    }

    void Update()
    {
        
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch(newState)
        {
            case GameState.SelectSkin:
                break;
            case GameState.OnPause:
                break;
            case GameState.OnConversation:
                break;
            case GameState.OnMenu:
                break;
            case GameState.OnPlay:
                break;
            case GameState.Defeated:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void InvalidMoveThenRollback(Transform moveTo)
    {
        print("Rollback launched");
        if (State == GameState.OnPlay)
        {
            player.transform.position = moveTo.position;
            player.transform.rotation = moveTo.rotation;
        }
    }
}

public enum GameState
{
    SelectSkin,
    OnPause,
    OnConversation,
    OnMenu, // onInventory, not a pause!
    OnPlay, // playing
    Defeated // restart checkpoint
}