using System;
using UnityEngine;

public enum GameState
{
    Menu,
    Play,
    Tablet,
    Exit
}

public class GameManager : MonoBehaviour
{
    GameState State;

    public static GameManager instance;

    public static event Action<GameState> StateChanged; 

    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateGameState(GameState.Play);
    }

    // Update is called once per frame
    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState) {
            case GameState.Menu:
                break;
            case GameState.Play:
                Debug.Log("State Play");
                break;
            case GameState.Tablet:
                Debug.Log("State Tablet");
                break;
            case GameState.Exit:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
       }

        StateChanged?.Invoke(newState);

    }
}
