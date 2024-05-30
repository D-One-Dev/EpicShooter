using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifetime;
    void Start()
    {
        _rb.velocity = transform.right * bulletSpeed;
        StartCoroutine(BulletLife());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<IDamageable>().Hurt(1);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<IDamageable>().Hurt(1);
        }
        Destroy(gameObject);
    }

    private IEnumerator BulletLife()
    {
        yield return new WaitForSeconds(lifetime);

        Destroy(gameObject);
    }
}
