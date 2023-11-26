using UnityEngine;
using Photon.Pun;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _spawn;

    void Start()
    {
       PhotonNetwork.Instantiate(_player.name, _spawn.position, Quaternion.identity);  
    }
}
