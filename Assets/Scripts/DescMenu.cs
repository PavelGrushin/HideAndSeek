using UnityEngine;

public class DescMenu : MonoBehaviour
{
    [SerializeField] private GameObject _cameraDesc;
    [SerializeField] private GameObject _crossHair;
    [SerializeField] private PlayerMovement _movement;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            _cameraDesc.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            _crossHair.SetActive(true);
            _movement.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _cameraDesc.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            _crossHair.SetActive(false);
            _movement.enabled = false;
        }
    }
    public void ClickBattonBack()                              //Back
    {
        _cameraDesc.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        _crossHair.SetActive(true);
        _movement.enabled = true;
    }
    public void ClickBattonExit()                               //Exit
    {
        Application.Quit();
    }
}
