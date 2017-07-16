using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayController : MonoBehaviour
{

    Vector3 _prevPos;
    Vector3 _prevScale;
    VideoManagerController _videoManager;


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


    // マウスカーソルが対象オブジェクトに進入した時にコールされる
    void OnMouseEnter()
    {
        print("MouseEnter!");
        if (!_videoManager.IsPlaying && _videoManager.IsEnabled)
        {
            GetComponentInChildren<AudioSource>().volume = _videoManager.Volume;
            GetComponentInChildren<VideoController>().Play();
        }
    }

    // マウスカーソルが対象オブジェクトから退出した時にコールされる
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
        Vector3 Distance = new Vector3(0 - _prevPos.x, 1 - _prevPos.y, -9.1f - _prevPos.z);
        transform.localScale = new Vector3(1.6f, 0.9f, 1f);
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
