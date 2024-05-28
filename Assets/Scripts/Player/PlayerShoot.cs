using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletsLayer;
    [SerializeField] private int ammoAmount = 30;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private float reloadTime = 1f;
    private int currentAmmo;
    private bool isReloading;
    private void Start()
    {
        currentAmmo = ammoAmount;
        ammoText.text = "Ammo: " + currentAmmo;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!isReloading)
        {
            if (currentAmmo <= 0)
            {
                StartCoroutine(Reloading());
                return;
            }

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity, bulletsLayer.transform);
            Vector3 rotation = mousePos - bullet.transform.position;
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

            CameraShake.instance.ShakeCamera(.25f);

            currentAmmo--;
            UpdateAmmoUI();
        }
    }

    private void UpdateAmmoUI()
    {
        ammoText.text = "Ammo: " + currentAmmo;
    }

    private IEnumerator Reloading()
    {
        ammoText.text = "Reloading...";
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);
        currentAmmo = ammoAmount;
        ammoText.text = "Ammo: " + currentAmmo;
        isReloading = false;
    }
}
