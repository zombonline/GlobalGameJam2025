using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyPlayer : MonoBehaviour
{
    [SerializeField] public PlayerInput playerInput;

    private InputAction teamSwap;

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
    }
    private void OnDisable()
    {
        teamSwap.Disable();
    }
    private void Update()
    {
        if (teamSwap.triggered)
        {
            Swap();
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

}
