using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    private bool _nextScene;
    private bool _loadingComplite;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Update()
    {
        if (_loadingComplite) 
        {
        if (Input.GetKey(KeyCode.Space) || _nextScene)
        {
            SceneManager.LoadScene("1_LobbySingle");
        }      
        }
    }

    public override void OnConnectedToMaster()
    {
        _loadingComplite = true;
        _textMeshPro.enabled = true;
    }

    public void EndScreensaver(int end)
    {
        if(end == 1)
        {
            _nextScene = true;
        }
    }
}
