using StarterAssets;
using System;
using System.Runtime.Remoting;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator, cellIndicator, gridVisualization;
    [SerializeField] private MouseManager mouseManager;
    [SerializeField] private Grid grid;
    private int selectedObjectIndex = -1;
    private Vector3 LastPosition;
    
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
        if (state == GameState.Tablet) StopPlacements();
    }

    private void Start()
    {
        StopPlacements();
    }

    public void StartPlacement(int ID)
    {
        StopPlacements();
        var objIndex = ResourceSystem.Instance.GetObject(ID);
        selectedObjectIndex = objIndex.BaseStats.ID;
        gridVisualization.SetActive(true);
        cellIndicator.SetActive(true);
        mouseManager.OnClicked += PlaceStructure;
        Debug.Log("Start");
        mouseManager.OnExit += StopPlacements;

    }

    private void PlaceStructure()
    {
        Debug.Log("Placement");
        Vector3 mousePoisiton = cellIndicator.transform.position;
        Vector3Int gridPosition = grid.WorldToCell(mousePoisiton);
        mouseIndicator.transform.position = gridPosition;
        mouseIndicator = null;
        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.Play);
    }

    private void StopPlacements()
    {
        Debug.Log("Stop");
        selectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        if(mouseIndicator != null)
        DestroyImmediate(mouseIndicator);
        mouseManager.OnClicked -= PlaceStructure;
        mouseManager.OnExit -= StopPlacements;
    }

    void Update()
    {
        if (isPlaying)
        {
            if (selectedObjectIndex < 0)
                return;
            Vector3 mousePosition = mouseManager.CheckRayCast();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);
            LastPosition = mousePosition;
            if (Input.GetKey(KeyCode.LeftControl)) 
            { 
                mouseIndicator.transform.position = gridPosition;
                cellIndicator.transform.position = gridPosition;
                return; 
            }
            Debug.Log("!");
            mouseIndicator.transform.position = mousePosition;
            cellIndicator.transform.position = grid.CellToWorld(gridPosition);
            //ObjectMove(mousePosition);
        }
    }
    void ObjectMove(Vector3 mousePosition)
    {
        Vector3 newPosition2 = Vector3.Lerp(mouseIndicator.transform.position, mousePosition, Time.deltaTime * 50);
        Vector3Int gridPosition = grid.WorldToCell(newPosition2);
        mouseIndicator.transform.position = newPosition2;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }

}
