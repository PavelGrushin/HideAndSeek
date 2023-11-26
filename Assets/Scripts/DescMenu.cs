using UnityEngine;

public class DescMenu : MonoBehaviour
{
    [SerializeField] private GameObject _cameraMenuDesc;
    [SerializeField] private GameObject _cameraMapDesc;
    [SerializeField] private GameObject _cameraShopDesc;

    [SerializeField] private GameObject _crossHair;
    [SerializeField] private PlayerMovement _movement;


    private bool _menuDesc;
    private bool _mapDesc;
    private bool _shopDesc;

    private void Update()
    {
        OnOff();
        if (Input.GetKeyDown(KeyCode.Escape) && _movement.enabled != false)
        {
            ActiveDesc();
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.S)) && _movement.enabled == false)
        {
            BackDesc();
        }
        else if (Input.GetKeyDown(KeyCode.A) && _movement.enabled == false && _menuDesc == true && _shopDesc == false)
        {
            _menuDesc = false;
            _shopDesc = false;
            _mapDesc = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && _movement.enabled == false && _menuDesc == false && _shopDesc == false)
        {
            _menuDesc = true;
            _mapDesc = false;
            _shopDesc = false;
        }
        else if (Input.GetKeyDown(KeyCode.D) && _movement.enabled == false && _menuDesc == true && _mapDesc == false)
        {
            _menuDesc = false;
            _mapDesc = false;
            _shopDesc = true;
        }
        else if (Input.GetKeyDown(KeyCode.A) && _movement.enabled == false && _menuDesc == false && _mapDesc == false)
        {
            _menuDesc = true;
            _mapDesc = false;
            _shopDesc = false;
        }
    }

    private void OnOff()
    {
        if (_menuDesc)
            _cameraMenuDesc.SetActive(true);
        else
            _cameraMenuDesc.SetActive(false);
        if (_mapDesc)
            _cameraMapDesc.SetActive(true);
        else
            _cameraMapDesc.SetActive(false);
        if (_shopDesc)
            _cameraShopDesc.SetActive(true);
        else
            _cameraShopDesc.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)               //CollideDesc
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ActiveDesc();
        }
    }
    private void ActiveDesc()                                   //ActiveDesc
    {
        _cameraMenuDesc.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        _crossHair.SetActive(false);
        _movement.enabled = false;

        _menuDesc = true;
        _mapDesc = false;
        _shopDesc = false;
    }
    public void ClickBattonBack()                              //ButtonBack
    {
        BackDesc();
    }
    private void BackDesc()                                         //BackDesc
    {
        _cameraMapDesc.SetActive(false);
        _cameraMenuDesc.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        _crossHair.SetActive(true);
        _movement.enabled = true;

        _menuDesc = false;
        _mapDesc = false;
        _shopDesc = false;

    }
    public void ClickBattonExit()                               //Exit
    {
        Application.Quit();
    }
}
