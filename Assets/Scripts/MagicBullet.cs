using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    public float speed = 10f; // 子彈飛行的速度
    public float damage = 20f; // 傷害值 (設定為 20，並保持 float 型別)

    void Start()
    {
        // 雙重保險 1：子彈生出來 5 秒後自動銷毀，避免沒打到東西飛到無限遠佔用效能
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // 讓子彈不斷往前飛
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider hit)
    {
        // 情況 A：打到玩家
        if (hit.gameObject.CompareTag("Player"))
        {
            // 檢查玩家有沒有吃無敵防護罩
            if (!PlayerCtrl.Instance.isInvincible)
            {
                // 強制把 float 傷害轉成 int 整數來扣血
                PlayerCtrl.Instance.PlayerHP -= (int)damage;
            }

            // 打到玩家後子彈消失
            Destroy(gameObject); 
        }
        // 情況 B：打到牆壁、地板或地形
        else if (hit.gameObject.CompareTag("Untagged") || hit.gameObject.name.Contains("Cube"))
        {
            // 雙重保險 2：撞到場景物件直接碎掉，避免子彈穿牆
            Destroy(gameObject);
        }
    }
}