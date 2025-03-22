using StarterAssets;
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
            
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);
            Debug.Log(mousePosition);
            if(mouseIndicator == null) { mouseIndicator = ObjectManager.Instance.SeacrhObj(); }
            if (mousePosition == null) { mouseIndicator.SetActive(false); }
            else { mouseIndicator.SetActive(true); }
                mouseIndicator.transform.position = mousePosition;
            cellIndicator.transform.position = grid.CellToWorld(gridPosition);
        }
    }

}
