using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyPlayer : MonoBehaviour
{
    [SerializeField] public PlayerInput playerInput;

    private InputAction teamSwap, ready;

    public bool isReady = false;

    private void Awake()
    {
        if(FindObjectOfType<LobbyManager>() == null)
        {
            return;
        }
        GetComponentInChildren<Collider2D>().enabled = false;
    }
    private void OnEnable()
    {
        teamSwap = playerInput.actions["TeamSwap"];
        teamSwap.Enable();
        ready = playerInput.actions["Ready"];
        ready.Enable();
    }
    private void OnDisable()
    {
        teamSwap.Disable();
        ready.Disable();
    }
    private void Update()
    {
        if (teamSwap.triggered)
        {
            Swap();
        }
        if (ready.triggered)
        {
            ToggleReady();
        }
    }
    private void Swap()
    {
        if(FindObjectOfType<LobbyManager>() == null)
        {
            return;
        }
        FindObjectOfType<LobbyManager>().SwapPlayer(this.gameObject);
    }

    private void ToggleReady()
    {
        if (FindObjectOfType<LobbyManager>() == null)
        {
            return;
        }
        isReady = !isReady;
        FindFirstObjectByType<LobbyManager>().TogglePlayerReady(this.GetComponent<PlayerInput>(), isReady);
    }

}
