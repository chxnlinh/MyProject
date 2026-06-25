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
    }
}

public class NPC_spawner
{
    public void spawnerNPC(int hp, int DeteDist, int attkPower, Vector3 pos)
    {
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