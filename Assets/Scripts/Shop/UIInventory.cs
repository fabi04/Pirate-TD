using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the item shop.
/// </summary>
public class UIInventory : MonoBehaviour
{
    public List<UIItem> items = new List<UIItem>();
    public List<PlaceableSO> shopItems = new List<PlaceableSO>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfSlots = 2;

    bool isActive = false;

    private void Start()
    {
        InitialiseSlots();
    }

    private void InitialiseSlots() {
        // first initialise the slots...
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel, false);
            items.Add(instance.GetComponent<UIItem>());
            instance.GetComponent<Button>().onClick.AddListener(delegate { OnShopItemClicked(instance.transform.GetComponent<UIItem>().item); });
        }
        // ...then add the items.
        for (int i = 0; i < shopItems.Count; i++)
        {
            AddNewItem(shopItems[i]);
        }
    }

    IEnumerator WaitForAnim()
    {
        yield return new WaitForSeconds(1f);
        isActive = false;
    }

    /// <summary>
    /// Update the given slot to contain the given item.
    /// </summary>
    /// <param name="slot">The item to insert</param>
    /// <param name="item">The slot to insert the item into</param>
    public void UpdateSlot(int slot, PlaceableSO item)
    {
        items[slot].InitialiseItem(item);
    }

    /// <summary>
    /// Add a new item, if there is a free slot.
    /// </summary>
    /// <param name="item">The item to add</param>
    public void AddNewItem(PlaceableSO item)
    {
        UpdateSlot(items.FindIndex(i => i.item == null), item);
    }

    /// <summary>
    /// Callback when an item in the shop is clicked.
    /// </summary>
    /// <param name="item">The item that was clicked.</param>
    public void OnShopItemClicked(PlaceableSO item)
    {
        if (isActive) return;
        isActive = true;
        FindObjectOfType<AnimationManager>().OnShopClicked();
        FindObjectOfType<BuildingPlacer>().OnShopItemSelected(item);
        StartCoroutine(WaitForAnim());
    }
}
