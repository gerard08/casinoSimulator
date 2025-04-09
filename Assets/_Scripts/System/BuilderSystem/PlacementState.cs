using UnityEngine;

public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = -1;
    int ID;
    Grid grid;
    PreviewSystem previewSystem;
    GridData floorData;
    GridData furnitureData;
    ObjectPlacer objectPlacer;
    GameObject obj;

    public PlacementState(int iD,
                          Grid grid,
                          PreviewSystem previewSystem,
                          GridData floorData,
                          GridData furnitureData,
                          ObjectPlacer objectPlacer,
                          GameObject _obj)
    {
        ID = iD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.floorData = floorData;
        this.furnitureData = furnitureData;
        this.objectPlacer = objectPlacer;
        this.obj = _obj;

        selectedObjectIndex = ResourceSystem.Instance.GetObject(ID).BaseStats.ID;
        if (selectedObjectIndex > -1)
        {
            previewSystem.StartShowingPlacementPreview(
                obj,
                ResourceSystem.Instance.GetObject(ID).BaseStats.Size);

            obj.SetActive(false);
        }
        else
            throw new System.Exception($"No object with ID {iD}");

    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false) return;

        obj.transform.position = gridPosition;
        obj.SetActive(true);
        obj = null;

        previewSystem.StopShowingPreview();

        int index = objectPlacer.PlaceObject(ResourceSystem.Instance.GetObject(selectedObjectIndex).BaseStats.Prefab, grid.CellToWorld(gridPosition));

        GridData selectedDATA = ResourceSystem.Instance.GetObject(selectedObjectIndex).BaseStats.ID == 0 ?
            furnitureData :
            floorData;
        selectedDATA.AddObjectAt(gridPosition,
            ResourceSystem.Instance.GetObject(selectedObjectIndex).BaseStats.Size,
            ResourceSystem.Instance.GetObject(selectedObjectIndex).BaseStats.ID,
            index);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {

        GridData selectedDATA = ResourceSystem.Instance.GetObject(selectedObjectIndex).BaseStats.ID == 0 ?
            furnitureData :
            floorData;

        return selectedDATA.CanPlaceObjectAt(gridPosition, ResourceSystem.Instance.GetObject(selectedObjectIndex).BaseStats.Size);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
    }

}
