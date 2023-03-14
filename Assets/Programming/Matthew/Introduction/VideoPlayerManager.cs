using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoPlayerManager : MonoBehaviour
{
    public VideoPlayer video;
    private int sceneIndex;

    private void Start()
    {
        video.loopPointReached += CheckOver;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        sceneIndex += 1;
        SceneManager.LoadScene(sceneIndex);
    }
}
