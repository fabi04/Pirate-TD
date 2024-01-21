using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class ClickManager : MonoBehaviour
{
    Vector3Int? currentSelect;

    bool resourceSelection;
    bool shopOpen = false;

    private TilemapManager tilemapManager;
    private SelectionPanelManager selectionPanelManager;

    private void Start() {
        tilemapManager = GetComponent<TilemapManager>();
        selectionPanelManager = GetComponent<SelectionPanelManager>();
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

    // public static ClickManager instance;

    // // Start is called before the first frame update
    // void Awake()
    // {
    //     if (instance == null)
    //     {
    //         instance = this;
    //     }
    //     else
    //     {
    //         Destroy(this);
    //     }
    //     placer = GetComponent<BuildingPlacer>();
    //     resourceSelection = false;
    // }

    // void Start()
    // {
    //     GameEvents.Instance.onShopTrigger += ToggleShopMode;
    // }

    // void Destroy()
    // {
    //     GameEvents.Instance.onShopTrigger -= ToggleShopMode;

    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !resourceSelection && !shopOpen)
    //     {
    //         GameObject hit = GetRayCast();
    //        // BuildingPlacer.instance.HandleClick();
    //         if (hit == null && currentSelect == null)
    //         {
    //             // if random click and nothing selected
    //             return;
    //         }
    //         else if(hit != null)
    //         {
    //             if (currentSelect != null)
    //             {
    //                 currentSelect.Deselect();
    //             }
    //             currentSelect = hit.GetComponent<Placeable>();
    //             currentSelect.Select();
    //         }
    //         else if (currentSelect.isSelected)
    //         {
    //             currentSelect.Deselect();
    //         }
    //     }
    //     if(Input.GetKey(KeyCode.Escape))
    //     {
    //         Reset();
    //     }
    // }

    // public void SetCurrentSelection(Placeable placeable)
    // {
    //     currentSelect = placeable;
    // }

    // public GameObject GetRayCast()
    // {
    //     if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
    //     {
    //         Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //         Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
    //         RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
    //         if(hit.collider != null)
    //         {
    //             return hit.collider.gameObject;
    //         }
    //     }
    //     return null;
    // }

    // public GameObject ResourceSelected()
    // {
    //     resourceSelection = true;
    //     GameObject hit =  GetRayCast();
    //     if(hit != null)
    //     {
    //         resourceSelection = false;
    //     }
    //     return hit;
    // }

    // public void Reset()
    // {
    //     //if (resourceSelection)
    //     //{
    //     //    GameEvents.current.TreeSelectionModeActivated();
    //     //}
    //     if(currentSelect != null)
    //     {
    //         currentSelect.Deselect();
    //         currentSelect = null;
    //     }
    //     resourceSelection = false;
    // }

    // public void OnShopClicked()
    // {
    //     Reset();
    //     GameEvents.Instance.ShopTrigger();
    // }

    // private void ToggleShopMode()
    // {
    //     Reset();
    //     shopOpen = !shopOpen;
    // }

    // public void OnSettingsClicked()
    // {
    //     GameEvents.Instance.PauseMenuTrigger();
    // }
}
