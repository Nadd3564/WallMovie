using UnityEngine;
using UnityEngine.UI;

//Enable/Disableボタン制御クラス
//    VideoManagerの管理フラグを制御する
public class BtnController : MonoBehaviour
{
    #region "Const"

    const string ENABLE = "Enable";
    const string DISABLE = "Disable";

    #endregion "Const"

    #region "Variant"

    private Text _text;
    private Image _image;
    private VideoManagerController _videoManager;

    #endregion "Variant"

    // Use this for initialization
    void Start()
    {
        _text = GetComponentInChildren<Text>();
        _text.text = "Disable";
        _image = GetComponent<Image>();
        _image.color = Color.gray;
        _videoManager = GameObject.Find("VideoManager").GetComponent<VideoManagerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActiveChange()
    {
        _videoManager.IsEnabled = _videoManager.IsEnabled ? false : true;
        if (_videoManager.IsEnabled)
        {
            _text.text = ENABLE;
            _image.color = Color.magenta;
        }
        else
        {
            _text.text = DISABLE;
            _image.color = Color.gray;
        }
    }
}
