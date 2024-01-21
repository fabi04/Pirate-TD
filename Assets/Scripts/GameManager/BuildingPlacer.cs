using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using Extensions;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class BuildingPlacer : MonoBehaviour
{
    private PlaceableSO item;

    public GameObject preview;

    private Placeable placeable;
    private TilemapManager tilemapManager;
    private MoveHandler moveHandler;
    bool placing;
    bool isValidPos;

    Vector3Int gridCoords;

    bool moving = false;
       
    void Start()
    {
        preview = Instantiate(preview);
        preview.SetActive(false);
        tilemapManager = GetComponent<TilemapManager>();
        moveHandler = FindObjectOfType<MoveHandler>();
        placing = false;
        isValidPos = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!placing) return;
        isValidPos = tilemapManager.IsTileAvailable(new Vector2Int(gridCoords.x, gridCoords.y)) && tilemapManager.HasPathAsNeighbour(gridCoords);
        placeable = Placeable.CreatePlaceableFromType(item.type, new Vector3Int(0, 0, 0));
        renderBuilding();
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && placing ) {
            if (isValidPos) {
                tilemapManager.AddTile(gridCoords, item.tile, item.type);
                //tilemapManager.DeselectTileWithRange();
                preview.SetActive(false);
                GetComponent<ClickManager>().Select(gridCoords);
                GetComponent<ResourcesManager>().DeductResources(item.resources, item.cost);
                isValidPos = false;
                placing = false;
            }
        }
    }

    public void HandleClick()
    {
        // if (buildingPrefab != null)
        // {
        //     if (moving)
        //     {
        //         if (TilemapManager.instance.IsTileAvailable(new Vector2Int(gridCoords.x, gridCoords.y)))
        //         {
        //             ClientSend.RequestMoveBuilding(building.GetPosition(), new Vector2(gridCoords.x, gridCoords.y),
        //                 (short)buildingPrefab.GetComponent<Placeable>().GetBuildingType()); ;
        //         }
        //     } else
        //     {
        //         if (TilemapManager.instance.IsTileAvailable(new Vector2Int(gridCoords.x, gridCoords.y)))
        //         {
        //             ClientSend.RequestPlaceBuilding(new Vector2(gridCoords.x, gridCoords.y),
        //                 (int)buildingPrefab.GetComponent<Placeable>().GetBuildingType());
        //         }
        //     }
        // }
    }

    // Callback when shop item was clicked
    public void OnShopItemSelected(PlaceableSO item)
    {
        placing = true;
        this.item = item;
        preview.SetActive(true);
        preview.GetComponent<SpriteRenderer>().sprite = item.previewImage;
    }

    // if a building is to be moved on the game board
    public void OnMoveClicked(GameObject building)
    {
        moving = true;
     //   tilemapManager.RemoveTile(building);
      //  buildingPrefab = building;
      //  this.building = buildingPrefab.GetComponent<Building>();
        placing = true;
    }

    public void PlaceBuilding(Vector2Int position, int type, bool isOwn)
    {
        // if (isOwn)
        // {
        // //    buildingPrefab.GetComponent<SpriteRenderer>().sortingOrder = 1;
        //     building.SetPosition((Vector2Int)gridCoords);
        //     building.Select();
        //     building.OnPlaced();
        //    // tilemapManager.AddTile(building);
        //  //   buildingPrefab = null;
        //     building = null;
        //     moving = false;
        // }
        // else
        // {
        //    // LifetimeManager.instance.CreatePlaceable(position, (TileType) type);
        // }
    }

    private void renderBuilding()
    {
        CalculatePosition();
        tilemapManager.SelectRange(gridCoords, placeable.range);
        preview.GetComponent<SpriteRenderer>().sortingOrder = 2;
        preview.gameObject.transform.position = tilemapManager.ConvertCellToWorld(gridCoords);
        ChangeColor(new Color(isValidPos ? 0 : 1, isValidPos ? 1 : 0, 0, 0.75f));
    }

    private void CalculatePosition()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gridCoords = tilemapManager.ConvertWorldToCell(mouseWorldPos);
    }

    private void ChangeColor(Color color)
    {
       preview.GetComponent<SpriteRenderer>().color = color;
    }
}
