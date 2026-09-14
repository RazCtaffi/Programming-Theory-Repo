using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRb;
    private InputSystem_Actions _controls;
    private Vector2 _moveInput;

    // Encapsulation:
    // Backing fields are kept private to protect internal state from unauthorized external manipulation.
    // Public properties provide controlled access with setter validation to prevent game-breaking values.

    [Header("Movement Settings")]
    [SerializeField] private float _baseSpeed = 12.0f;
    private float _currentSpeed;
    public float Speed
    {
        get { return _currentSpeed; }
        set { _currentSpeed = Mathf.Max(0f, value); }
    }

    [Header("Shooting Settings")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _bulletSpeed = 500.0f;
    [SerializeField] private float _baseFireRate = 0.5f;
    private float _currentFireRate;
    public float FireRate
    {
        get { return _currentFireRate; }
        set { _currentFireRate = Mathf.Max(0.05f, value); }
    }
    public bool IsSpreadShotActive { get; private set; } = false;
    public float ActiveSpreadAngle { get; private set; } = 0f;
    private float _nextFireTime;
    private int _speedBoostCount = 0;
    private int _rapidFireCount = 0;
    private int _spreadShotCount = 0;

    private void Start()
    {
        if (MainManager.Instance != null)
        {
            SetColor(MainManager.Instance.ChosenColor);
        }
    }

    private void Awake()
    {
        _controls = new InputSystem_Actions();
        _playerRb = GetComponent<Rigidbody>();
        _currentFireRate = _baseFireRate;
        _currentSpeed = _baseSpeed;
    }

    private void OnEnable()
    {
        _controls.Player.Enable();
    }
    private void OnDisable()
    {
        _controls.Player.Disable();
    }

    // Abstraction:
    // Update() acts as a high-level coordinator. It reads input and delegates action to specialized methods,
    // hiding the low-level implementation details of physics translation and object instantiation.

    private void Update()
    {
        _moveInput = _controls.Player.Move.ReadValue<Vector2>();
        if (_controls.Player.Shoot.WasPressedThisFrame() && Time.time >= _nextFireTime)
        {
            Shoot();
        }

        if (_controls.Player.Quit.WasPressedThisFrame() && MainManager.Instance != null)
        {
            MainManager.Instance.ReturnToMenu();
        }
    }

    // Abstraction:
    //Simple call hides vector normalization, frame timing, and boundary clamping.

    private void FixedUpdate()
    {
        Move();
        //ConstrainPlayerPos();
    }

    private void SetColor(Color color)
    {
        if (TryGetComponent<Renderer>(out Renderer playerRenderer))
        {
            // Applies chosen color to the player sphere material instance
            playerRenderer.material.color = color;
        }
    }

    private void Move()
    {
        // Movement Using AddForce

        //float horizontalInput = _moveInput.x;
        //float verticalInput = _moveInput.y;
        //_playerRb.AddForce(new Vector3(horizontalInput, 0, verticalInput) * _moveSpeed);

        //Movement Using MovePosition

        Vector3 direction = new Vector3(_moveInput.x, 0, _moveInput.y).normalized;
        Vector3 targetPosition = _playerRb.position + direction * (Speed * Time.fixedDeltaTime);

        targetPosition.x = Mathf.Clamp(targetPosition.x, -18, 18);
        targetPosition.z = Mathf.Clamp(targetPosition.z, -10, 10);

        _playerRb.MovePosition(targetPosition);
    }


    private void Shoot()
    {
        _nextFireTime = Time.time + FireRate;
        if (IsSpreadShotActive)
        {
            FireBullet(0f);
            FireBullet(-ActiveSpreadAngle);
            FireBullet(ActiveSpreadAngle);
        }
        else
        {
            FireBullet(0f);
        }
    }

    private void FireBullet(float yAngleOffset)
    {
        Quaternion spreadRotation = Quaternion.Euler(0, yAngleOffset, 0);

        Quaternion finalRotation = _firePoint.rotation * spreadRotation * Quaternion.Euler(90, 0, 0);

        GameObject bulletInstance = Instantiate(_bulletPrefab, _firePoint.position, finalRotation);

        if (bulletInstance.TryGetComponent<Rigidbody>(out Rigidbody bulletRb))
        {
            Vector3 shootDirection = (_firePoint.rotation * spreadRotation) * Vector3.forward;
            bulletRb.AddForce(shootDirection * _bulletSpeed);
        }
    }

    public void ApplySpeedBoost(float boostAmount)
    {
        _speedBoostCount++;
        _currentSpeed = _baseSpeed + boostAmount;
    }

    public void RemoveSpeedBoost()
    {
        _speedBoostCount = Mathf.Max(0, _speedBoostCount - 1);
        if (_speedBoostCount == 0)
        {
            _currentSpeed = _baseSpeed;
        }
    }

    public void ApplyRapidFire(float newFireRate)
    {
        _rapidFireCount++;
        _currentFireRate = newFireRate;
    }

    public void RemoveRapidFire()
    {
        _rapidFireCount = Mathf.Max(0, _rapidFireCount - 1);
        if( _rapidFireCount == 0)
        {
            _currentFireRate = _baseFireRate;
        }
    }

    public void ApplySpreadShot(float angle)
    {
        _spreadShotCount++;
        ActiveSpreadAngle = angle;
        IsSpreadShotActive = true;
    }

    public void RemoveSpreadShot()
    {
        _spreadShotCount = Mathf.Max(0, _spreadShotCount - 1);
        if (_spreadShotCount == 0)
        {
            IsSpreadShotActive = false;
        }
    }

    //Use ConstrainPlayerPos() method only if moving the player using AddForce.
    //private void ConstrainPlayerPos()
    //{

    //    Vector3 currentPos = transform.position;
    //    if (Mathf.Abs(currentPos.x) > 18)
    //    {
    //        transform.position = new Vector3(Mathf.Sign(currentPos.x) * 18, currentPos.y, currentPos.z);
    //        _playerRb.linearVelocity = new Vector3(0, _playerRb.linearVelocity.y, _playerRb.linearVelocity.z);
    //    }
    //    if (Mathf.Abs(currentPos.z) > 10)
    //    {
    //        transform.position = new Vector3(currentPos.x, currentPos.y, Mathf.Sign(currentPos.z) * 10);
    //        _playerRb.linearVelocity = new Vector3(_playerRb.linearVelocity.x, _playerRb.linearVelocity.y, 0);
    //    }
    //}
}