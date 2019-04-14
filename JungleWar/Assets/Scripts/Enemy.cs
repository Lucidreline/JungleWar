using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        health -= damageDealer.GetDamage();
        ProcessHit(damageDealer);
        Destroy(other.gameObject);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
