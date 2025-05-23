using UnityEngine;

public class Pause_manager : MonoBehaviour
{
    public GameObject canvas;
    public playerController player;

    void Update()
    {
        canvas.SetActive(player.isPaused);
    }
}
