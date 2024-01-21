using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerControl : MonoBehaviour
{
    public float movementSpeed;
    [SerializeField] GameObject camera;
    Vector3 worldPos;

    [SerializeField] Tilemap topLayer;
    [SerializeField] Tilemap bottomLayer;


    private (Vector3Int, PlaceableData) currentSelect;

    // Start is called before the first frame update
    void Start()
    {
      //  worldPos = TilemapManager.instance.ConvertCellToWorld(new Vector3Int(0, 0, 0));
        transform.position = worldPos;
        camera.transform.position = worldPos;

    }

    // Update is called once per frame
    void Update()
    {
      //  MoveAndSelect();
    }

    // private void MoveAndSelect()
    // {
    //    // Vector3Int newPos = TilemapManager.instance.ConvertWorldToCell(worldPos);
    //     if (Mathf.Abs(Vector3.Distance(transform.position, worldPos)) <= 0.05f)
    //     {
    //         if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1.0f)
    //         {
    //             newPos.y -= (int)Input.GetAxisRaw("Horizontal");
    //         }
    //         else if (Mathf.Abs(Input.GetAxisRaw("Vertical"))== 1.0f)
    //         {
    //             newPos.x += (int)Input.GetAxisRaw("Vertical");
    //         }
    //         else
    //         {
    //             return;
    //         }
    //     }

    //     //PlaceableData placeable = TilemapManager.instance.GetPlaceableDataInTopLayer(newPos);
    //     if (bottomLayer.GetTile(newPos) != null && topLayer.GetTile(newPos) == null)
    //     {
    //         worldPos = TilemapManager.instance.ConvertCellToWorld(newPos);
    //         transform.position = Vector3.MoveTowards(transform.position, worldPos, movementSpeed * Time.fixedDeltaTime);
    //        // GetComponent<SpriteRenderer>().sortingOrder = newPos.x;
    //         camera.transform.position = Vector3.MoveTowards(camera.transform.position, worldPos, movementSpeed * Time.fixedDeltaTime); ;
    //     //    Deselect();
    //     }
    //     //else if (topLayer.GetTile(newPos) != null)
    //     //{
    //     //    Deselect();
    //     //   // currentSelect = (newPos, placeable);
    //     //    TilemapManager.instance.SelectTile(currentSelect.Item1);
    //     //    SelectionPanelManager.instance.ToggleSelectionPanel(true, currentSelect.Item2.buildingType);
    //     //}
    // }

    // private void Deselect()
    // {
    //     if (currentSelect.Item2 != null && !currentSelect.Item2.Equals(currentSelect))
    //     {
    //         TilemapManager.instance.DeselectTile(currentSelect.Item1);
    //         SelectionPanelManager.instance.ToggleSelectionPanel(false, currentSelect.Item2.buildingType);
    //     }
    // }

    public void FarmRessource()
    {

    }
}
