using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// The player's health.
    /// </summary>
    private int _health = 100;
    
    /// <summary>
    /// Damages the player with the specified amount.
    /// </summary>
    /// <param name="damage">The damage inflicted to the player.</param>
    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Called when the player reaches 0 health.
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
    }
}