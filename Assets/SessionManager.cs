using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    private static List<PlayerData> players = new List<PlayerData>();
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


    public static List<PlayerData> GetPlayers()
    {
        return players;
    }

    public static void ClearPlayers()
    {
        players.Clear();
    }

    private static int bubbleWins = 0, spikeWins = 0;
    private static int requiredWins = 5;

    public static void SetRequiredWins(int wins)
    {
        Debug.Log("Required wins set to " + wins.ToString());
        requiredWins = wins;
    }
    public static int GetRequiredWins() {
        return requiredWins;
    }
    public static void AddBubbleWin()
    {
        bubbleWins++;
        if(bubbleWins >= requiredWins)
        {
            Debug.Log("Bubble wins");
        }
    }
    public static void AddSpikeWin()
    {
        spikeWins++;
        if(spikeWins >= requiredWins)
        {
            Debug.Log("Spike wins");
        }
    }

    public static void ClearWins()
    {
        bubbleWins = 0;
        spikeWins = 0;
    }   
    public static int GetBubbleWins()
    {
        return bubbleWins;
    }
    public static int GetSpikeWins()
    {
        return spikeWins;
    }
}
