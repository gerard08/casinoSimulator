using NUnit.Framework;
using StarterAssets;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject gridVisualization, obj;
    [SerializeField] private MouseManager mouseManager;
    [SerializeField] private Grid grid;

    [SerializeField]
    private GridData floorData, furnitureData;
    
    bool isPlaying;

    [SerializeField]
    private PreviewSystem preview;
    [SerializeField]
    private ObjectPlacer objectPlacer;

    [SerializeField]
    private IBuildingState buildingState;

    private GameState gameState;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;

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
        gameState = state;
        isPlaying = state == GameState.Builder || state == GameState.Remove;
        if (state == GameState.Tablet) StopPlacements();
        if (state == GameState.Remove) StartRemoving();
    }

    private void Start()
    {
        StopPlacements();
        floorData = new();
        furnitureData = new();
    }

    public void StartPlacement(int ID)
    {
        StopPlacements();
        gridVisualization.SetActive(true);
        obj = ObjectManager.Instance.GetObj();
        buildingState = new PlacementState(ID,
                                           grid,
                                           preview,
                                           floorData,
                                           furnitureData,
                                           objectPlacer,
                                           obj);
        mouseManager.OnClicked += PlaceStructure;
        mouseManager.OnExit += StopPlacements;

    }
    public void StartRemoving()
    {
        StopPlacements();
        gridVisualization.SetActive(true);
        obj = ObjectManager.Instance.GetObj();
        buildingState = new RemovingState(grid,
                                          preview,
                                          floorData,
                                          furnitureData,
                                          objectPlacer,
                                          obj);
        mouseManager.OnClicked += PlaceStructure;
        mouseManager.OnExit += StopPlacements;

    }
    private void PlaceStructure()
    {
        Debug.Log("Placement");
        Vector3 mousePoisiton = lastDetectedPosition;
        Vector3Int gridPosition = grid.WorldToCell(mousePoisiton);

        buildingState.OnAction(gridPosition);
        if(gameState == GameState.Builder)
        {
            ObjectManager.Instance.SetObjType();
            GameManager.Instance.UpdateGameState(GameState.Play);
            gridVisualization.SetActive(false);
        }
    }
    private void StopPlacements()
    {
        if (buildingState == null)
            return;
        gridVisualization.SetActive(false);
        buildingState.EndState();
        obj = null;
        mouseManager.OnClicked -= PlaceStructure;
        mouseManager.OnExit -= StopPlacements;
        lastDetectedPosition = Vector3Int.zero;
        buildingState = null;
    }

    void Update()
    {
        if (isPlaying)
        {
            if (buildingState == null)
                return;
            Vector3 mousePosition = mouseManager.CheckRayCast();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);

            if (lastDetectedPosition != gridPosition)
            {
                buildingState.UpdateState(gridPosition);
                lastDetectedPosition = gridPosition;
            }
           
        }
    }


}
