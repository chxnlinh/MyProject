using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public ItemData itemData;

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Player"))
        {
            InventoryManage.Instance.AddItem(itemData);
            Destroy(gameObject); 
        }
    }
}
