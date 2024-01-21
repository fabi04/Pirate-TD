using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel, false);
            items.Add(instance.GetComponent<UIItem>());
            instance.GetComponent<Button>().onClick.AddListener(delegate { OnShopItemClicked(instance.transform.GetComponent<UIItem>().item); });
        }

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

    public void UpdateSlot(int slot, PlaceableSO item)
    {
        items[slot].UpdateItem(item);
    }

    public void AddNewItem(PlaceableSO item)
    {
        UpdateSlot(items.FindIndex(i => i.item == null), item);
    }

    public void OnShopItemClicked(PlaceableSO item)
    {
        if (isActive) return;
        isActive = true;
        FindObjectOfType<AnimationManager>().OnShopClicked();
        FindObjectOfType<BuildingPlacer>().OnShopItemSelected(item);
        StartCoroutine(WaitForAnim());
        //GameEvents.Instance.ShopTrigger(); // close Shop again
        //GameEvents.Instance.ShopitemTrigger(item);

    }

    private void UpdateUI()
    {
        foreach (var item in items)
        {
            item.Refresh();
        }
    }
}
