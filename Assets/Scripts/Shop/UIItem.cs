using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Resources;
public class UIItem : MonoBehaviour
{
    public PlaceableSO item;
    [SerializeField] Image spriteImage;

    [SerializeField] TMP_Text spriteText;

    [SerializeField] TMP_Text descriptionText;

    [SerializeField] List<TMP_Text> costText;

    [SerializeField] List<Image> costImage;

    public void Update()
    {
        Refresh();
    }

    public void UpdateItem(PlaceableSO item)
    {
        this.item = item;
        if(this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.previewImage;
            spriteText.text = item.title;
            descriptionText.SetText(item.description); 
            for (int i = 0; i < item.cost.Count; i++)
            {
                costText[i].text = item.cost[i].ToString();
                costImage[i].sprite = ImageUtilities.instance.GetSpriteForResource(item.resources[i]);
            }
        }
    }

    public void Refresh()
    {
        if(item == null) return;
        bool isValid = true;
        for (int counter = 0; counter < item.cost.Capacity; counter++)
        {
            if (!FindObjectOfType<ResourcesManager>().CanAfford(item.resources[counter], item.cost[counter]))
            {
                isValid = false;
                spriteImage.color = Color.red;
                costText[counter].color = Color.red;
                GetComponent<Button>().interactable = false;
            }
            else
            {
                costText[counter].color = Color.white;
            }
            if (isValid)
            {
                spriteImage.color = Color.white;
                GetComponent<Button>().interactable = true;
            }
        }
    }

}
