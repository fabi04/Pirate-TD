using System;
using System.Resources;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    (Vector3Int, Placeable)? currentSelect;
    (Vector3Int, Placeable)? lastSelect;
    bool shopOpen = false;
    bool resourceSelection;

    private TilemapManager tilemapManager;
    private BuildingPlacer buildingPlacer;
    private SelectionPanelManager selectionPanelManager;

    private void Start() {
        tilemapManager = GetComponent<TilemapManager>();
        selectionPanelManager = GetComponent<SelectionPanelManager>();
        buildingPlacer = GetComponent<BuildingPlacer>();
    }

    public void Update() {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tilemapManager.MouseOverTile(worldPos);
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !shopOpen) {
            Vector3Int gridCoords = tilemapManager.ConvertWorldToCell(worldPos);
            if (resourceSelection) {
                HandleResourceSelected(gridCoords);
            } else {
                Deselect();
                if (tilemapManager.GetTileInTopLayer(gridCoords)) {
                    Select(gridCoords);
                } 
            }
        }
    }

    private void HandleResourceSelected(Vector3Int gridCoords) {
        ResourceBuilding resourceBuilding = lastSelect.Value.Item2 as ResourceBuilding;
        resourceBuilding.OnFarmResourceClicked(tilemapManager, GetComponent<ResourcesManager>(), gridCoords);
        Select(lastSelect.Value.Item1);
        resourceSelection = false;
    }
    
    /// <summary>
    /// Selects the tile, if any, at the given position.
    /// </summary>
    /// <param name="position">The position of the tile to select</param>
    public void Select(Vector3Int position) {
        Deselect();
        currentSelect = (position, tilemapManager.GetPlaceableInTopLayer(position));
        selectionPanelManager.ToggleSelectionPanel(true, currentSelect.Value.Item2.type);
        tilemapManager.SelectTileWithRange(position);
    }

    /// <summary>
    /// Deselects the currently selected tile, if any.
    /// </summary>
    public void Deselect() {
        if (currentSelect != null) {
            tilemapManager.DeselectTileWithRange(currentSelect.Value.Item1);
            selectionPanelManager.ToggleSelectionPanel(false, currentSelect.Value.Item2.type);
            currentSelect = null;
        }
    }

    /// <summary>
    /// Called when the user clicked the move button on the current selection.
    /// </summary>
    public void MoveClicked() {
        if (currentSelect != null) {
            buildingPlacer.MoveBuilding(currentSelect.Value.Item1);
        } else {
            throw new InvalidOperationException("Move failed: There is no valid object at the selected position");
        }
    }

    public void FarmResourceClicked() {
        if (currentSelect != null) {
            resourceSelection = true;
            lastSelect = currentSelect;
            Deselect();
        }
    }
}
