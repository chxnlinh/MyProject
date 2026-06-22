using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManage : MonoBehaviour
{

    public static InventoryManage Instance;
    public List<ItemData> items = new List<ItemData>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this; 
    }

    public void AddItem(ItemData item)
    {
        items.Add(item);
        UIManage.Instance.UpdateInventory(); 
    }

    public void UseItem(ItemData item)
    {
        if (item.skillPrefab != null)
        {
            // 你原本的技能邏輯
        }
        
        if (item.healAmount > 0)
        {
            // 補血道具
            PlayerCtrl.Instance.Heal(item.healAmount);
        }

        // ==========================================
        // [新增] 判斷並觸發新道具效果
        // ==========================================
        if (item.speedBoostDuration > 0)
        {
            // 呼叫 PlayerCtrl 的加速功能
            PlayerCtrl.Instance.DrinkEnergyPotion(item.speedBoostDuration);
        }

        if (item.shieldDuration > 0)
        {
            // 呼叫 PlayerCtrl 的無敵防護罩功能
            PlayerCtrl.Instance.ActivateShield(item.shieldDuration);
        }

        items.Remove(item);
        UIManage.Instance.UpdateInventory();
    }





}
