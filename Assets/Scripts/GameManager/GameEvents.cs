using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    public event Action onShopTrigger;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
    }
    
    public void ShopTrigger()
    {
        if(onShopTrigger != null)
        {
            onShopTrigger.Invoke();
        }
    }

    public event Action onPauseTrigger;

    public void PauseMenuTrigger()
    {
        if (onPauseTrigger != null)
        {
            onPauseTrigger.Invoke();
        }
    }

    public delegate void ShopItemAction(PlaceableSO item);
    public event ShopItemAction onShopitemTrigger;

    public void ShopitemTrigger(PlaceableSO item)
    {
        if(onShopitemTrigger != null)
        {
            onShopitemTrigger.Invoke(item);
        }
    }

    //public delegate GameObject ResoucesSelectAction();
    //public event ResoucesSelectAction onResourceSelected;
    //public GameObject ResourceSelected()  
    //{
    //    if(onResourceSelected != null)
    //    {
    //        return onResourceSelected.Invoke();
    //    }
    //    return null;
    //}

    // called when an Object is being created from Scritable Object
    public delegate GameObject ObjectCreationAction(PlaceableSO item);
    public event ObjectCreationAction onObjectTrigger;
    public GameObject ObjectTrigger(PlaceableSO item)
    {
        if(onObjectTrigger != null)
        {
            return onObjectTrigger.Invoke(item);
        }
        return null;
    }


    public delegate void PlaceableDestructionAction(GameObject placeable);
    public event PlaceableDestructionAction onPlaceableDestruction;
    public void PlaceableDestructed(GameObject placeable)
    {
        if (onPlaceableDestruction != null)
        {
            onPlaceableDestruction.Invoke(placeable);
        }
    }


    public delegate void PlaceableSmokeAction(GameObject placeable);
    public event PlaceableSmokeAction onPlaceableSmoke;
    public void PlaceableSmoked(GameObject placeable)
    {
        if (onPlaceableSmoke != null)
        {
            onPlaceableSmoke.Invoke(placeable);
        }
    }
    
    // when tree selection mode is activated 
    public event Action onTreeSelectAction;
    public void TreeSelectionModeActivated()
    {
        if(onTreeSelectAction != null)
        {
            onTreeSelectAction.Invoke();
        }
    }
public event Action onStoneSelectAction;
    public void StoneSelectionModeActivated()
    {
        if (onStoneSelectAction != null)
        {
            onStoneSelectAction.Invoke();
        }
    }

}
