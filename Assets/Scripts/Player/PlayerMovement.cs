using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform footPoint;
    [SerializeField] private float jumpColliderRadius;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private Animator _animator; 

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

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (transform.position.x > mouseWorldPos.x) transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        else if (transform.position.x < mouseWorldPos.x) transform.localEulerAngles = new Vector3(0f, 0f, 0f);

        //if (movement < 0f) transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        //else if (movement > 0f) transform.localEulerAngles = new Vector3(0f, 0f, 0f);

        _animator.SetFloat("XSpeed", Mathf.Abs(_rb.velocity.x));
        _animator.SetFloat("YSpeed", Mathf.Abs(_rb.velocity.y));
    }

    private void Jump()
    {
        RaycastHit2D hit = Physics2D.CircleCast(footPoint.position, jumpColliderRadius, -Vector2.up, jumpColliderRadius, groundMask);
        if(hit.collider != null)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            _rb.AddForce(Vector2.up * jumpForce);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(footPoint.position, jumpColliderRadius);
    }
}