using UnityEngine;

public class MagicWeapon : MonoBehaviour
{
    /// <summary>
    /// The projectile prefab.
    /// </summary>
    [SerializeField] private GameObject projectilePrefab;
    
    /// <summary>
    /// 0 if the magic weapon is single burst, nonzero otherwise.
    /// </summary>
    [SerializeField] private float fireRate = 0;

    /// <summary>
    /// The point from which the projectile emerges.
    /// </summary>
    [SerializeField] private Transform firePoint;

    /// <summary>
    /// The time at which the next projectile can be fired.
    /// </summary>
    private float _timeToFire = 0;

    /// <summary>
    /// The camera in the scene.
    /// </summary>
    private Camera _camera;

    /// <summary>
    /// The direction in which the projectile will be fired.
    /// </summary>
    private Vector2 _lookDirection;

    /// <summary>
    /// The angle at which the projectile will be fired.
    /// </summary>
    private float _lookAngle;

    /// <summary>
    /// Sets the camera.
    /// </summary>
    void Start()
    {
        _camera = Camera.main;
    }
    
    /// <summary>
    /// Detects for input and calls for the projectile to be fired.
    /// </summary>
    void Update()
    {
        if (fireRate == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && Time.time > _timeToFire)
            {
                _timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    /// <summary>
    /// Shoots the projectile.
    /// </summary>
    private void Shoot()
    {
        _lookDirection = new Vector2(_camera.ScreenToWorldPoint(Input.mousePosition).x,
            _camera.ScreenToWorldPoint(Input.mousePosition).y);
        _lookAngle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, _lookAngle);
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}