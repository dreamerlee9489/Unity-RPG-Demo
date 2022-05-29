using System.IO;
using UnityEngine;
using LitJson;

namespace App.Manager
{
    public enum JsonType { LitJson, JsonUtlity }

    public class JsonManager
    {
        private static JsonManager instance = new JsonManager();
        public static JsonManager Instance => instance;
        private JsonManager() { }

        public void SaveData(object data, string fileName, JsonType type = JsonType.LitJson)
        {
            switch (type)
            {
                case JsonType.LitJson:
                    File.WriteAllText(Application.persistentDataPath + "/" + fileName + ".json", JsonMapper.ToJson(data));
                    return;
                case JsonType.JsonUtlity:
                    File.WriteAllText(Application.persistentDataPath + "/" + fileName + ".json", JsonUtility.ToJson(data));
                    return;
            }
        }

        public T LoadData<T>(string fileName, JsonType type = JsonType.LitJson) where T : class, new()
        {
            switch (type)
            {
                case JsonType.LitJson:
                    if (File.Exists(Application.persistentDataPath + "/" + fileName + ".json"))
                        return JsonMapper.ToObject<T>(File.ReadAllText(Application.persistentDataPath + "/" + fileName + ".json"));
                    else if(File.Exists(Application.streamingAssetsPath + "/" + fileName + ".json"))
                        return JsonMapper.ToObject<T>(File.ReadAllText(Application.streamingAssetsPath + "/" + fileName + ".json"));
                    return null;
                case JsonType.JsonUtlity:
                    if (File.Exists(Application.persistentDataPath + "/" + fileName + ".json"))
                        return JsonUtility.FromJson<T>(File.ReadAllText(Application.persistentDataPath + "/" + fileName + ".json"));
                    else if(File.Exists(Application.streamingAssetsPath + "/" + fileName + ".json"))
                        return JsonUtility.FromJson<T>(File.ReadAllText(Application.streamingAssetsPath + "/" + fileName + ".json"));
                    return null;
                default:
                    return null;
            }
        }
    }
}
