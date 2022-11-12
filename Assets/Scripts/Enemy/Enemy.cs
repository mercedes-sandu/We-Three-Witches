using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// The damage the enemy deals.
    /// </summary>
    [SerializeField] private int damage = 10;
    
    /// <summary>
    /// The enemy's health.
    /// </summary>
    private int _health = 100;

    /// <summary>
    /// Damages the enemy with the specified amount.
    /// </summary>
    /// <param name="damage">The damage inflicted to the enemy.</param>
    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Called when the enemy reaches 0 health.
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Returns the damage the enemy deals.
    /// </summary>
    /// <returns>Enemy damage.</returns>
    public int GetDamage() => damage;

    /// <summary>
    /// Takes damage or damages the collider accordingly.
    /// </summary>
    /// <param name="col">The collider.</param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            var player = col.GetComponent<Player>();
            if (player.IsUsingSpecial())
            {
                TakeDamage(player.GetSpecialDamage());
            }
            else
            {
                player.TakeDamage(damage);
            }
        }
    }
}