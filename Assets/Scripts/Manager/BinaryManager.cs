using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace App.Manager
{
    public class BinaryManager
    {
        static BinaryManager instance = null;
        static byte key = 123;
        public static string DATA_DIR = Application.persistentDataPath + "/Bin/";

        public static BinaryManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new BinaryManager();
                return instance;
            }
        }

        public void SaveData(object data, string fileName)
        {
            if (!Directory.Exists(DATA_DIR))
                Directory.CreateDirectory(DATA_DIR);
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, data);
                byte[] bytes = ms.GetBuffer();
                for (int i = 0; i < bytes.Length; i++)
                    bytes[i] ^= key;
                File.WriteAllBytes(DATA_DIR + fileName + ".bin", bytes);
            }
        }

        public T LoadData<T>(string fileName) where T : class
        {
            if (File.Exists(DATA_DIR + fileName + ".bin"))
            {
                byte[] bytes = File.ReadAllBytes(DATA_DIR + fileName + ".bin");
                for (int i = 0; i < bytes.Length; i++)
                    bytes[i] ^= key;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    return new BinaryFormatter().Deserialize(ms) as T;
                }
            }
            return default(T);
        }

        /// <summary>
        /// 加载Excel表
        /// </summary>
        /// <typeparam name="T1">表类型</typeparam>
        /// <typeparam name="T2">表容器类型</typeparam>
        public T2 LoadTable<T1, T2>() where T1 : class where T2 : class
        {
            string tableName = typeof(T1).Name;
            T2 table = Activator.CreateInstance(typeof(T2)) as T2;
            using (FileStream fs = File.Open(DATA_DIR + "Excel/" + tableName + ".table", FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);
                int index = 0;
                int keyLen = BitConverter.ToInt32(bytes, index);
                index += sizeof(int);//读取键名长度
                string keyName = Encoding.UTF8.GetString(bytes, index, keyLen);
                index += keyLen;//读取键名
                int rowCount = BitConverter.ToInt32(bytes, index);
                index += sizeof(int);//读取行数
                for (int i = 0; i < rowCount; i++)
                {
                    T1 obj = Activator.CreateInstance(typeof(T1)) as T1;
                    FieldInfo[] fields = typeof(T1).GetFields();
                    foreach (FieldInfo field in fields)
                    {
                        switch (field.FieldType.Name)
                        {
                            case "Int32":
                                obj.GetType().GetField(field.Name).SetValue(obj, BitConverter.ToInt32(bytes, index));
                                index += sizeof(int);
                                break;
                            case "Single":
                                obj.GetType().GetField(field.Name).SetValue(obj, BitConverter.ToSingle(bytes, index));
                                index += sizeof(float);
                                break;
                            case "Boolean":
                                obj.GetType().GetField(field.Name).SetValue(obj, BitConverter.ToBoolean(bytes, index));
                                index += sizeof(bool);
                                break;
                            case "String":
                                int strlen = BitConverter.ToInt32(bytes, index);
                                index += sizeof(int);
                                obj.GetType().GetField(field.Name).SetValue(obj, Encoding.UTF8.GetString(bytes, index, strlen));
                                index += strlen;
                                break;
                            default:
                                break;
                        }
                    }
                    //Invoke()传入的对象必须是调用者(字典对象)
                    object dic = table.GetType().GetField("tuples").GetValue(table);
                    dic.GetType().GetMethod("Add").Invoke(dic, new object[] { obj.GetType().GetField(keyName).GetValue(obj), obj });
                }
            }
            return table;
        }
    }
}