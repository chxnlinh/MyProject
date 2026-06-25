using UnityEngine;

public class NPCDeathChecker : MonoBehaviour
{
    private NPCctrl myNPC;
    private bool isDead = false;

    void Start()
    {
        myNPC = GetComponent<NPCctrl>();
    }

    void Update()
    {
        if (!isDead && myNPC != null && myNPC.NPC_HP <= 0)
        {
            isDead = true; 
            LevelManager.Instance.AddKill(); 
        }
    }
}