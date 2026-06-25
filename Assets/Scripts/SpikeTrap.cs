using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public int damage = 50;
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
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