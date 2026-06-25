using UnityEngine;

public class BossShooter : MonoBehaviour
{
    public Transform playerTarget; // 玩家
    public GameObject bulletPrefab; // 子彈的預製物件
    public Transform firePoint; // 發射孔

    public float fireRate = 1.5f; // 每 1.5 秒射一次
    private float nextFireTime = 0f;

    void Update()
    {
        if (playerTarget == null) return;

        // 1. 永遠面向玩家 (自動瞄準)
        transform.LookAt(playerTarget.position);

        // 2. 計時器：時間到就開火
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // 在發射孔的位置，生成一顆子彈
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}