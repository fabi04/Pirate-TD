using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    public static LobbyUIManager instance;
    [SerializeField] public Button button;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Start()
    {
        button.onClick.AddListener(ConnectToServer);
    }

    public void ConnectToServer()
    {
        button.interactable = false;
        button.gameObject.SetActive(false);
        Client.instance.ConnectToServer();
    }
}
