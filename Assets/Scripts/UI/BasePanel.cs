using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    //淡入淡出的速度
    private float alphaSpeed=10;
    public bool isShow=false;
    private UnityAction hideCallBack=null;

    protected virtual void Awake()
    {
        canvasGroup=this.GetComponent<CanvasGroup>();
        if(canvasGroup == null) {
            canvasGroup=this.gameObject.AddComponent<CanvasGroup>();
        }
    }
    public abstract void Init();

    public virtual void ShowMe()
    {
        canvasGroup.alpha = 0;
        isShow = true;
    }
    public virtual void HideMe(UnityAction callback)
    {
        canvasGroup.alpha = 1;
        isShow = false;
        hideCallBack = callback;
    }

    protected virtual void Start()
    {
        Init();
    }

    void Update()
    {
        if (isShow==true&&canvasGroup.alpha!=1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha>=1)
            {
                canvasGroup.alpha=1;
            }
        }else if (isShow == false&&canvasGroup.alpha!=0)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                hideCallBack?.Invoke();
            }
        }

    }
}
