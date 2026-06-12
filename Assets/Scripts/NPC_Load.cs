using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Load : MonoBehaviour
{
    int NPC_Total = 5;

   public  Transform npcT01;
   public  Transform npcT02; 
        

    void Start()
    {
        Vector3 pos = transform.position; 
        NPC_spawner npc01 = new NPC_spawner();
        npc01.spawnerNPC(120, 5, 10, npcT01.position );

        NPC_spawner npc02 = new NPC_spawner();
        npc02.spawnerNPC(200, 10, 20, npcT02.position);


    }

    // Update is called once per frame
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




public class NPC_spawner: MonoBehaviour
{
    public void spawnerNPC(int hp, int DeteDist, int attkPower, Vector3 pos)
    {
        GameObject NPC = Instantiate(Resources.Load("NPC", typeof(GameObject))) as GameObject;
        NPC.transform.position = pos;
        NPCctrl npcCtrl = NPC.GetComponent<NPCctrl>();
        npcCtrl.NPC_maxHP = hp;
        npcCtrl.DetcDist = DeteDist; 
    }

}
