using UnityEngine;

public class DisplayController : MonoBehaviour
{
    #region "Const"

    static readonly Vector3 DISPLAY_SCALE = new Vector3(1.6f, 0.9f, 1f);
    static readonly Vector3 DISPLAY_POS = new Vector3(0f, 1f, -9.1f);

    #endregion "Const"

    #region "Variant"

    Vector3 _prevPos;
    Vector3 _prevScale;
    VideoManagerController _videoManager;

    #endregion "Variant"

    // Use this for initialization
    void Start()
    {
        _prevPos = transform.localPosition;
        _prevScale = transform.localScale;
        _videoManager = GameObject.Find("VideoManager").GetComponent<VideoManagerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnMouseEnter()
    {
        print("MouseEnter!");
        if (!_videoManager.IsPlaying && _videoManager.IsEnabled)
        {
            GetComponentInChildren<AudioSource>().volume = _videoManager.Volume;
            GetComponentInChildren<VideoController>().Play();
        }
    }

    void OnMouseExit()
    {
        print("MouseExit!");
        if (!_videoManager.IsPlaying && _videoManager.IsEnabled)
        {
            GetComponentInChildren<VideoController>().Stop();
        }
    }

    void OnMouseDown()
    {
        print("OnMouseDown()");
        if (_videoManager.IsEnabled)
        {
            if (!_videoManager.IsPlaying)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
    }

    public void Play()
    {
        Vector3 Distance = new Vector3(DISPLAY_POS.x - _prevPos.x, DISPLAY_POS.y - _prevPos.y, DISPLAY_POS.z - _prevPos.z);
        transform.localScale = DISPLAY_SCALE;
        transform.Translate(Vector3.Lerp(transform.localPosition, Distance, 1f), Space.Self);
        _videoManager.PlayingVideo = this.gameObject;
        GetComponentInChildren<AudioSource>().volume = _videoManager.Volume;
        GetComponentInChildren<VideoController>().Play();
        _videoManager.IsPlaying = true;

    }

    public void Stop()
    {
        GetComponentInChildren<VideoController>().Stop();
        transform.localScale = _prevScale;
        transform.localPosition = _prevPos;
        _videoManager.IsPlaying = false;
    }

    void OnMouseUp()
    {
        print("OnMouseUp()");
    }
}
