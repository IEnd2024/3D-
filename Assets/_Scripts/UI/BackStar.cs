using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class BackStar : MonoBehaviour
{
    public Image t;
    public RawImage start;
    public RawImage loading;
    public VideoPlayer loading_Video;
    public VideoPlayer start_Video;
    public AudioSource stay;
    private void Start()
    {
        t.enabled = false;
        start.enabled = true;
        start_Video.enabled = true;
        loading.enabled = false;
        loading_Video.enabled = false;
        start_Video.loopPointReached += Loading;
        stay.Stop();
    }

    private void Loading(VideoPlayer source)
    {
        
        start_Video.enabled = false;
        loading.enabled = true;
        loading_Video.enabled = true;
        Invoke("LoadingOver", 4f);
    }
    private void LoadingOver()
    {
        start.enabled = false;
        loading.enabled = false;
        loading_Video.enabled = false;
        stay.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "shop")
        {
            t.enabled = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "shop")
        {
            t.enabled = false;

        }

    }
}
    
    
