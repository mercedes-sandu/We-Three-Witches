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
    /// The animator component of the enemy.
    /// </summary>
    private Animator _anim;
    
    /// <summary>
    /// Subscribes to GameEvents.
    /// </summary>
    void Awake()
    {
        GameEvent.OnPlayerCreate += CreatePlayer;
    }

    /// <summary>
    /// Initializes components.
    /// </summary>
    void Start()
    {
        _anim = GetComponent<Animator>();
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
        _anim.Play(name.Replace("(Clone)", "") + "Damaged");
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
        _anim.Play(name.Replace("(Clone)", "") + "Die");
    }

    /// <summary>
    /// Called by the animator when the die animation is finished.
    /// </summary>
    public void DestroyObject()
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