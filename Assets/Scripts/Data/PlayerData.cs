using UnityEngine;
using System.Collections.Generic;

namespace App.Data
{   
    [System.Serializable]
    public struct Vector
    {
        public float x, y, z, w;
        public Vector(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public Vector(Vector3 vector3)
        {
            x = vector3.x;
            y = vector3.y;
            z = vector3.z;
            w = 0;
        }
        public Vector(Quaternion quaternion)
        {
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
            w = quaternion.w;
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public string nickName = "冒险家";
        public int golds = 5000;
        public string sceneName = "";
        public Vector position;
        public Vector rotation;
        public List<ItemData> itemDatas = new List<ItemData>();
        public PlayerData() {}
    }
}
