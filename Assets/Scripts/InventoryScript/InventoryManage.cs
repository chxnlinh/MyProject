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

        if(item.skillPrefab != null)
        {

            Instantiate(item.skillPrefab, PlayerCtrl.Instance.transform.position,Quaternion.identity); 
        }
        if(item.healAmount > 0)
        {
            // PlayerCtrl.Instance.PlayerHP += item.healAmount;

            PlayerCtrl.Instance.Heal(item.healAmount); 
        }

        items.Remove(item);
        UIManage.Instance.UpdateInventory();
    }





}
