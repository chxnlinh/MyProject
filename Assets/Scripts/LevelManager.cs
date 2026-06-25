using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int killCount = 0; // 目前擊殺數
    public GameObject winDoor; // 要打開的門
    public GameObject winPanel; // 勝利 UI 畫面

    private void Awake()
    {
        Instance = this;
    }

    // 當有 NPC 死亡時，會呼叫這個功能
    public void AddKill()
    {
        killCount++;
        Debug.Log("目前擊殺數：" + killCount);

        // 如果殺滿 3 隻，就把門關閉（打開通道）
        if (killCount >= 3)
        {
            if (winDoor != null)
            {
                winDoor.SetActive(false); 
                Debug.Log("大門已開啟！");
            }
        }
    }

    // 玩家踩到終點時呼叫
    public void ShowWinScreen()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true); // 顯示勝利畫面
            Time.timeScale = 0f; // 時間凍結
        }
    }
}