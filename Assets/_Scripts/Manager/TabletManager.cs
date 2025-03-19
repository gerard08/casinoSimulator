using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabletManager : MonoBehaviour
{
    public Button[] buttons;

    public GameObject TabletPanel;

    public static bool GameIsPaused = false;

    void Awake()
    {
        GameManager.StateChanged += GameManager_StateChanged;   
    }

    void OnDestroy() //Buena practis quitar el evento del buffer 
    {
        GameManager.StateChanged -= GameManager_StateChanged;
    }

    private void GameManager_StateChanged(GameState state)
    {
        TabletPanel.SetActive(state == GameState.Tablet);
    }

    public void spawnObject1()
    {
        ObjectManager.Instance.SpawnObjects(1);
        GameManager.Instance.UpdateGameState(GameState.Play);

    }

    public void spawnObject2()
    {
        ObjectManager.Instance.SpawnObjects(2);
        GameManager.Instance.UpdateGameState(GameState.Play);

    }
}
