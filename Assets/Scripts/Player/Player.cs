using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    /// <summary>
    /// The player's health bar.
    /// </summary>
    private Image _healthBar;

    /// <summary>
    /// The animator component.
    /// </summary>
    private Animator _anim;
    
    /// <summary>
    /// The player's health.
    /// </summary>
    private int _health = 100;

    /// <summary>
    /// The amount of damage the player's special ability deals.
    /// </summary>
    private const int SpecialDamage = 20;

    /// <summary>
    /// The player's total mana.
    /// </summary>
    private const int TotalMana = 3;

    /// <summary>
    /// The player's current mana.
    /// </summary>
    private int _mana = 3;

    /// <summary>
    /// True if the player is regenerating mana, false otherwise.
    /// </summary>
    private bool _isRegeneratingMana = false;

    /// <summary>
    /// The rate at which mana regenerates (1 every ManaRegenerationRate seconds).
    /// </summary>
    private const float ManaRegenerationRate = 10f;

    /// <summary>
    /// The mana points.
    /// </summary>
    private readonly Image[] _manaPoints = new Image[3];
    
    /// <summary>
    /// The sprite for a full mana point.
    /// </summary>
    private Sprite _manaFull;
    
    /// <summary>
    /// The sprite for an empty mana point.
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

        _anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Regenerates the player's mana when necessary.
    /// </summary>
    void Update()
    {
        if (_mana < TotalMana && !_isRegeneratingMana)
        {
            StartCoroutine(RegenerateMana());
        }
    }
    
    /// <summary>
    /// Damages the player with the specified amount and updates the health bar.
    /// </summary>
    /// <param name="damage">The damage inflicted to the player.</param>
    public void TakeDamage(int damage)
    {
        _health -= damage;
        
        // todo: fix health bar ui
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
    /// Expends a mana point.
    /// </summary>
    public void ExpendMana()
    {
        _mana--;
        _manaPoints[_mana].sprite = _manaEmpty;
        Debug.Log(_mana);
    }

    /// <summary>
    /// Regains a mana point.
    /// </summary>
    private void RegainMana()
    {
        _mana++;
        _manaPoints[_mana - 1].sprite = _manaFull;
    }

    /// <summary>
    /// Regenerates the player's mana.
    /// </summary>
    /// <returns></returns>
    private IEnumerator RegenerateMana()
    {
        _isRegeneratingMana = true;
        while (_mana < TotalMana)
        {
            yield return new WaitForSeconds(ManaRegenerationRate);
            RegainMana();
        }
        _isRegeneratingMana = false;
    }

    /// <summary>
    /// Called when the player reaches 0 health.
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Returns the player's current number of mana points.
    /// </summary>
    /// <returns>Mana.</returns>
    public int GetMana() => _mana;
    
    /// <summary>
    /// Returns the amount of damage the player's special ability deals.
    /// </summary>
    /// <returns>Special damage.</returns>
    public int GetSpecialDamage() => SpecialDamage;

    /// <summary>
    /// Returns whether the player is using their special ability.
    /// </summary>
    /// <returns>True if the player is using their special ability, false otherwise.</returns>
    public bool IsUsingSpecial() => _anim.GetCurrentAnimatorStateInfo(0).IsName(name
        .Replace(" ", "").Replace("(Clone)", "") + "Special");
}