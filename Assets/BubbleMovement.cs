using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BubbleMovement : PlayerMovement
{
    [SerializeField] float moveForce = 5f;

    private void Update()
    {
        Aim(move.ReadValue<Vector2>());
        if (fire.triggered)
        {
            Fire();
        }

    }
    private void Aim(Vector2 val)
    {
        movementDir = val.normalized;
    }

    private void Fire()
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(movementDir * moveForce, ForceMode2D.Impulse);
    }

}
