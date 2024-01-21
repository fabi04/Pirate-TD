using UnityEngine;



using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Player currentPlayer;
    public Dictionary<int, Player> players = new Dictionary<int, Player>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void SpawnPlayer(Player player)
    {
        if (player != null)
        {
            if (player.id == Client.instance.id)
            {
                currentPlayer = player;
            }
            players.Add(player.id, player);
        }
    }

    public Player GetPlayer(int id)
    {
        Player playerOut;
        players.TryGetValue(id, out playerOut);
        return playerOut;
    }
}

