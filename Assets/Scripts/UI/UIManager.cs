using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager instance=new UIManager();
    public static UIManager Instance=>instance;
    private Dictionary<string,BasePanel> panelDic=new Dictionary<string,BasePanel>();
    private Transform canvasTrans;
    private bool isgameStart=false;

    private UIManager()
    {
        GameObject canvas=GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        canvasTrans = canvas.transform;
        GameObject.DontDestroyOnLoad(canvas);
    }
    public bool IsGameStart
    {
        get { return isgameStart; }
        set
        {
            isgameStart = value;
        }
    }       
    //显示面版
    public T ShowPanel<T>() where T : BasePanel
    {
        //需要保证预设体和泛型T名字一致
        string panelName=typeof(T).Name;
        if(panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/"+panelName));

        panelObj.transform.SetParent(canvasTrans,false);
        T panel=panelObj.GetComponent<T>();
        panelDic.Add(panelName, panel);
        panel.ShowMe();

        return panel;
    }
    //隐藏面版
    public void HidePanel<T>(bool isFade=true) where T : BasePanel
    {
        //根据泛型得名字
        string pane1Name = typeof(T).Name;
        //判断当前显示的面板有没有你想要隐藏的
        if (panelDic.ContainsKey(pane1Name)) 
        {
            if(isFade==true)
            {
                panelDic[pane1Name].HideMe(() =>
                {
                    //删除对象
                    GameObject.Destroy(panelDic[pane1Name].gameObject);
                    //删除字典里面存储的面板脚本
                    panelDic.Remove(pane1Name);
                });
            }
            else
            {
                //删除对象
                GameObject.Destroy(panelDic[pane1Name].gameObject);
                //删除字典里面存储的面板脚本
                panelDic.Remove(pane1Name); 
            }
            
        }
                                          
            
    }

    //得到面板
    public T GetPane1<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name; 
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        //如果没有对应面板显示就返回空    
        return null;
    }
}





