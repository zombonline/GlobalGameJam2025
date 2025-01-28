using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private float health = 100f;
    private bool canTakeDamage = true;

    public delegate void DeathDelegate(string tag);
    public static event DeathDelegate onDeath;

    public void TakeDamage(float damage)
    {
        if(!canTakeDamage)
        {
            return;
        }
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        onDeath?.Invoke(this.gameObject.tag);
        Destroy(gameObject);
    }

    public void SetCanTakeDamage(bool canTakeDamage)
    {
        this.canTakeDamage = canTakeDamage;
    }
}
