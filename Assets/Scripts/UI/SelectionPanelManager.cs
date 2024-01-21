using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectionPanelManager : MonoBehaviour
{
    [SerializeField] GameObject housePanel;
    [SerializeField] GameObject stonePanel;
    [SerializeField] GameObject lumberPanel;
    [SerializeField] GameObject woodPanel;

    [SerializeField] GameObject townhallPanel;

    private GameObject curr;

    /// <summary>
    /// Sets the state of the current selection panel. 
    /// </summary>
    /// <param name="state">The state (on or off)</param>
    /// <param name="type">The corresponding tile's type</param>
    public void ToggleSelectionPanel(bool state, TileType type)
    {
        Debug.Log(type);
        switch(type)
        {
            case TileType.TREE:
            {
                curr = woodPanel;
                break;
            }
            case TileType.STONE:
            {
                curr = stonePanel;
                break;
            }
            case TileType.LUMBER_HOUSE:
            {
                curr = lumberPanel;
                break;
            }
            //case House:
            //{
            //    curr = housePanel;
            //    break;
            //}
            //case Lumber:
            //{
            //    curr = lumberPanel;
            //    break;
            //}
            //case StoneHouse:
            //{
            //    curr = stonePanel;
            //    break;
            //}
            //case Townhall:
            //{
            //    curr = townhallPanel;
            //    break;
            //}
            default:
            {
                return;
            }
        }
        AnimateBuildingSelect(state);
    }

    private void AnimateBuildingSelect(bool state)
    {
        if(curr != null)
        {
            Animator animator = curr.GetComponent<Animator>();
            if(animator != null)
            {
                animator.SetBool("selection", state);
            }
        }
    }
}
