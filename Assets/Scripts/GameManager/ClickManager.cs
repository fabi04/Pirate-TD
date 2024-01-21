using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    Vector3Int? currentSelect;
    bool shopOpen = false;

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
            if (tilemapManager.GetTileInTopLayer(gridCoords)) {
                Deselect();
                Select(gridCoords);
            } else {
                Deselect();
            }
        }
    }
    public void Select(Vector3Int position) {
        currentSelect = position;
        selectionPanelManager.ToggleSelectionPanel(true, tilemapManager.GetTileType(position));
        tilemapManager.SelectTileWithRange(position);
    }

    public void Deselect() {
        if (currentSelect != null) {
            tilemapManager.DeselectTileWithRange(currentSelect.Value);
            selectionPanelManager.ToggleSelectionPanel(false, tilemapManager.GetTileType(currentSelect.Value));
            currentSelect = null;
        }
    }

    /// <summary>
    /// Called when the user clicked the move button on the current selection.
    /// </summary>
    public void MoveClicked() {
        Debug.Log("Moved clicked");
        if (currentSelect != null) {
            buildingPlacer.MoveBuilding(currentSelect.Value);
        }
    }
}
