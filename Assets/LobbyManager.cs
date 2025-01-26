using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[System.Serializable]


public class LobbyManager : MonoBehaviour
{
    [SerializeField] LobbyPlayerUI[] playerUIElements;
    PlayerInputManager playerInputManager;
    [SerializeField] GameObject[] prefabs;
    int currentPrefab;


    [SerializeField] Counter winsCounter;

    [SerializeField] Button startButton;
    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        winsCounter.onCountChange += SetRequiredWins;
        winsCounter.UpdateCounter(SessionManager.GetRequiredWins());
        JoinExistingPlayers();
        SessionManager.ClearWins();
    }

    private void JoinExistingPlayers()
    {
        PlayerData[] previousSessionPlayers = new PlayerData[SessionManager.GetPlayers().Count];
        Array.Copy(SessionManager.GetPlayers().ToArray(), previousSessionPlayers, SessionManager.GetPlayers().Count);
        SessionManager.ClearPlayers();
        foreach (var player in previousSessionPlayers)
        {
            playerInputManager.playerPrefab = player.prefab;
            playerInputManager.JoinPlayer(player.playerIndex, -1, player.controlScheme, player.devices);
        }
    }
    private void AddPlayerToSession(PlayerInput playerInput)
    {
        var newPlayer = new PlayerData
        {
            playerIndex = playerInput.playerIndex,
            controlScheme = playerInput.currentControlScheme,
            devices = playerInput.devices.ToArray(),
            prefab = GetComponent<PlayerInputManager>().playerPrefab,
            team = playerInput.GetComponent<PlayerInput>().tag == "Bubble" ? Team.Bubble : Team.Spike
        };
        SessionManager.AddPlayer(newPlayer);
        playerUIElements[playerInput.playerIndex].SetPlayer(newPlayer);
    }
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        AddPlayerToSession(playerInput);
        CheckCanStart();
    }
    public void SwapPlayer(GameObject player)
    {
        currentPrefab = (currentPrefab + 1) % prefabs.Length;
        var inputManager = GetComponent<PlayerInputManager>();
        inputManager.playerPrefab = prefabs[currentPrefab];

        var playerInput = player.GetComponent<PlayerInput>();
        int playerIndex = playerInput.playerIndex;
        var devices = playerInput.devices.ToArray();
        var controlScheme = playerInput.currentControlScheme;

        playerInput.gameObject.SetActive(false);

        // Join new player first, then disable and destroy old player with a delay
        inputManager.JoinPlayer(playerIndex, -1, controlScheme, devices);

        Destroy(player);
    }
    public void TogglePlayerReady(PlayerInput player, bool val)
    {
        playerUIElements[player.playerIndex].SetReadyDisplay(val);
        CheckCanStart();
    }
    public bool CheckAllPlayersReady()
    {
        foreach(LobbyPlayer player in FindObjectsByType<LobbyPlayer>(FindObjectsSortMode.None))
        {
            if (!player.isReady)
            {
                return false;
            }
        }
        return true;
    }
    public void SetRequiredWins(int wins)
    {
        SessionManager.SetRequiredWins(wins);
    }
    public void CheckCanStart()
    {
        startButton.interactable = CheckAllPlayersReady() && SessionManager.GetPlayers().Count >= 2;
        try
        {
            foreach (var button in FindObjectsByType<Button>(FindObjectsSortMode.None))
            {
                if (button != startButton)
                {
                    button.Select();
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
