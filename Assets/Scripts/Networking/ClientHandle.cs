using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet packet)
    {
        string msg = packet.ReadString();
        int id = packet.ReadInt();
        Client.instance.id = id;
        ClientSend.WelcomeReceived();
        Debug.Log($"Message recieved from server: {msg}");
    }

    public static void PlayerConnect(Packet packet)
    {
        Debug.Log("here");
        int id = packet.ReadInt();
        string username = packet.ReadString();
        int count = packet.ReadInt();
        Dictionary<Resources, int> resources = new Dictionary<Resources, int>();
        for (int i = 0; i < count; i++)
        {
            resources.Add((Resources)packet.ReadShort(), packet.ReadInt());
        }
        Vector2 position = packet.ReadVector2Int();
        float seedX = packet.ReadFloat();
        float seedY = packet.ReadFloat();
        float scale = packet.ReadFloat();

        Player player = new Player(id, username, position);
        //ResourcesGenerator.instance.SetParameters(seedX, seedY, scale, position);
       // ResourcesManager.instance.SetResources(resources);
        Debug.Log("Player connected successfully!");

        PlayerManager.instance.SpawnPlayer(player);
    }

    public static void BuildingPlaced(Packet packet)
    {
        Vector2Int position = packet.ReadVector2Int();
        int type = packet.ReadShort();
       // int hitpoints = packet.ReadInt();
        int fromClient = packet.ReadInt();
        Debug.Log($"Message received. Placing at {position.x} and {position.y}");
     //  BuildingPlacer.instance.PlaceBuilding(position, type, Client.instance.id == fromClient);
    }

    public static void RessourceUpdate(Packet packet)
    {
        int count = packet.ReadInt();
        Dictionary<Resources, int> resources = new Dictionary<Resources, int>();
        for (int i = 0; i < count; i++)
        {
            Resources resource = (Resources) packet.ReadShort();
            int value = packet.ReadInt();
            resources.Add(resource, value);
        }
       // ResourcesManager.instance.SetResources(resources);
    }
}
