using UnityEngine;

public class CloseRangeEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    [SerializeField] private float viewRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float moveSpeed;
    private Transform player;

    private bool isActive;

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, viewRadius, transform.right, viewRadius, playerLayer);
        if(hit.collider != null)
        {
            if (!isActive)
            {
                isActive = true;
                player = hit.collider.gameObject.transform;
            }
        }

        if(player != null)
        {
            if (player.position.x - .1f > transform.position.x)
            {
                transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                _rb.velocity = transform.right * moveSpeed;
                _animator.SetTrigger("Run");
            }
            else if (player.position.x + .1f < transform.position.x)
            {
                transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                _rb.velocity = transform.right * moveSpeed;
                _animator.SetTrigger("Run");
            }
            else
            {
                _rb.velocity = Vector3.zero;
                _animator.SetTrigger("Idle");
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("!1111");
            _animator.SetTrigger("Attack");
        }
    }

    public void Hurt(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            ScoreController.instance.AddScore(100);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
