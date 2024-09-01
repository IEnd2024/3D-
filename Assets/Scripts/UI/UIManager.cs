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
    //��ʾ���
    public T ShowPanel<T>() where T : BasePanel
    {
        //��Ҫ��֤Ԥ����ͷ���T����һ��
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
    //�������
    public void HidePanel<T>(bool isFade=true) where T : BasePanel
    {
        //���ݷ��͵�����
        string pane1Name = typeof(T).Name;
        //�жϵ�ǰ��ʾ�������û������Ҫ���ص�
        if (panelDic.ContainsKey(pane1Name)) 
        {
            if(isFade==true)
            {
                panelDic[pane1Name].HideMe(() =>
                {
                    //ɾ������
                    GameObject.Destroy(panelDic[pane1Name].gameObject);
                    //ɾ���ֵ�����洢�����ű�
                    panelDic.Remove(pane1Name);
                });
            }
            else
            {
                //ɾ������
                GameObject.Destroy(panelDic[pane1Name].gameObject);
                //ɾ���ֵ�����洢�����ű�
                panelDic.Remove(pane1Name); 
            }
            
        }
                                          
            
    }

    //�õ����
    public T GetPane1<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name; 
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        //���û�ж�Ӧ�����ʾ�ͷ��ؿ�    
        return null;
    }
}





