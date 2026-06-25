using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int killCount = 0;
    public GameObject winDoor;
    public GameObject winPanel;

    private void Awake()
    {
        Instance = this;
    }

    public void AddKill()
    {
        killCount++;
        Debug.Log("目前擊殺數：" + killCount);

        if (killCount >= 3)
        {
            if (winDoor != null)
            {
                winDoor.SetActive(false); 
                Debug.Log("大門已開啟！");
            }
        }
    }

    public void ShowWinScreen()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true); 
            Time.timeScale = 0f; 
        }
    }
}