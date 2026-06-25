using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedNPC : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float attackRange = 15f;
    public float fireRate = 2f;
    private float nextFireTime = 0f;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            Vector3 lookPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(lookPosition);

            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
    }
}