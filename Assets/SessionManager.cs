using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PlayerData
{
    public int playerIndex;
    public string controlScheme;
    public InputDevice[] devices;
    public GameObject prefab;
}
public static class SessionManager 
{
    public static List<PlayerData> players = new List<PlayerData>();

    public static void AddPlayer(PlayerData player)
    {
        foreach (var p in players)
        {
            if (p.playerIndex == player.playerIndex)
            {
                players.Remove(p);
                break;
            }
        }

        Debug.Log(player.prefab.name + " added to players list");
        players.Add(player);
    }
    public static void RemovePlayer(PlayerData player)
    {
        players.Remove(player);
    }
}
