using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Inventory/Item")] 
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    public GameObject skillPrefab;
    public int healAmount; 

    // ==========================================
    // [新增] 新道具的屬性
    // 如果數值大於 0，背包就會知道這個道具有這個效果！
    // ==========================================
    public float speedBoostDuration; // 加速持續時間
    public float shieldDuration;     // 無敵持續時間
}