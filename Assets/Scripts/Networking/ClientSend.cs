using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet packet)
    {
        packet.WriteLength();
        Client.instance.tcp.SendData(packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    public static void WelcomeReceived()
    {
        using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            packet.Write(Client.instance.id);
            packet.Write("user");

            SendTCPData(packet);
        }
    }

    public static void RequestPlaceBuilding(Vector2 position, int type)
    {
        using (Packet packet = new Packet((int)ClientPackets.buildingPlacedIn))
        {
            Debug.Log($"Sending {position.x} and {position.y} with type {type}");
            packet.Write(position);
            packet.Write((short)type);

            SendTCPData(packet);
        }
    }

    public static void RequestMoveBuilding(Vector2 oldPosition, Vector2 newPosition, int type)
    {
        using (Packet packet = new Packet((int)ClientPackets.buildingMovedIn))
        {
            Debug.Log($"Sending data {newPosition.x} and {newPosition.y}");
            Debug.Log($"Sending old data {oldPosition.x} and {oldPosition.y}");
            packet.Write(oldPosition);
            packet.Write(newPosition);
            packet.Write((short)type);

            SendTCPData(packet);
        }
    }
}
