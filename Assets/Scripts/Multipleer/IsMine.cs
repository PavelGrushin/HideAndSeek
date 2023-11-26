using UnityEngine;
using Photon.Pun;

public class IsMine : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _mineCamera;
    [SerializeField] private GameObject _freeLookCamera;
    [SerializeField] private GameObject _virtualCamera;
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private GameObject _model;

    void Start()
    {
        if (!_photonView.IsMine)
        {
            _playerMovement.enabled = false;
            _mineCamera.SetActive(false);
            _freeLookCamera.SetActive(false);
            _virtualCamera.SetActive(false);
            _model.SetActive(true);
        }
    }
}
