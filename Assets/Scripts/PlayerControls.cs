using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{   
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private InputActions controls;
    [SerializeField] private InputActionReference lookAction;
    private Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new InputActions();
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    // Update is called once per frame
    void Update()
    {
        moveInput = controls.Player.Move.ReadValue<Vector2>();

        // 1. Read the mouse screen position from the New Input System
        Vector2 mouseScreenPos = controls.Player.Look.ReadValue<Vector2>();

        // 2. Convert to World space 
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, mainCamera.nearClipPlane));

        // 3. Calculate direction and angle
        Vector2 direction = mouseWorldPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 4. Apply Z rotation (Adjust the '- 90f' offset depending on your sprite's default face)
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    void FixedUpdate(){
        rb.linearVelocity = moveInput * moveSpeed;
    }
}
