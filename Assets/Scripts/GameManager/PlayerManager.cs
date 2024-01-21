using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages the players spawned in the game.
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private Player currentPlayer;
    private Dictionary<int, Player> players = new Dictionary<int, Player>();

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

    /// <summary>
    /// Spawn a new player into the game.
    /// </summary>
    /// <param name="player">The player containings the player's data</param>
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

    /// <summary>
    /// Get the player corresponding to the id.
    /// </summary>
    public Player GetPlayer(int id)
    {
        Player playerOut;
        players.TryGetValue(id, out playerOut);
        return playerOut;
    }
}

