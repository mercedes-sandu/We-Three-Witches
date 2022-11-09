using System;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private float speed = 5f;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private float jumpForce = 10f;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Transform groundCheck;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private LayerMask groundLayer;

    /// <summary>
    /// 
    /// </summary>
    private float _horizontal;
    
    /// <summary>
    /// 
    /// </summary>
    private bool _facingRight = true;
    
    /// <summary>
    /// 
    /// </summary>
    private Vector2 _pointingDirection = Vector2.zero;

    /// <summary>
    /// 
    /// </summary>
    private Rigidbody2D _rb;
    
    /// <summary>
    /// 
    /// </summary>
    private Animator _anim;

    /// <summary>
    /// 
    /// </summary>
    private Camera _camera;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _camera = Camera.main;
    }

    /// <summary>
    /// 
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

        // Shooting
        _pointingDirection = (_camera.WorldToScreenPoint(Input.mousePosition) - transform.position).normalized;
        if (Input.GetMouseButtonDown(0))
        {
            // todo: shoot
            Debug.Log("player shot in direction " + _pointingDirection);
        }
        
        // Special ability
        if (Input.GetKey(KeyCode.E))
        {
            // todo: special ability
            Debug.Log("player used special ability");
        }
        
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
    /// 
    /// </summary>
    private void Flip()
    {
        if (_facingRight && _horizontal < 0f || !_facingRight && _horizontal > 0f)
        {
            _facingRight = !_facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>True if the player is grounded, false otherwise.</returns>
    private bool IsGrounded() => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
}