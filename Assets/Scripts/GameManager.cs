using UnityEngine;

public class GameManager : MonoBehaviour
{
     enum states
    {
        MENU,
        PLAY,
        EXIT
    }

    states state;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = states.MENU;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case states.MENU:
                enabled = true; break;
            case states.EXIT: 
                enabled = false; break; 
            case states.PLAY:
                enabled = true; break;
            default: break;
        }
    }
}
