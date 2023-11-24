using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Project
{
    public class DragAndDropPresenter : MonoBehaviour
    {
        public delegate void OnDragAndDropHandler();
        public static event OnDragAndDropHandler OnDraggableSpot;
        public static event OnDragAndDropHandler OnDragged;
        public static event OnDragAndDropHandler OnSeeNothing;

        private const string TagOfDraggableGameObjects = "Draggable";

        [SerializeField] private int RotationSensivity = 10;

        [SerializeField] private float _dropForce = 150f;
        [SerializeField] private float _dragForce = 5f;
        [SerializeField] private float _rayLenght;
        [SerializeField] private Transform _camera;

        public Transform Hand;
        [SerializeField] private float _scrolSensivity;
        [SerializeField] private float _minPositionOnZ = 0.8f;
        [SerializeField] private float _maxPositionOnZ = 3;

        [SerializeField] private KeyCode _rotationButton = KeyCode.R;
        [SerializeField] private KeyCode _pickUpButton = KeyCode.Mouse0;
        [SerializeField] private KeyCode _launchButton = KeyCode.Mouse1;

        private Rigidbody _rigidbodyOfDraggableObject;
        private readonly int _defaultLayerNumber = 1;

        private GameObject _draggableObject;


        private void Update()
        {
            RaycastHit hit;

            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _rayLenght, _defaultLayerNumber))
            {
                if (hit.transform.CompareTag(TagOfDraggableGameObjects))
                {
                    SeeDraggableObject(hit);

                    CheckDragButton(hit);
                }
                else
                {
                    OnSeeNothing?.Invoke();

                }
            }
            else
            {
                SeeNothing();
            }


            if (_draggableObject != null)
            {
                CheckDropButton();
                CheckDropWithForceButton();
                CheckScrollAction();
                CheckRotateButton();
            }
        }

        private void CheckRotateButton()
        {
            if (Input.GetKey(_rotationButton))
            {
                RotateDraggableObject();
            }
            else if (Input.GetKeyUp(_rotationButton))
            {
                StopRotateDraggableObject();
            }
            if (Input.GetKeyUp(_pickUpButton))
            {
                StopRotateDraggableObject();
            }
        }

        private void RotateDraggableObject()
        {
            if (_draggableObject == null)
                return;

            //MouseLook mouseLook = FindObjectOfType<MouseLook>();
            //mouseLook.RotateDraggableObject = true;

            Vector3 directionOfRotation = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
            _draggableObject.transform.Rotate(directionOfRotation * RotationSensivity);
        }

        private void StopRotateDraggableObject()
        {
            //MouseLook mouseLook = FindObjectOfType<MouseLook>();
            //mouseLook.RotateDraggableObject = false;
        }
        private void SeeNothing()
        {
            OnSeeNothing?.Invoke();

        }

        private void FixedUpdate()
        {
            if (Input.GetKey(_pickUpButton) && _draggableObject != null)
                Drag();
        }
        private void CheckDragButton(RaycastHit hit)
        {
            if (Input.GetKey(_pickUpButton))
            {
                if (_draggableObject == null)
                {
                    PrepareToDrag(hit);
                }

            }
        }

        private void SeeDraggableObject(RaycastHit hit)
        {
            OnDraggableSpot?.Invoke();

        }

        private void CheckScrollAction()
        {
            if (Input.mouseScrollDelta.y != 0)
            {
                Scroll(Input.mouseScrollDelta.y * _scrolSensivity);
            }
        }

        private void CheckDropWithForceButton()
        {
            if (Input.GetKeyDown(_launchButton) && !Input.GetKey(_rotationButton))
            {
                DropWithForce();
            }
        }

        private void CheckDropButton()
        {
            if (Input.GetKeyUp(_pickUpButton))
            {
                Drop();
            }
        }

        private void Scroll(float value)
        {
            if (_draggableObject == null)
                return;

            float clampedPosition = Mathf.Clamp(Hand.localPosition.z + value, _minPositionOnZ, _maxPositionOnZ);

            Hand.SetLocalPositionAndRotation(new Vector3(Hand.localPosition.x,
                Hand.localPosition.y, clampedPosition), Quaternion.identity);
        }

        private void PrepareToDrag(RaycastHit hit)
        {
            if (hit.transform.GetComponent<Draggable>()._canInteract)
            {
                OnDragged?.Invoke();
                _draggableObject = hit.transform.gameObject;
                _draggableObject.GetComponent<Draggable>().PrepareToDrag();
                _rigidbodyOfDraggableObject = _draggableObject.GetComponent<Rigidbody>();
            }
        }

        private void Drop()
        {
            _draggableObject.GetComponent<Draggable>().PrepareToDrop();


            Hand.transform.localPosition = new Vector3(Hand.transform.localPosition.x,
                        Hand.transform.localPosition.y, _minPositionOnZ);
            _rigidbodyOfDraggableObject = null;
            _draggableObject = null;
        }

        private void DropWithForce()
        {
            Hand.transform.localPosition = new Vector3(Hand.transform.localPosition.x,
                Hand.transform.localPosition.y, _minPositionOnZ);


            _draggableObject.GetComponent<Draggable>().DropWithForce(_camera.forward, _dropForce);

            _rigidbodyOfDraggableObject = null;
            _draggableObject = null;
        }

        private void Drag()
        {
            Vector3 direction = Hand.position - _rigidbodyOfDraggableObject.position;
            _rigidbodyOfDraggableObject.velocity = direction * _dragForce;
        }
    }
}