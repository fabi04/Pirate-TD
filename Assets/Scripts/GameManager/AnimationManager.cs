using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    [SerializeField] public GameObject shopPanel;


    /// <summary>
    /// Animate the opening and closing of the shop.
    /// </summary>
    public void OnShopClicked()
    {
        FindObjectOfType<ClickManager>().Deselect();
        GameEvents.Instance.ShopTrigger();
        Animator animator = shopPanel.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("shopOpenTrigger");
        }
    }
}
