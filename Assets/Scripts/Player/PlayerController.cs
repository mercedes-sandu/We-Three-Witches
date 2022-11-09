using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// The player's horizontal movement speed.
    /// </summary>
    [SerializeField] private float speed = 5f;
    
    /// <summary>
    /// The player's vertical jump force.
    /// </summary>
    [SerializeField] private float jumpForce = 10f;
    
    /// <summary>
    /// The GameObject which detects if the player is grounded.
    /// </summary>
    [SerializeField] private Transform groundCheck;

    /// <summary>
    /// What is tagged as the ground for the player.
    /// </summary>
    [SerializeField] private LayerMask groundLayer;

    /// <summary>
    /// The horizontal movement input.
    /// </summary>
    private float _horizontal;
    
    /// <summary>
    /// True if the player is facing right, false if the player is facing left.
    /// </summary>
    private bool _facingRight = true;

    /// <summary>
    /// The player's Rigidbody2D component.
    /// </summary>
    private Rigidbody2D _rb;
    
    /// <summary>
    /// The player's Animator component.
    /// </summary>
    private Animator _anim;

    /// <summary>
    /// The main camera in the scene.
    /// </summary>
    private Camera _camera;

    /// <summary>
    /// Initializes components.
    /// </summary>
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _camera = Camera.main;
    }

    /// <summary>
    /// Detects input for the player and changes the player's state accordingly.
    /// </summary>
    void Update()
    {
        // Moving horizontally
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _horizontal = -1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _horizontal = 1f;
        }
        else
        {
            _horizontal = 0f;
        }

        // Jumping
        if (IsGrounded() && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }
        if (IsGrounded() && (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Space)))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
        }

        // Special ability
        if (Input.GetKeyDown(KeyCode.E))
        {
            // todo: special ability
            Debug.Log("player used special ability");
        }
        
        // Animation
        _anim.SetBool("moving", _horizontal != 0f);
        
        // Flip sprite if changing direction
        Flip();
    }

    /// <summary>
    /// Moves the player horizontally.
    /// </summary>
    void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * speed, _rb.velocity.y);
    }

    /// <summary>
    /// Flips the player's sprite if the player is moving in a different direction.
    /// </summary>
    private void Flip()
    {
        if (_facingRight && _horizontal < 0f || !_facingRight && _horizontal > 0f)
        {
            _facingRight = !_facingRight;
            transform.Rotate(0, 180f, 0);
        }
    }

    /// <summary>
    /// Returns whether the player is grounded.
    /// </summary>
    /// <returns>True if the player is grounded, false otherwise.</returns>
    private bool IsGrounded() => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
}