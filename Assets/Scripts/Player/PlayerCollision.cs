using UnityEngine;
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Aid"))
        {
            _playerHealth.ChangeHealth(1);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _playerHealth.ChangeHealth(-1);
        }
    }
}