using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Rigidbody2D _rb;

    private PlayerInput _playerInput;
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Gameplay.Jump.performed += ctx => Jump();
    }
    private void OnEnable()
    {
        _playerInput.Enable();
    }
    private void OnDisable()
    {
        _playerInput.Disable();   
    }

    private void FixedUpdate()
    {
        float movement = _playerInput.Gameplay.Movement.ReadValue<float>();

        _rb.velocity = new Vector2(movement * movementSpeed, _rb.velocity.y);

        if (movement < 0f) transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        else if (movement > 0f) transform.localEulerAngles = new Vector3(0f, 0f, 0f);
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        _rb.AddForce(Vector2.up * jumpForce);
    }
}
