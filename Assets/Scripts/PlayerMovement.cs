using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float _normalSpeed = 8f;           //  8
    [SerializeField] private float _boostSpeed = 15f;           //  15
    [SerializeField] private float _smoothTime;                 //  0.1 скорость вращения объекта

    [Header("Jump")]
    [SerializeField] private float _groundDistance = 0.4f;      //  0.4
    [SerializeField] private float _gravity = -50f;             //  -30
    [SerializeField] private float _jumpHeight = 4.5f;          //  3

    [Header("Components")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _camera;
    //[SerializeField] private Animator _animator;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _scaleFace;

    [Header("Keys")]
    [SerializeField] private KeyCode _jump = KeyCode.Space;
    [SerializeField] private KeyCode _run = KeyCode.LeftShift;
    [SerializeField] private KeyCode _sitDown = KeyCode.LeftControl;

    private Vector3 _direction;
    private Vector3 _velocity;
    private float _speed;
    private float _smoothVelocity;

    private bool _isGraunded;
    private bool _isSitting;

    void Start()
    {
        _isSitting = false;
        _speed = _normalSpeed;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        BoostSpeed();
        Jump();
        SitDown();
    }

    private void Move()                 //  Движение, повороты.
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        _direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (_direction.magnitude >= 0.1f)
        {

            float rotationAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref _smoothVelocity, _smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 move = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;

            _characterController.Move(move.normalized * _speed * Time.deltaTime);

            //_animator.SetBool("Walk", true);
        }
        if (_direction.magnitude <= 0.1f)
        {
            //_animator.SetBool("Walk", false);
        }
    }
    private void BoostSpeed()           //  Бег
    {
        if (Input.GetKey(_run) && _direction.magnitude >= 0.1f)
        {
            _speed = _boostSpeed;
            //_animator.SetBool("Run", true);
        }
        else
        {
            _speed = _normalSpeed;
            //_animator.SetBool("Run", false);
        }

    }
    private void Jump()                 //  Прыжок
    {
        _isGraunded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);

        if (_isGraunded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        if (Input.GetKey(_jump) && _isGraunded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);

            //_animator.SetBool("Jump", true);
        }
        if (!_isGraunded)
        {
            //_animator.SetBool("Jump", false);
        }
    }
    private void SitDown()              //  Сидеть
    {
        if (Input.GetKeyDown(_sitDown) && !_isSitting)
        {
            _characterController.height = 1f;
            _characterController.center = new Vector3(0f, 0.5f, 0f);
            _isSitting = true;
            _speed = _normalSpeed;
           // _animator.SetBool("Crouch", true);
        }
        else if (_direction.magnitude >= 0.1f && _isSitting)
        {
            //_animator.SetBool("SneakWalk", true);
            if (Input.GetKeyDown(_sitDown))
            {
                //_animator.SetBool("Walk", true);
            }
        }
        else if (Input.GetKeyDown(_sitDown) && _isSitting)
        {
            _characterController.height = 1.84f;
            _characterController.center = new Vector3(0f, 0.94f, 0f);
            _isSitting = false;
            //_animator.SetBool("Crouch", false);
        }
        else if (_direction.magnitude <= 0.1f)
        {
            //_animator.SetBool("SneakWalk", false);
        }
    }
}
