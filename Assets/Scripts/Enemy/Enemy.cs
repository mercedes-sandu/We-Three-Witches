using Pathfinding;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// The damaged sound.
    /// </summary>
    [SerializeField] private AudioClip damagedSound;
    
    /// <summary>
    /// The damage the enemy deals.
    /// </summary>
    [SerializeField] private int damage = 10;

    /// <summary>
    /// The enemy's health bar.
    /// </summary>
    [SerializeField] private Slider slider;

    /// <summary>
    /// The UI offset
    /// </summary>
    [SerializeField] private Vector3 offset;

    /// <summary>
    /// The UI scale.
    /// </summary>
    [SerializeField] private float scale;
    
    /// <summary>
    /// The low health bar color.
    /// </summary>
    private readonly Color32 _lowHealthColor = new Color32(255, 255, 255, 255);
    
    /// <summary>
    /// The high health bar color.
    /// </summary>
    private readonly Color32 _highHealthColor = new Color32(0, 255, 145, 255);

    /// <summary>
    /// The enemy's maximum health.
    /// </summary>
    private readonly int _maxHealth = 100;
    
    /// <summary>
    /// The enemy's current health.
    /// </summary>
    private int _health = 100;

    /// <summary>
    /// The enemy's current target.
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
    /// The main camera.
    /// </summary>
    private Camera _camera;
    
    /// <summary>
    /// The audio source component.
    /// </summary>
    private AudioSource _audioSource;
    
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
        _camera = Camera.main;
        _audioSource = GetComponent<AudioSource>();
        slider.value = _health;
        slider.maxValue = _maxHealth;
        slider.transform.localScale = new Vector3(scale, scale, 1);
    }

    /// <summary>
    /// Updates the position of the health bar.
    /// </summary>
    void Update()
    {
        slider.transform.position = _camera.WorldToScreenPoint(transform.position + offset);
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
        _audioSource.PlayOneShot(damagedSound);
        _anim.Play(name.Replace("(Clone)", "") + "Damaged");
        _health -= d;
        slider.value = _health;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(_lowHealthColor, 
            _highHealthColor, slider.normalizedValue);
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