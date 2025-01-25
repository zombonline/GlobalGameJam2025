using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public PlayerInput playerInput;
    protected InputAction move;
    protected InputAction fire;

    protected Vector2 movementDir;
    protected Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        move = playerInput.actions["Move"];
        move.Enable();

        fire = playerInput.actions["Fire"];
        fire.Enable();
    }
    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
    }

    
}
