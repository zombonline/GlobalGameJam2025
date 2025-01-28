using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DamageDoer : MonoBehaviour
{
    [SerializeField] private float damage = 100f;
    [SerializeField] private string[] tagsCanDamage, tagsCanEffect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected");
        if (collision.gameObject.TryGetComponent(out Health health) && tagsCanDamage.Contains<string>(collision.gameObject.tag))
        {
            health.TakeDamage(damage);
        }
        if(tagsCanEffect.Contains<string>(collision.gameObject.tag))
        {
            if(collision.gameObject.tag == "Spike")
            {
                collision.gameObject.GetComponent<SpikeMovement>().SetTempCooldown(2f);
            }
        }

    }
}
