using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class SpikeMovement : PlayerMovement
{
    [SerializeField] private float pressLength = 0.5f;
    [SerializeField] private float chargeForce = 50f;
    [SerializeField] private float partialChargeEffector = 0.5f;
    private float chargePressValue = 0f;

    [SerializeField] private float cooldownBetweenCharges = 1f;
    private float cooldownTimer = 0f;

    [SerializeField] private float moveSpeed = 1f;

    [SerializeField] Image chargeImageFill, chargeImageBackground;
    private void Update()
    {
        base.Update();
        if (!canMove) return;
        Aim(move.ReadValue<Vector2>());
        if (fire.IsPressed() && cooldownTimer <= 0)
        {
            Fire();
        }
        if (fire.WasReleasedThisFrame() && cooldownTimer <= 0)
        {
            if(movementDir == Vector2.zero)
            {
                chargePressValue = 0f;
                chargeImageFill.fillAmount = 0f;
                return;
            }
            if (chargePressValue > pressLength)
            {
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(movementDir * chargeForce, ForceMode2D.Impulse);
            }
            else
            {
                float partialCharValue = chargePressValue / pressLength;
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(movementDir * chargeForce * partialChargeEffector * partialCharValue, ForceMode2D.Impulse);
            }
            chargePressValue = 0f;
           
            cooldownTimer = cooldownBetweenCharges;
            chargeImageFill.fillAmount = 0f;
        }
        cooldownTimer -= Time.deltaTime;
        if(cooldownTimer <= 0)
        {
            chargeImageBackground.color = new Color(46f,46f,46f, 0.5f);
        }
        else
        {
            chargeImageBackground.color = Color.red;
        }
    }
    private void FixedUpdate()
    {
        if (chargePressValue == 0 && cooldownTimer <= 0)
        {
            rb.MovePosition(rb.position + movementDir * moveSpeed * Time.fixedDeltaTime);
        }
    }
    private void Fire()
    {
        chargePressValue += Time.deltaTime;
        chargeImageFill.fillAmount = chargePressValue / pressLength;
    }

    private void Aim(Vector2 val)
    {
        movementDir = val.normalized;
    }
    public float GetMoveChargeValue()
    {
        return chargePressValue;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            rb.linearVelocity = Vector2.zero;
        }
        if(collision.gameObject.CompareTag("Wall Breakable"))
        {
            Destroy(collision.gameObject);
            rb.linearVelocity = velocityLastFrame;
        }
    }
    public void SetTempCooldown(float val)
    {
        chargeImageFill.fillAmount = 0f;
        chargeImageBackground.color = Color.red;
        cooldownTimer = val;
    }
}
