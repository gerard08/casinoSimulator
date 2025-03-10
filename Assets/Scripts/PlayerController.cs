using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;

/// <summary>
/// Just a crappy character controller for the video
/// </summary>
public class PlayerController : NetworkBehaviour
{
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _camera = GetComponentInChildren<Camera>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the center of the screen
        Cursor.visible = false; // Hides the cursor
    }


    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            // Disable the camera on remote player instances
            if (_camera != null)
            {
                _camera.gameObject.SetActive(false);
            }

            this.enabled = false;
        }
    }

    private void Update()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    #region Movement

    [SerializeField] private float speed = 80;
    [SerializeField] private float mouseSensitivity = 2.0f;
    private float verticalLookRotation = 0;

    private Vector3 _input;
    private Rigidbody _rb;
    private Camera _camera;

    private void HandleMovement()
    {
        // Player movement
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float verticalInput = Input.GetAxis("Vertical");     // W/S or Up/Down
        Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;
        transform.position += moveDirection * speed;
    }

    #endregion

    #region Rotation

    private void HandleRotation()
    {
        Time deltaTime;

        // Mouse look (horizontal rotation)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);

        // Mouse look (vertical rotation)
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        _camera.transform.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);
    }

    #endregion
}