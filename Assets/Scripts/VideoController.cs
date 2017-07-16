using UnityEngine;
using UnityEngine.Video;
using System.Collections;

//VideoPlayerゲームオブジェクト制御クラス
public class VideoController : MonoBehaviour
{
    const float THUMB_WAIT = 2.0f;

    VideoPlayer videoPlayer;
    AudioSource audioSource;
    VideoClip videoClip;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setting(string path)
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.url = path;
        videoPlayer.isLooping = true;
        videoPlayer.loopPointReached += VideoPlayer_loopPointReached;
        videoClip = Resources.Load(path) as VideoClip;
        audioSource = gameObject.AddComponent<AudioSource>();

        //下の2つは念のための程度の設定です
        //AudioのOutputModeを選択
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        //audioTrack[0]を有効に
        videoPlayer.EnableAudioTrack(0, true);

        //audioSourceにaudioTrack[0]を設定？？
        videoPlayer.SetTargetAudioSource(0, audioSource);
        //コレが最後に来るのが大事(直感)
        //videoPlayer.clip = videoClip;

        audioSource.volume = 0;
        ////再生
        videoPlayer.Play();
        Invoke("Stop", THUMB_WAIT);
    }

    public void Play()
    {
        videoPlayer.Play();
    }

    public void Stop()
    {
        videoPlayer.Stop();
        audioSource.volume = GameObject.Find("VideoManager").GetComponent<VideoManagerController>().Volume;
    }

    /// <summary>
    /// ループイベント
    /// </summary>
    /// <remarks>
    /// 本イベントをシーク終了イベントとみなしてランダム再生処理を実行
    /// </remarks>
    /// <param name="source"></param>
    private void VideoPlayer_loopPointReached(VideoPlayer source)
    {
        GetComponentInParent<DisplayController>().Stop();
        GameObject.Find("VideoManager").GetComponent<VideoManagerController>().NextPlay();
    }

}
