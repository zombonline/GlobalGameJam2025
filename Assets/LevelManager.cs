using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform[] bubbleSpawns, spikeSpawns;
    PlayerInputManager inputManager;

    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
    }

    private void Start()
    {
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        int bubbleCount = 0;
        int spikeCount = 0;
        Debug.Log(SessionManager.players.Count + "PLAYERS IN SESSION MANAGER");
        foreach (var player in SessionManager.players)
        {
            inputManager.playerPrefab = player.prefab;
            Debug.Log(inputManager.playerPrefab.name + "SET TO INPUT MANAGER");
            var newPlayer =  inputManager.JoinPlayer(player.playerIndex, -1, player.controlScheme, player.devices);
            if (player.prefab.tag == "Bubble")
            {
                newPlayer.transform.position = bubbleSpawns[bubbleCount].position;
                bubbleCount++;
            }
            else
            {
                newPlayer.transform.position = spikeSpawns[spikeCount].position;
                spikeCount++;
            }
        }

    }
}
