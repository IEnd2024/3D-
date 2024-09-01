using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using LitJson;
using System.IO;

public enum JsonType
{
    JsonUtlity,
    LitJson,
}

public class JsonManager
{
    private static JsonManager instance;
    public static JsonManager Instance=>instance;

    private JsonManager() { }

    //��������
    public void SaveData(object data, string fileName,JsonType type = JsonType.LitJson)
    {
        //ȷ���洢·��
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        //���л��õ�son�ַ���
        string jsonstr = ""; 
        switch (type)
        {
            case JsonType.JsonUtlity:
                jsonstr=JsonUtility.ToJson(data);
                break;
            case JsonType.LitJson:
                jsonstr = JsonMapper.ToJson(data);
                break;
        }
        //�����л���Json�ַ����洢��ָ��·�����ļ���
        File.WriteAllText(path, jsonstr);
    }

    //��ȡ����
    public T LoadData<T>(string fileName,JsonType type = JsonType.LitJson) where T : new()
    {
        //ȷ�����ĸ�·����ȡ
        //�������ж�Ĭ�������ļ������Ƿ���������Ҫ����������оʹ��л�ȡ
        string path = Application.streamingAssetsPath + "/" + fileName + ".json";
        //���ж��Ƿ��������ļ�
        //���������Ĭ���ļ��ʹӶ�д�ļ�����ȥѰ��
        if(!File.Exists(path))
        {
            path = Application.persistentDataPath + "/" + fileName + ".json";
        }
        //�����д�ļ����ж���û���Ǿͷ���һ��Ĭ�϶���
        if (!File.Exists(path))
        {
            return new T();
        }
        //���з����л�
        string jsonstr = File.ReadAllText(path);
        T data=default(T);
        switch (type)
        {
            case JsonType.JsonUtlity:
                data = JsonUtility.FromJson<T>(jsonstr);
                break;
            case JsonType.LitJson:
                data = JsonMapper.ToObject<T>(jsonstr);
                break;
        }
        // �Ѷ��󷵻س�ȥ
        return default(T);

        }




    }
