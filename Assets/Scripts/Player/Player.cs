using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    /// <summary>
    /// The player's health bar.
    /// </summary>
    private Image _healthBar;
    
    /// <summary>
    /// The player's health.
    /// </summary>
    private int _health = 100;

    /// <summary>
    /// The player's mana.
    /// </summary>
    private int _mana = 3;

    /// <summary>
    /// 
    /// </summary>
    private readonly Image[] _manaPoints = new Image[3];
    
    /// <summary>
    /// 
    /// </summary>
    private Sprite _manaFull;
    
    /// <summary>
    /// 
    /// </summary>
    private Sprite _manaEmpty;
    
    /// <summary>
    /// The initial health bar width.
    /// </summary>
    private float _initialHealthBarWidth;
    
    /// <summary>
    /// The current health bar width.
    /// </summary>
    private float _currentHealthBarWidth;

    void Start()
    {
        _healthBar = GameObject.Find("Health Bar").GetComponent<Image>();
        
        _manaPoints[0] = GameObject.Find("Mana 1").GetComponent<Image>();
        _manaPoints[1] = GameObject.Find("Mana 2").GetComponent<Image>();
        _manaPoints[2] = GameObject.Find("Mana 3").GetComponent<Image>();
        
        _manaFull = Resources.Load<Sprite>("Sprites/manafull");
        _manaEmpty = Resources.Load<Sprite>("Sprites/manaempty");
        
        foreach (Image point in _manaPoints)
        {
            point.sprite = _manaFull;
        }
        
        _initialHealthBarWidth = _healthBar.rectTransform.rect.width;
        _currentHealthBarWidth = _initialHealthBarWidth;
    }
    
    /// <summary>
    /// Damages the player with the specified amount and updates the health bar.
    /// </summary>
    /// <param name="damage">The damage inflicted to the player.</param>
    public void TakeDamage(int damage)
    {
        _health -= damage;
        
        float widthModifier = _initialHealthBarWidth * damage / _health;
        _currentHealthBarWidth -= widthModifier;
        _healthBar.rectTransform.sizeDelta = new Vector2(_currentHealthBarWidth, 
            _healthBar.rectTransform.rect.height);
        _healthBar.rectTransform.anchoredPosition = new Vector2(_healthBar.rectTransform.anchoredPosition.x 
                                                               - widthModifier, 
            _healthBar.rectTransform.anchoredPosition.y);
        if (_currentHealthBarWidth <= 0)
        {
            _currentHealthBarWidth = 0;
            _healthBar.rectTransform.sizeDelta = new Vector2(0, _healthBar.rectTransform.rect.height);
        }
        
        if (_health <= 0)
        {
            _health = 0;
            Die();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ExpendMana()
    {
        _mana--;
    }

    /// <summary>
    /// Called when the player reaches 0 health.
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
    }
}