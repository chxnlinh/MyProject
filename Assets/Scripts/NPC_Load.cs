using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Load : MonoBehaviour
{
    int NPC_Total = 5;

    public Transform npcT01;
    public Transform npcT02; 
        
    void Start()
    {
        // 補上防呆機制：確保你已經在 Unity 面板裡拖曳了生成點物件
        if (npcT01 != null && npcT02 != null) 
        {
            NPC_spawner npc01 = new NPC_spawner();
            npc01.spawnerNPC(120, 5, 10, npcT01.position);

            NPC_spawner npc02 = new NPC_spawner();
            npc02.spawnerNPC(160, 10, 20, npcT02.position);
        }
        else
        {
            Debug.LogWarning("【提醒】請記得在 Inspector 面板把 npcT01 跟 npcT02 拖曳進去喔！");
        }
    }

    void Update()
    {
        /*
        float number = Random.Range(0, 2000f);
        if(NPC_Total > 0 && number < 1)
        {
            GameObject NPC = Instantiate( Resources.Load("NPC", typeof(GameObject))) as GameObject;
            NPC.transform.position = transform.position  + new Vector3( Random.Range(-2f, 2), 0, Random.Range(-2f, 2));
            NPC_Total--; 
        }
        */
    }
}

// ====================================================
// 🔴 關鍵修正 1：把後面的 ": MonoBehaviour" 刪掉，讓它變成普通類別
// ====================================================
public class NPC_spawner
{
    public void spawnerNPC(int hp, int DeteDist, int attkPower, Vector3 pos)
    {
        // 🔴 關鍵修正 2：因為移除了 MonoBehaviour，呼叫 Instantiate 前面必須加上 GameObject.
        GameObject NPC = GameObject.Instantiate(Resources.Load("NPC", typeof(GameObject))) as GameObject;
        NPC.transform.position = pos;
        
        NPCctrl npcCtrl = NPC.GetComponent<NPCctrl>();
        if (npcCtrl != null)
        {
            npcCtrl.NPC_maxHP = hp;
            npcCtrl.DetcDist = DeteDist; 
        }
    }
}