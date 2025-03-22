using StarterAssets;
using System.Runtime.Remoting;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator, cellIndicator;
    [SerializeField] private MouseManager mouseManager;
    [SerializeField] private Grid grid;

    bool isPlaying;
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
        isPlaying = state == GameState.Builder;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            Vector3 mousePosition = mouseManager.CheckRayCast();
            
            Debug.Log(mousePosition);
            if(mouseIndicator == null) { mouseIndicator = ObjectManager.Instance.SeacrhObj(); }
            if (mousePosition == null) { mouseIndicator.SetActive(false); }
            else { mouseIndicator.SetActive(true); }
            ObjectMove(mousePosition);
        }
    }
    void ObjectMove(Vector3 mousePosition)
    {
        Vector3 newPosition2 = Vector3.Lerp(mouseIndicator.transform.position, mousePosition, Time.deltaTime * 50);
        mouseIndicator.transform.position = newPosition2;
        Vector3Int gridPosition = grid.WorldToCell(newPosition2);
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }

}
