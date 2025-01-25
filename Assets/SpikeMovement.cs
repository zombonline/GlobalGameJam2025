using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class SpikeMovement : PlayerMovement
{
    [SerializeField] private float pressLength = 0.5f;
    [SerializeField] private float moveForce = 50f;
    [SerializeField] private float partialChargeMovementEffector = 0.5f;
    private float moveChargeValue = 0f;

    [SerializeField] private float cooldownBetweenCharges = 1f;
    private float cooldownTimer = 0f;


    [SerializeField] Image cooldownImageFill, cooldownImageBack;
    private void Update()
    {
        Aim(move.ReadValue<Vector2>());
        if (fire.IsPressed() && cooldownTimer <= 0)
        {
            Fire();
        }
        if (fire.WasReleasedThisFrame())
        {
            if(moveChargeValue > pressLength)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(movementDir * moveForce, ForceMode2D.Impulse);
            }
            else
            {
                float partialCharValue = moveChargeValue / pressLength;
                rb.velocity = Vector2.zero;
                rb.AddForce(movementDir * moveForce * partialChargeMovementEffector * partialCharValue, ForceMode2D.Impulse);
            }
            moveChargeValue = 0f;
            cooldownTimer = cooldownBetweenCharges;
            cooldownImageFill.fillAmount = 0f;
        }
        cooldownTimer -= Time.deltaTime;
        if(cooldownTimer <= 0)
        {
            cooldownImageBack.color = new Color(46f,46f,46f, 0.5f);
        }
        else
        {
            cooldownImageBack.color = Color.red;
        }
    }
    private void Aim(Vector2 val)
    {
        movementDir = val.normalized;
    }

    private void Fire()
    {
        moveChargeValue += Time.deltaTime;
        cooldownImageFill.fillAmount = moveChargeValue / pressLength;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = Vector2.zero;
        }
    }
}
