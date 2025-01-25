using JetBrains.Annotations;
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
    [SerializeField] RectTransform[] players;
    [SerializeField] Sprite bubbleSprite, spikeSprite;

    [SerializeField] GameObject[] prefabs;
    int currentPrefab;
    void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("Player joined: " + playerInput.playerIndex);
        
        players[playerInput.playerIndex].Find("Player Name").GetComponentInChildren<TextMeshProUGUI>().text = "Player " + playerInput.playerIndex.ToString();
        if(playerInput.CompareTag("Bubble"))
            players[playerInput.playerIndex].Find("Player Image").GetComponentInChildren<Image>().sprite = bubbleSprite;
        else
            players[playerInput.playerIndex].Find("Player Image").GetComponentInChildren<Image>().sprite = spikeSprite;

        SessionManager.AddPlayer(new PlayerData
        {
            playerIndex = playerInput.playerIndex,
            controlScheme = playerInput.currentControlScheme,
            devices = playerInput.devices.ToArray(),
            prefab = GetComponent<PlayerInputManager>().playerPrefab
        });
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

        StartCoroutine(DestroyAfterFrame(player));
    }

    private IEnumerator DestroyAfterFrame(GameObject player)
    {
        yield return new WaitForEndOfFrame();
        Destroy(player);
    }
}
