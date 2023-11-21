using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _normalSpeed = 8f; //8
    [SerializeField] private float _boostSpeed = 15f;
    [SerializeField] private float _smoothTime; //0.2 скорость вращения объекта
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _camera;
    [SerializeField] private Animator _animator;

    private Vector3 _direction;
    private float _speed;
    private float _smoothVelocity;
    void Start()
    {
        _speed = _normalSpeed;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        BoostSpeed();
    }

    private void Move()                 //Движение, повороты.
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

            _animator.SetBool("Walk", true);
        }
        if (_direction.magnitude <= 0.1f)
        {
            _animator.SetBool("Walk", false);
        }
    }

    private void BoostSpeed()           //Бег
    {
        if (Input.GetKey(KeyCode.LeftShift) && _direction.magnitude >= 0.1f)
        {
            _speed = _boostSpeed;
            _animator.SetBool("Run", true);
        }
        else
        {
            _speed = _normalSpeed;
            _animator.SetBool("Run", false);
        }

    }

}
