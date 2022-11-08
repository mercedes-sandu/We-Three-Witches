using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Rigidbody2D))]
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
    private Vector2 _facingDirection = Vector2.right;
    
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
    private bool _grounded;

    /// <summary>
    /// 
    /// </summary>
    private bool _running;
    
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
        Vector2 dir = Vector2.zero;
        _pointingDirection = (_camera.WorldToScreenPoint(Input.mousePosition) - transform.position).normalized;
        
        if (_grounded && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space)))
        {
            dir.y = 1f;
            _grounded = false;
        }
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            dir.x = -1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            dir.x = 1f;
        }

        if (Input.GetMouseButtonDown(0))
        {
            // todo: shoot
            Debug.Log("player shot in direction " + _pointingDirection);
        }

        if (Input.GetKey(KeyCode.E))
        {
            // todo: special ability
            Debug.Log("player used special ability");
        }
        
        _running = dir.magnitude > 0f;
        
        _rb.velocity = dir.normalized * speed;
    }
}