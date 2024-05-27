using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletsLayer;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity, bulletsLayer.transform);

            Vector3 rotation = mousePos - bullet.transform.position;

            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            bullet.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
    }
}
