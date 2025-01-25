using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[System.Serializable]


public class LobbyManager : MonoBehaviour
{
    [SerializeField] RectTransform[] playerUIElements;
    [SerializeField] Sprite bubbleSprite, spikeSprite;
    PlayerInputManager playerInputManager;
    [SerializeField] GameObject[] prefabs;
    int currentPrefab;


    [SerializeField] Counter winsCounter;
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
    private void DisplayPlayer(PlayerInput playerInput)
    {
        playerUIElements[playerInput.playerIndex].Find("Player Name").GetComponentInChildren<TextMeshProUGUI>().text = "Player " + playerInput.playerIndex.ToString();
        if (playerInput.CompareTag("Bubble"))
            playerUIElements[playerInput.playerIndex].Find("Player Image").GetComponentInChildren<Image>().sprite = bubbleSprite;
        else
            playerUIElements[playerInput.playerIndex].Find("Player Image").GetComponentInChildren<Image>().sprite = spikeSprite;
    }
    private void AddPlayerToSession(PlayerInput playerInput)
    {
        SessionManager.AddPlayer(new PlayerData
        {
            playerIndex = playerInput.playerIndex,
            controlScheme = playerInput.currentControlScheme,
            devices = playerInput.devices.ToArray(),
            prefab = GetComponent<PlayerInputManager>().playerPrefab
        });
    }
    void OnPlayerJoined(PlayerInput playerInput)
    {
        DisplayPlayer(playerInput);
        AddPlayerToSession(playerInput);
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
        playerUIElements[player.playerIndex].Find("Ready Image").GetComponentInChildren<Image>().enabled = val;    
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

}
