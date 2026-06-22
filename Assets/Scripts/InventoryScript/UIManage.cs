using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 



public class UIManage : MonoBehaviour
{
    public static UIManage Instance;
    public Transform inventoryContent;
    public GameObject inventoryButtonPrefab;
    public GameObject inventoryUI;
    private void Awake()
    {
        if (Instance == null)
            Instance = this; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            inventoryUI.SetActive(!inventoryUI.activeSelf); 

    }

    public void UpdateInventory()
    {
        foreach (Transform child in inventoryContent)
        {
            Destroy(child.gameObject); 
        }
        
        foreach(ItemData itemX in InventoryManage.Instance.items)
        {
            GameObject buttonObj = Instantiate(inventoryButtonPrefab, inventoryContent);
            buttonObj.GetComponentInChildren<TMP_Text>().text = itemX.itemName;
            buttonObj.GetComponent<Image>().sprite = itemX.icon;
            Button btn = buttonObj.GetComponent<Button>();
            btn.onClick.AddListener( ()=> InventoryManage.Instance.UseItem(itemX)); 

        }

    }

}
