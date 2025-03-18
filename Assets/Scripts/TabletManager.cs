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
}
