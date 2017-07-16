using UnityEngine;

public class SkipBtnController : MonoBehaviour
{

    VideoManagerController _videoManager;

    // Use this for initialization
    void Start()
    {
        _videoManager = GameObject.Find("VideoManager").GetComponent<VideoManagerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Skip()
    {
        _videoManager.Skip();
    }
}
