using UnityEngine;
using UnityEngine.SceneManagement; // 切換場景必須加這行

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuPanel; // 存放你的暫停面板

    // 1. 點擊「暫停」按鈕時執行
    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true); // 顯示暫停選單
        Time.timeScale = 0f;            // 魔法：把遊戲時間凍結！
    }

    // 2. 點擊「繼續」按鈕時執行
    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false); // 隱藏暫停選單
        Time.timeScale = 1f;             // 恢復遊戲時間
    }

    // 3. 點擊「重新開始」按鈕時執行
    public void RestartGame()
    {
        Time.timeScale = 1f; // ⚠️ 超級重要：重載場景前一定要先解凍時間！
        // 重新讀取目前的場景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    // 4. 點擊「回到主畫面」按鈕時執行
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // ⚠️ 一樣要解凍時間
        // 括號裡換成你主畫面場景的精準名稱，例如 "MainMenu"
        SceneManager.LoadScene("MainMenu"); 
    }
}