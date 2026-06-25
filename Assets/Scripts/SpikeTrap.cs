using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public int damage = 50; // 踩到一次扣多少血

    private void OnTriggerEnter(Collider hit)
    {
        // 檢查踩上來的是不是玩家
        if (hit.gameObject.CompareTag("Player"))
        {
            // 檢查玩家現在是不是吃了防護罩 (無敵狀態)
            if (!PlayerCtrl.Instance.isInvincible)
            {
                PlayerCtrl.Instance.PlayerHP -= damage;
                Debug.Log("踩到陷阱！扣血：" + damage);
            }
            else
            {
                Debug.Log("無敵狀態！陷阱無效！");
            }
        }
    }
}