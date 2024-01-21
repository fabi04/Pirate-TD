using UnityEngine;

/// <summary>
/// Models a simple player.
/// </summary>
public class Player
{
    public int id { get; private set; }
    public string username { get; private set; }

    public Vector2 position { get; private set; }

    public Player(int id, string username, Vector2 position)
    {
        this.id = id;
        this.username = username;
        this.position = position;
    }
}
