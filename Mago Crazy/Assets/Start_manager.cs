using UnityEngine.SceneManagement;
using UnityEngine;

public class Start_manager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Video");
    }
    public void Salir()
    {
        Application.Quit();
    }
    public void Tutorial()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
