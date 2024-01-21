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
    public void ToggleSelectionPanel(bool state, TileType type)
    {
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
        //placeable.ProcessPanel(curr);
        AnimateBuildingSelect(state);
    }

    public void AnimateBuildingSelect(bool state)
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
