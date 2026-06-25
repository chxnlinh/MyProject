using UnityEngine;

public class BossShooter : MonoBehaviour
{
    public Transform playerTarget;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float fireRate = 1.5f;
    private float nextFireTime = 0f;

    void Update()
    {
        if (playerTarget == null) return;

        transform.LookAt(playerTarget.position);

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}