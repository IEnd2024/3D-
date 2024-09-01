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

    //保存数据
    public void SaveData(object data, string fileName,JsonType type = JsonType.LitJson)
    {
        //确定存储路径
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        //序列化得到son字符串
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
        //把序列化的Json字符串存储到指定路径的文件中
        File.WriteAllText(path, jsonstr);
    }

    //读取数据
    public T LoadData<T>(string fileName,JsonType type = JsonType.LitJson) where T : new()
    {
        //确定从哪个路径读取
        //首先先判断默认数据文件夹中是否有我们想要的数据如果有就从中获取
        string path = Application.streamingAssetsPath + "/" + fileName + ".json";
        //先判断是否存在这个文件
        //如果不存在默认文件就从读写文件夹中去寻找
        if(!File.Exists(path))
        {
            path = Application.persistentDataPath + "/" + fileName + ".json";
        }
        //如果读写文件夹中都还没有那就返回一个默认对象
        if (!File.Exists(path))
        {
            return new T();
        }
        //进行反序列化
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
        // 把对象返回出去
        return default(T);

        }




    }
