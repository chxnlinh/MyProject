using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedNPC : MonoBehaviour
{
    public Transform player; // 玩家的位置
    public GameObject bulletPrefab; // 子彈的預製物件
    public Transform firePoint; // 發射點

    public float attackRange = 15f; // 攻擊距離，玩家進入這個範圍就會開火
    public float fireRate = 2f; // 攻擊頻率，每隔幾秒開一槍
    private float nextFireTime = 0f; // 冷卻計時器

    void Start()
    {
        // 遊戲開始時，自動尋找場景中叫做 "Player" 的物件
        if (player == null)
        {
            player = GameObject.Find("Player").transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        // 計算 NPC 和玩家之間的直線距離
        float distance = Vector3.Distance(transform.position, player.position);

        // 如果玩家進入了射程範圍
        if (distance <= attackRange)
        {
            // 鎖定玩家：讓 NPC 永遠面向玩家 (鎖定 Y 軸，避免 NPC 仰角跌倒)
            Vector3 lookPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(lookPosition);

            // 檢查開火冷卻時間到了沒
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate; // 重新設定下一次能開火的時間
            }
        }
    }

    void Shoot()
    {
        // 在 FirePoint 的位置生成一顆子彈，角度跟 NPC 面對的方向一模一樣
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
    }
}