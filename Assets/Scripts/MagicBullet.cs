using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    public float speed = 10f; // 子彈飛行的速度
    public float damage = 10f; // 打到玩家扣多少血

    void Start()
    {
        // 防呆機制：子彈生出來 5 秒後自動銷毀，避免飛到地圖外吃效能
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // 讓子彈每一幀都持續往自己的「正前方」飛
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider hit)
    {
        // 如果撞到的東西標籤是 Player
        if (hit.gameObject.CompareTag("Player"))
        {
            // 確認玩家目前"不是"無敵狀態才扣血
            if (!PlayerCtrl.Instance.isInvincible)
            {
                PlayerCtrl.Instance.PlayerHP -= damage;
            }

            // 打到玩家後，子彈銷毀自己
            Destroy(gameObject);
        }
    }
}