using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SelectionPanelManager : MonoBehaviour
{
    [SerializeField] GameObject housePanel;
    [SerializeField] GameObject stonePanel;
    [SerializeField] GameObject resourceBuildingPanel;
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
            case TileType.STONE_HOUSE:
            case TileType.LUMBER_HOUSE:
            {
                curr = resourceBuildingPanel;
                resourceBuildingPanel.GetComponentInChildren<TMP_Text>().text = type.ToString();
                break;
            }
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
            curr.SetActive(true);
            Animator animator = curr.GetComponent<Animator>();
            if(animator != null)
            {
                animator.SetBool("selection", state);
            }
        }
    }
}
