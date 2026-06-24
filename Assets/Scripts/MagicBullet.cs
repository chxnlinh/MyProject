using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    public float speed = 10f; // 子彈飛行的速度
    public float damage = 10f; // 確保這裡是 float

    void Start()
    {
        // 子彈生出來 5 秒後自動銷毀
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // 讓子彈往前飛
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider hit)
    {
        // 如果撞到的東西標籤是 Player
        if (hit.gameObject.CompareTag("Player"))
        {
            // 🔴 修正：確保扣血時，兩邊的型別都是 float。
            // 如果系統還是有疙瘩，我們可以直接在扣血時做強制轉換
            if (!PlayerCtrl.Instance.isInvincible)
            {
                PlayerCtrl.Instance.PlayerHP -= (int)damage; // 強制把傷害轉成整數來扣除，或是直接扣除
            }

            // 銷毀子彈
            Destroy(gameObject);
        }
    }
}