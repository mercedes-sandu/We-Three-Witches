using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    /// <summary>
    /// The transform of the target the enemy will follow.
    /// </summary>
    public Transform target;

    /// <summary>
    /// The speed of the enemy.
    /// </summary>
    public float speed = 7f;
    
    /// <summary>
    /// The distance at which the enemy will stop following the target.
    /// </summary>
    public float nextWaypointDistance = 3f;
    
    /// <summary>
    /// True if the enemy is a flying type, false otherwise.
    /// </summary>
    [SerializeField] private bool isFlying = false;

    /// <summary>
    /// The path the enemy will follow.
    /// </summary>
    private Path _path;

    /// <summary>
    /// The enemy's current way point.
    /// </summary>
    private int _currentWayPoint = 0;
    
    /// <summary>
    /// True if the enemy has reached the end of the path, false otherwise.
    /// </summary>
    private bool _reachedEndOfPath = false;

    /// <summary>
    /// The seeker component of the enemy.
    /// </summary>
    private Seeker _seeker;

    /// <summary>
    /// The Rigidbody2D component of the enemy.
    /// </summary>
    private Rigidbody2D _rb;
    
    /// <summary>
    /// Subscribes to GameEvents.
    /// </summary>
    void Awake()
    {
        GameEvent.OnPlayerCreate += CreatePlayer;
    }
    
    /// <summary>
    /// Initializes components and calls for the path to be updated.
    /// </summary>
    void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = isFlying ? 0 : 6;
        // todo: change linear drag?
        
        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
    }

    /// <summary>
    /// Updates the path to the target.
    /// </summary>
    private void UpdatePath()
    {
        if (_seeker.IsDone())
        {
            _seeker.StartPath(_rb.position, target.position, OnPathComplete);
        }
    }

    /// <summary>
    /// Initializes the target to be the player.
    /// </summary>
    /// <param name="player">The player.</param>
    private void CreatePlayer(GameObject player)
    {
        target = player.transform;
    }
    
    /// <summary>
    /// Called when the path has been completed.
    /// </summary>
    /// <param name="p">The path.</param>
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWayPoint = 0;
        }
    }

    /// <summary>
    /// Updates the enemy's position and movement.
    /// </summary>
    void FixedUpdate()
    {
        if (_path == null)
        {
            return;
        }

        if (_currentWayPoint >= _path.vectorPath.Count)
        {
            _reachedEndOfPath = true;
            // todo: attack enemy
            Debug.Log("attack enemy");
            return;
        }
        else
        {
            _reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)_path.vectorPath[_currentWayPoint] - _rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        
        _rb.AddForce(force);
        
        float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWayPoint]);

        if (distance < nextWaypointDistance)
        {
            _currentWayPoint++;
        }

        transform.localScale = force.x switch
        {
            <= 0.01f => new Vector3(-1f, 1f, 1f),
            >= 0.01f => new Vector3(1f, 1f, 1f),
            _ => transform.localScale
        };
    }
    
    /// <summary>
    /// Unsubscribes from GameEvents.
    /// </summary>
    void OnDestroy()
    {
        GameEvent.OnPlayerCreate -= CreatePlayer;
    }
}