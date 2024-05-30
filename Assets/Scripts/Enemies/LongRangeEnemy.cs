using System.Collections;
using UnityEngine;

public class LongRangeEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    [SerializeField] private float sightDistance;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletsLayer;
    [SerializeField] private float recoilTime;
    private bool isActive;
    private bool hasShot;
    private Transform player;
    public void Hurt(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            ScoreController.instance.AddScore(100);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, sightDistance, playerLayer);
        if(hit.collider != null)
        {
            player = hit.collider.gameObject.transform;
            if (!isActive) isActive = true;
            Shoot();
        }

        if (isActive)
        {
            if (player.position.x > transform.position.x) transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            else if (player.position.x < transform.position.x) transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void Shoot()
    {
        if (!hasShot)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletsLayer);
            bullet.transform.localEulerAngles = transform.localEulerAngles;
            hasShot = true;
            StartCoroutine(Recoil());
        }
    }

    private IEnumerator Recoil()
    {
        yield return new WaitForSeconds(recoilTime);
        hasShot = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * sightDistance);
    }
}
