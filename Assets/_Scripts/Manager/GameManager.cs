using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState State {  get; private set; }   

    //public static GameManager instance;

    //Ideas hacer 2 eventos uno antes de cambiar el state y otro despues de cambiar el state
    public static event Action<GameState> StateChanged;

    void Start() => UpdateGameState(GameState.Play); //Solo al iniciar se actualiza el state


    // Update is called once per frame
    public void UpdateGameState(GameState newState)
    {   
        if(State == newState)  return;

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

[Serializable]
public enum GameState
{
    Menu,
    Play,
    Tablet,
    Exit
}