using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class mover : MonoBehaviour
{
    public VideoPlayer video;

    void Start()
    {
        video.loopPointReached += OnVideoLoop;
    }

    void OnVideoLoop(VideoPlayer vp)
    {
        SceneManager.LoadScene("Final");
    }

}
