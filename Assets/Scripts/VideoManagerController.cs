using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

//VideoManagerゲームオブジェクト制御クラス
public class VideoManagerController : MonoBehaviour
{
    #region "Const"

    const string PREFIX = "file://";
    const string OP_PATH = @"H:\XPShare\ANIME\OP";
    const string SHARE_PATH = PREFIX + @"";
    const float THUMB_SHOW_WAIT = 1.0f;

    #endregion "Const"

    #region "Variant"

    [SerializeField]
    public GameObject _org;

    /// <summary>動画ファイルパス一覧</summary>
    private List<string> _videoPath = new List<string>();
    /// <summary>複製されたDisplayゲームオブジェクト一覧</summary>
    private List<GameObject> _WallVideoList = new List<GameObject>();
    /// <summary>キュー</summary>
    private Queue<GameObject> _wallVideoQueue = new Queue<GameObject>();

    private bool _isPlaying = false;
    private bool _isEnabled = false;
    private float _volume = 1f;

    #endregion "Variant"

    #region "Property"

    /// <summary>
    /// 再生判定
    /// </summary>
    public bool IsPlaying
    {
        get { return _isPlaying; }
        set { _isPlaying = value; }
    }

    /// <summary>
    /// ENABLE制御判定
    /// </summary>
    public bool IsEnabled
    {
        get { return _isEnabled; }
        set { _isEnabled = value; }
    }

    public float Volume
    {
        get { return _volume; }
        set
        {
            _volume = value;
            if (PlayingVideo != null)
                PlayingVideo.GetComponentInChildren<AudioSource>().volume = value;
        }
    }

    /// <summary>
    /// 再生中のゲームオブジェクト
    /// </summary>
    public GameObject PlayingVideo { get; set; }

    #endregion "Property"

    // Use this for initialization
    void Start()
    {
        CreateVideoPath();
        CreateWallVideo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateVideoPath()
    {
        var SrcDir = new string[] { OP_PATH };
        List<string> getFiles = new List<string>();

        Array.ForEach(SrcDir, Path =>
        {
            getFiles = Directory.GetFiles(Path, "*.*", SearchOption.TopDirectoryOnly).ToList();
        });

        getFiles.ForEach(path =>
        {
            string[] split = path.Split(System.IO.Path.DirectorySeparatorChar);
            string dir = string.Format("{0}/{1}/{2}/{3}/{4}", split[0], split[1], split[2], split[3], split[4]);
            _videoPath.Add(PREFIX + dir);
        });
    }

    /// <summary>
    /// Displayオブジェクト複製/配置
    /// </summary>
    private void CreateWallVideo()
    {
        uint Cnt = 1;
        float margin = 1f;
        Vector3 prevPos = _org.transform.localPosition;
        _org.SetActive(false);
        _videoPath.ForEach(path =>
        {
            if ((Cnt % 10) == 0)
            {
                prevPos = _org.transform.localPosition;
                if (Cnt < 10)
                {
                    prevPos = new Vector3(prevPos.x, prevPos.y, prevPos.z);
                }
                else if (Cnt < 20)
                {
                    prevPos = new Vector3(prevPos.x, prevPos.y - 1, prevPos.z);
                }
                else if (Cnt < 30)
                {
                    prevPos = new Vector3(prevPos.x, prevPos.y - 2, prevPos.z);
                }
                else if (Cnt < 40)
                {
                    prevPos = new Vector3(prevPos.x, prevPos.y - 3, prevPos.z);
                }
                else if (Cnt < 50)
                {
                    prevPos = new Vector3(prevPos.x, prevPos.y - 4, prevPos.z);
                }
                else if (Cnt < 60)
                {
                    prevPos = new Vector3(prevPos.x, prevPos.y - 5, prevPos.z);
                }
                else if (Cnt < 70)
                {
                    prevPos = new Vector3(prevPos.x, prevPos.y - 6, prevPos.z);
                }
                else if (Cnt < 80)
                {
                    prevPos = new Vector3(prevPos.x, prevPos.y - 7, prevPos.z);
                }
            }

            GameObject instance = Instantiate<GameObject>(_org);
            instance.GetComponentInChildren<VideoController>().Setting(path);
            prevPos = new Vector3(prevPos.x + margin, prevPos.y, prevPos.z);
            instance.transform.position = prevPos;
            instance.SetActive(false);
            _WallVideoList.Add(instance);
            Cnt++;
        });

        // サムネイル表示
        Invoke("ShowThumbnail", THUMB_SHOW_WAIT);

        // ランダム再生用キュー構築
        ReloadVideoQueue();
    }

    /// <summary>
    /// サムネイル表示
    /// </summary>
    private void ShowThumbnail()
    {
        _WallVideoList.ForEach(obj =>
        {
            obj.GetComponentInChildren<VideoController>().ShowThumbnail();
            obj.SetActive(true);
        });
    }

    /// <summary>
    /// ランダム再生用キューの構築
    /// </summary>
    public void ReloadVideoQueue()
    {
        var copyTmp = new List<GameObject>(_WallVideoList);

        var s = copyTmp.OrderBy(obj => Guid.NewGuid()).ToList();
        s.ForEach(obj =>
        {
            _wallVideoQueue.Enqueue(obj);
        });
    }

    /// <summary>
    /// ランダム再生
    /// </summary>
    public void NextPlay()
    {
        if (_wallVideoQueue.Count == 0)
        {
            ReloadVideoQueue();
        }
        _wallVideoQueue.Dequeue().GetComponent<DisplayController>().Play();
    }

    /// <summary>
    /// スキップ
    /// </summary>
    public void Skip()
    {
        if (!IsPlaying)
        {
            return;
        }

        PlayingVideo.GetComponent<DisplayController>().Stop();
        NextPlay();
    }
}
