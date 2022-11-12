using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // /// <summary>
    // /// 
    // /// </summary>
    // public AIPath aiPath;
    
    /// <summary>
    /// The damage the enemy deals.
    /// </summary>
    [SerializeField] private int damage = 10;
    
    /// <summary>
    /// The enemy's health.
    /// </summary>
    private int _health = 100;

    /// <summary>
    /// 
    /// </summary>
    private GameObject _player;
    
    /// <summary>
    /// The player component of the player.
    /// </summary>
    private Player _playerComponent;
    
    /// <summary>
    /// Subscribes to GameEvents.
    /// </summary>
    void Awake()
    {
        GameEvent.OnPlayerCreate += CreatePlayer;
    }

    /// <summary>
    /// Initializes the player variables.
    /// </summary>
    /// <param name="player">The player.</param>
    private void CreatePlayer(GameObject player)
    {
        _playerComponent = player.GetComponent<Player>();
    }

    /// <summary>
    /// Damages the enemy with the specified amount.
    /// </summary>
    /// <param name="d">The damage inflicted to the enemy.</param>
    public void TakeDamage(int d)
    {
        _health -= d;
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
    /// Takes damage or damages the collider accordingly.
    /// </summary>
    /// <param name="col">The collider.</param>
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            if (_playerComponent.IsUsingSpecial())
            {
                TakeDamage(_playerComponent.GetSpecialDamage());
            }
            else
            {
                _playerComponent.TakeDamage(damage);
            }
        }
    }

    /// <summary>
    /// Unsubscribes from GameEvents.
    /// </summary>
    void OnDestroy()
    {
        GameEvent.OnPlayerCreate -= CreatePlayer;
    }
}