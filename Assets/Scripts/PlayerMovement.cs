using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed; //8
    [SerializeField] private float _smoothTime; //0.2 скорость вращения объекта
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _camera;

    private float _smoothVelocity;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {

            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref _smoothVelocity, _smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 move = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;

            _characterController.Move(move.normalized * _speed * Time.deltaTime);
        }
    }
}
