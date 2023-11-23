using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(Rigidbody))]

    public class Draggable : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private const string DraggableTag = "Draggable";
        private const string DefaultLayerName = "Default";
        private const string DraggableLayerName = "Draggable";
        private const float TimeToRestoreInteractive = 0.5f;

        public bool _canInteract { get; private set; }
        private int _defaultLayer;
        private int _draggableLayer;

        private void Start()
        {
            _canInteract = true;
            tag = DraggableTag;

            _rigidbody = GetComponent<Rigidbody>();
            _defaultLayer = LayerMask.NameToLayer(DefaultLayerName);
            _draggableLayer = LayerMask.NameToLayer(DraggableLayerName);

        }


        public void PrepareToDrop()
        {
            gameObject.layer = _defaultLayer;
            _rigidbody.useGravity = true;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            _rigidbody.constraints = RigidbodyConstraints.None;
            DisableInteractive();
        }

        public void PrepareToDrag()
        {
            gameObject.layer = _draggableLayer;
            _rigidbody.useGravity = false;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        public void DropWithForce(Vector3 Direction, float dropForce)
        {
            gameObject.layer = _defaultLayer;
            _rigidbody.useGravity = true;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            _rigidbody.constraints = RigidbodyConstraints.None;

            _rigidbody.AddForce(Direction * dropForce, ForceMode.Force);
            DisableInteractive();
        }

        private void DisableInteractive()
        {
            _canInteract = false;

            Invoke("RestoreInteractive", TimeToRestoreInteractive);
        }

        private void RestoreInteractive()
        {
            _canInteract = true;
        }
    }
}