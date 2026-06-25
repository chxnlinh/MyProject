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
        // 檢查如果還沒死，且血量歸零了
        if (!isDead && myNPC != null && myNPC.NPC_HP <= 0)
        {
            isDead = true; // 標記為已死亡，避免重複計算
            LevelManager.Instance.AddKill(); // 通知大管家 +1 分
        }
    }
}