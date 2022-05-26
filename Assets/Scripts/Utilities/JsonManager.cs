using System.IO;
using UnityEngine;
using LitJson;

namespace App.Utilities
{
    public enum JsonType { LitJson, JsonUtlity }

    public class JsonManager
    {
        private static JsonManager instance = new JsonManager();
        public static JsonManager Instance => instance;
        private JsonManager() { }

        public void SaveData(object data, string fileName, JsonType type = JsonType.LitJson)
        {
            Debug.Log(Application.persistentDataPath);
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

        public T LoadData<T>(string fileName, JsonType type = JsonType.LitJson)
        {
            switch (type)
            {
                case JsonType.LitJson:
                    if (File.Exists(Application.persistentDataPath + "/" + fileName + ".json"))
                        return JsonMapper.ToObject<T>(File.ReadAllText(Application.persistentDataPath + "/" + fileName + ".json"));
                    else
                        return JsonMapper.ToObject<T>(File.ReadAllText(Application.streamingAssetsPath + "/" + fileName + ".json"));
                case JsonType.JsonUtlity:
                    if (File.Exists(Application.persistentDataPath + "/" + fileName + ".json"))
                        return JsonUtility.FromJson<T>(File.ReadAllText(Application.persistentDataPath + "/" + fileName + ".json"));
                    else
                        return JsonUtility.FromJson<T>(File.ReadAllText(Application.streamingAssetsPath + "/" + fileName + ".json"));
                default:
                    return default(T);
            }
        }
    }
}
