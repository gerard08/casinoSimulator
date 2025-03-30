using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabletManager : MonoBehaviour
{
    public Button[] buttons;

    public GameObject TabletPanel;

    public static bool GameIsPaused = false;

    public int id;

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

    public void spawnObject(int ID)
    {
        ObjectManager.Instance.SpawnObjects(ID);
        if (ID < 0)
        {
            Debug.Log($"No ID found {ID}");
            return;
        }
        TabletPanel.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.Builder);
    }

}
