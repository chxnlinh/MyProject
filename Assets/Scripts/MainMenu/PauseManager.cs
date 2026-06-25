using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuPanel; 

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true); 
        Time.timeScale = 0f;            
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false); 
        Time.timeScale = 1f;             
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu"); 
    }
}