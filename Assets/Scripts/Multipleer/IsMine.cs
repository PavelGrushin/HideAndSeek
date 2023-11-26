using UnityEngine;
using Photon.Pun;

public class IsMine : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _camera;
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private GameObject _model;

    void Start()
    {
        if (!_photonView.IsMine)
        {
            _playerMovement.enabled = false;
            _camera.SetActive(false);
            _model.SetActive(true);
        }
    }
}
