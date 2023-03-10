using UnityEngine;
using UnityEngine.Video;

public class IntroductionVideoController : MonoBehaviour
{
    public VideoClip[] videoClips;
    private VideoPlayer videoPlayer;
    private int videoClipIndex;

    [SerializeField] private GameObject backgroundPanel;
    
    ViewManager viewManager;
    [SerializeField] private ViewType viewTypeAfterIntro;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoClipIndex = 0;
        if (backgroundPanel != null)
        {
            backgroundPanel.SetActive(false);
        }
    }

    private void Start()
    {
        StartVideo();
        
    } 

    private void StartVideo()
    {
        if (!AudioManager.Instance.isMusicOn)
        {
          videoPlayer.SetDirectAudioMute(0, true);  
        }
        videoPlayer.clip = videoClips[videoClipIndex];
        videoPlayer.Play();
        if (videoClipIndex >= videoClips.Length - 1)
        {
            videoPlayer.loopPointReached += SkipVideo;
        }
        else
        {
            videoPlayer.loopPointReached += OpenPanel;
        }
    }

    public void SkipVideo(VideoPlayer vp)
    {
        if (videoClipIndex >= videoClips.Length - 1)
        {
            videoPlayer.Stop();
            ViewManager.Instance.SwitchView(viewTypeAfterIntro);
        }
        else
        {
            videoPlayer.Stop();
            videoClipIndex++;
        }
        if (backgroundPanel != null)
        {
            backgroundPanel.SetActive(false);
        }
        StartVideo();
    }
    
    private void OpenPanel(VideoPlayer vp)
    {
        backgroundPanel.SetActive(true);
    }
}