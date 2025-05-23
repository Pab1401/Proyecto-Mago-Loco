using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class pause_menu_manager : MonoBehaviour
{
    public playerController player;
    public void Exit()
    {
        SceneManager.LoadScene("Start");
    }

    public void Resume()
    {
        player.Paused();
    }
}
