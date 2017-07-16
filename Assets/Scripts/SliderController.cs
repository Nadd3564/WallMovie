using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour {

    [SerializeField]
    public GameObject _videoManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnSlide()
    {
        _videoManager.GetComponent<VideoManagerController>().Volume 
            = GetComponent<Slider>().value;
    }
}
