using UnityEngine;
using UnityEngine.Video;

public class VidPlayer : MonoBehaviour
{
    [SerializeField] string videoFilename;
    void Start()
    {
        PlayVideo();
        
    }

    public void PlayVideo()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFilename);
            Debug.Log(videoPath);
            videoPlayer.url = videoPath;
            videoPlayer.Play(); 
        }
    }
}
