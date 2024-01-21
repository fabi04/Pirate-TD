using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // public static UIManager instance;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] ParticleSystem treeParticleSystem;
    [SerializeField] ParticleSystem smokeParticleSystem;
   // public GameObject gamePanel;
    private GameObject selectionPanel;

    private void Start()
    {
        print("uiManager initialzed");
        // instance = this;
        GameEvents.Instance.onPauseTrigger += AnimatePauseMenu;
        GameEvents.Instance.onPlaceableDestruction += AnimatePlaceableDestruction;
        GameEvents.Instance.onPlaceableSmoke += AnimatePlaceableSmoke;
        GameEvents.Instance.onTreeSelectAction += AnimateResourceSelectionMode;
        GameEvents.Instance.onStoneSelectAction += AnimateResourceSelectionMode;
        GameEvents.Instance.onShopTrigger += AnimateShop;

    }

    private void Destroy()
    {
        GameEvents.Instance.onPauseTrigger -= AnimatePauseMenu;
        GameEvents.Instance.onPlaceableDestruction -= AnimatePlaceableDestruction;
        GameEvents.Instance.onPlaceableSmoke -= AnimatePlaceableSmoke;
        GameEvents.Instance.onTreeSelectAction -= AnimateResourceSelectionMode;
        GameEvents.Instance.onStoneSelectAction -= AnimateResourceSelectionMode;
        GameEvents.Instance.onShopTrigger -= AnimateShop;
    }

    public void OnShopClicked()
    {
        GameEvents.Instance.ShopTrigger();
    }

    public void AnimateShop()
    {
        if(panel != null)
        {
            Animator animator = panel.GetComponent<Animator>();
            if(animator != null)
            {
                //bool isOpen = animator.GetBool("open");
                animator.SetTrigger("Trigger");
            }
        }
    }

    public void AnimatePauseMenu()
    {
        if (pauseMenu.activeSelf)
        {
            StartCoroutine(DisableSettingsMenu());

        }
        else
        {
            pauseMenu.SetActive(true);
        }
        pauseMenu.GetComponent<Animator>().SetTrigger("TriggerPauseMenu");
    }

    IEnumerator DisableSettingsMenu()
    {
        yield return new WaitForSeconds(1.5f);
        pauseMenu.SetActive(false);
    }

    public void AnimatePlaceableDestruction(GameObject placeable)
    {
        treeParticleSystem.transform.position = placeable.transform.position;
        treeParticleSystem.Play();
    }

    public void AnimatePlaceableSmoke(GameObject placeable)
    {
        Vector2 smokePos = placeable.transform.position;
        smokePos.y -= 0.85f;
        smokeParticleSystem.transform.position = smokePos;
        smokeParticleSystem.Play();
    }

    

    public void AnimateResourceSelectionMode()
    {
        if (panel != null)
        {
            Animator animator = panel.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("SelectTrigger");
            }
        }
    }
}
