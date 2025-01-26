using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private float health = 100f;
    private bool canTakeDamage = true;

    public delegate void DeathDelegate(GameObject gameObject);
    public event DeathDelegate onDeath;


    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        onDeath?.Invoke(this.gameObject);
        Destroy(gameObject);
    }

    public void SetCanTakeDamage(bool canTakeDamage)
    {
        canTakeDamage = canTakeDamage;
    }
}
