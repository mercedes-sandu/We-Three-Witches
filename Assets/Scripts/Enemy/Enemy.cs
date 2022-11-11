using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// The enemy's health.
    /// </summary>
    private int _health = 100;

    /// <summary>
    /// The player.
    /// </summary>
    private Player _player;

    /// <summary>
    /// Initializes components.
    /// </summary>
    void Start()
    {
        _player = FindObjectOfType<Player>();
    }
    
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
    /// Detects collisions and takes damage accordingly.
    /// </summary>
    /// <param name="col">The collision.</param>
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player") && _player.IsUsingSpecial())
        {
            TakeDamage(_player.GetSpecialDamage());
        }
    }
}