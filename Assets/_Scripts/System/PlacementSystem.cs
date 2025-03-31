using NUnit.Framework;
using StarterAssets;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator, gridVisualization;
    [SerializeField] private MouseManager mouseManager;
    [SerializeField] private Grid grid;
    private int selectedObjectIndex = -1;

    [SerializeField]
    private GridData floorData, furnitureData;

    bool isPlaying;

    private List<GameObject> placedGameObject = new();

    [SerializeField]
    private PreviewSystem preview;

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
        isPlaying = state == GameState.Builder;
        if (state == GameState.Tablet) StopPlacements();
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
        selectedObjectIndex = ResourceSystem.Instance.GetObject(ID).BaseStats.ID;
        mouseIndicator = ObjectManager.Instance.GetObj();
        gridVisualization.SetActive(true);
        preview.StartShowingPlacementPreview(
            mouseIndicator,
            ResourceSystem.Instance.GetObject(ID).BaseStats.Size);
        ObjectManager.Instance.SetVisibilityObj(false);
        mouseManager.OnClicked += PlaceStructure;
        Debug.Log("Start");
        mouseManager.OnExit += StopPlacements;

    }

    private void PlaceStructure()
    {
        Debug.Log("Placement");
        Vector3 mousePoisiton = lastDetectedPosition;
        Vector3Int gridPosition = grid.WorldToCell(mousePoisiton);

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false) return;

        mouseIndicator.transform.position = gridPosition;
        mouseIndicator = null;
        gridVisualization.SetActive(false);
        placedGameObject.Add(ResourceSystem.Instance.GetObject(selectedObjectIndex).BaseStats.Prefab);
        GridData selectedDATA = ResourceSystem.Instance.GetObject(selectedObjectIndex).BaseStats.ID == 0 ?
            floorData :
            furnitureData;
        selectedDATA.AddObjectAt(gridPosition,
            ResourceSystem.Instance.GetObject(selectedObjectIndex).BaseStats.Size,
            ResourceSystem.Instance.GetObject(selectedObjectIndex).BaseStats.ID,
            placedGameObject.Count - 1);
        preview.UpdatePosition(grid.CellToWorld(gridPosition), false);
        ObjectManager.Instance.SetVisibilityObj(true);
        preview.StopShowingPreview();
        GameManager.Instance.UpdateGameState(GameState.Play);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {

        GridData selectedDATA = ResourceSystem.Instance.GetObject(selectedObjectIndex).BaseStats.ID == 0 ? 
            floorData : 
            furnitureData;

        return selectedDATA.CanPlaceObjectAt(gridPosition, ResourceSystem.Instance.GetObject(selectedObjectIndex).BaseStats.Size);
    }

    private void StopPlacements()
    {
        Debug.Log("Stop");
        selectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        preview.StopShowingPreview();
        if(mouseIndicator != null)
        DestroyImmediate(mouseIndicator);
        mouseManager.OnClicked -= PlaceStructure;
        mouseManager.OnExit -= StopPlacements;
        lastDetectedPosition = Vector3Int.zero;
    }

    void Update()
    {
        if (isPlaying)
        {
            if (selectedObjectIndex < 0)
                return;
            Vector3 mousePosition = mouseManager.CheckRayCast();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);
            if(lastDetectedPosition != gridPosition)
            {
                bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
                preview.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);

                /*if (Input.GetKey(KeyCode.LeftControl))
                {
                    mouseIndicator.transform.position = gridPosition;
                    preview.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
                    return;
                }*/
                mouseIndicator.transform.position = mousePosition;
                lastDetectedPosition = gridPosition;
            }
            //ObjectMove(mousePosition);
        }
    }
    void ObjectMove(Vector3 mousePosition)
    {
        Vector3 newPosition2 = Vector3.Lerp(mouseIndicator.transform.position, mousePosition, Time.deltaTime * 50);
        Vector3Int gridPosition = grid.WorldToCell(newPosition2);
        mouseIndicator.transform.position = newPosition2;
        //cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }

}
