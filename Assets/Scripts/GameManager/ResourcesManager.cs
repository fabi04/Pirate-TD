using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] float interPolationScale = 0.025f;
    [SerializeField] TMP_Text woodText;
    [SerializeField] TMP_Text stoneText;
    [SerializeField] TMP_Text populationText;
    [SerializeField] Slider populationSlider;
    [SerializeField] Slider stoneSlider;
    [SerializeField] Slider woodSlider;

    public int woodCapacity;
    public int stoneCapacity;
    public int populationCapacity;

    public int wood;
    public int stone;
    public int population;

    private Dictionary<Resources, int> resources;

    private void Start() {
        resources = new Dictionary<Resources, int>();
        resources.Add(Resources.STONE, stone);
        resources.Add(Resources.WOOD, wood);
        resources.Add(Resources.POPULATION, population);
    }

    public bool CanAfford(Resources resource, int amount)
    {
        if (resources == null) return false;
        if (resources.TryGetValue(resource, out int value)) {
            if (value < amount)
            {
                return false;
            }
        }
        return true;
    }

    public void DeductResources(List<Resources> resources, List<int> cost) {
        if (resources.Count != cost.Count) return;
        Dictionary<Resources, int> combined = new Dictionary<Resources, int>();
        for (int i = 0; i < resources.Count; i++) {
            combined.Add(resources[i], cost[i]);
        }
        DeductResources(combined);
    }

    public void DeductResources(Dictionary<Resources, int> amount) {
        foreach (KeyValuePair<Resources, int> resource in amount) {
            if (CanAfford(resource.Key, resource.Value)) {
                resources[resource.Key] -= resource.Value;
            }
        }
    }

    public void SetResources(Dictionary<Resources, int> resources)
    {
        this.resources = resources;
    }

    private void Update()
    {
        if (resources == null) return;
        woodText.text = resources[Resources.WOOD].ToString() + " / " + woodCapacity.ToString();
        stoneText.text = resources[Resources.STONE] + " / " + stoneCapacity.ToString();
        populationText.text = resources[Resources.POPULATION] + " / " + populationCapacity.ToString();
        woodSlider.value = Mathf.Lerp((float)woodSlider.value, (float)resources[Resources.WOOD] / woodCapacity, interPolationScale);
        stoneSlider.value = Mathf.Lerp((float)stoneSlider.value, (float)resources[Resources.STONE] / stoneCapacity, interPolationScale);
        populationSlider.value = Mathf.Lerp((float)populationSlider.value, (float)resources[Resources.POPULATION] / populationCapacity, interPolationScale);
    }
}
