using UnityEngine;

public class Projectile : MonoBehaviour
{
    /// <summary>
    /// The speed of the projectile.
    /// </summary>
    [SerializeField] private float speed = 20f;

    /// <summary>
    /// The amount of damage the projectile inflicts.
    /// </summary>
    [SerializeField] private int damage = 5;

    /// <summary>
    /// The shoot sound.
    /// </summary>
    [SerializeField] private AudioClip shootSound;

    /// <summary>
    /// The Rigidbody2D component of the projectile.
    /// </summary>
    private Rigidbody2D _rb;
    
    /// <summary>
    /// The audio source component.
    /// </summary>
    private AudioSource _audioSource;
    
    /// <summary>
    /// Sets components and projectile velocity.
    /// </summary>
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(shootSound);
        _rb.velocity = transform.right * speed;
    }

    /// <summary>
    /// Inflicts damage to the collider and destroys the projectile.
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player") && !col.CompareTag("Background"))
        {
            if (col.CompareTag("Enemy"))
            { 
                col.GetComponent<Enemy>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Destroys the projectile if it leaves the screen.
    /// </summary>
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}