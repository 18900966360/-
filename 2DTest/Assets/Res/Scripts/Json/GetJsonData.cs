using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using LitJson;

public class GetJsonData : MonoBehaviour
{
    // Start is called before the first frame update
    public static GunsState LoadJsonFromFile(string path)
    {

        BinaryFormatter bf = new BinaryFormatter();
        if(!File.Exists(Application.dataPath + "/Res/Json/"+path))
        {
            return null;
        }

        StreamReader sr = new StreamReader(Application.dataPath + "/Res/Json/"+path);
        if(sr == null) 
        {
            return null;
        }

        string json =sr.ReadToEnd();
        
        if(json.Length > 0)
        {
            return JsonUtility.FromJson<GunsState>(json);
        }
        return null;
    }
    public static JsonData JsonFileToPerson(string path)
    {
        TextAsset text = Resources.Load<TextAsset>(path); // 以文本方式加载

        LitJson.JsonData Data = JsonMapper.ToObject<JsonData>(text.text);
        return Data;
    }
}
