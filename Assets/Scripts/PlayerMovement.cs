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

    protected Vector2 velocityLastFrame;

    protected bool canMove = true;

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
        rb.bodyType = canMove ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;

    }
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

    protected void GetVelocityLastFrame()
    {
        velocityLastFrame = rb.linearVelocity;
    }


}
