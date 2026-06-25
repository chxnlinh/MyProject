using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider hit)
    {
        // 如果踩進來的是玩家
        if (hit.gameObject.CompareTag("Player"))
        {
            LevelManager.Instance.ShowWinScreen(); // 呼叫勝利畫面
        }
    }
}