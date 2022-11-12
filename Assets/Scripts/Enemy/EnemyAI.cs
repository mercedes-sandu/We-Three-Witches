using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 7f;
    
    public float nextWaypointDistance = 3f;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private bool isFlying = false;

    private Path _path;

    private int _currentWayPoint = 0;
    
    private bool _reachedEndOfPath = false;

    private Seeker _seeker;

    /// <summary>
    /// 
    /// </summary>
    private Rigidbody2D _rb;
    
    /// <summary>
    /// Subscribes to GameEvents.
    /// </summary>
    void Awake()
    {
        GameEvent.OnPlayerCreate += CreatePlayer;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = isFlying ? 0 : 6;
        // todo: change linear drag?
        
        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
    }

    private void UpdatePath()
    {
        if (_seeker.IsDone())
        {
            _seeker.StartPath(_rb.position, target.position, OnPathComplete);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>
    private void CreatePlayer(GameObject player)
    {
        target = player.transform;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWayPoint = 0;
        }
    }

    /// <summary>
    /// 
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