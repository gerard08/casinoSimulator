using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabletManager : MonoBehaviour
{
    public Button[] buttons;

    public GameObject TabletPanel;

    public static bool GameIsPaused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {

            //Tablet();
       
    }
    // Update is called once per frame
    public void Tablet()
    {
        TabletPanel.SetActive(GameIsPaused = GameIsPaused ? false : true);
    }


}
