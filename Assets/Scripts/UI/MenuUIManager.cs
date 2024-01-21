using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{

    [SerializeField] private GameObject StartMenuCanvas;
    [SerializeField] private GameObject GameModeSelectionCanvas;


    public void OnPlayButtonClicked()
    {
        StartMenuCanvas.SetActive(false);
        GameModeSelectionCanvas.SetActive(true);
    }

    public void OnBackButtonClicked()
    {
        StartMenuCanvas.SetActive(true);
        GameModeSelectionCanvas.SetActive(false);
    }
    public void OnSinglePLayerButtonCLicked()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void OnSettingsButtonClicked()
    {
        SceneManager.LoadScene("Settings");
    }

}
