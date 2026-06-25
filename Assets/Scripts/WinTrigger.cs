using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            LevelManager.Instance.ShowWinScreen(); 
        }
    }
}