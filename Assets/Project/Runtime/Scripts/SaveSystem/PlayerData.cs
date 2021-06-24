using UnityEngine;

namespace SaveSystem.Data
{
    [System.Serializable]
    public class PlayerData
    {
        /**/
        public SerialVector3 Position;
        public SerialQuaternion Rotation;

        public PlayerData(Transform transform)
        {
            Position = new SerialVector3(transform.position);
            Rotation = new SerialQuaternion(transform.rotation);
        }
        /**/
    }

    [System.Serializable]
    public class SerialVector3
    {
        public float x, y, z;

        public SerialVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }
        public Vector3 Get()
        {
            return new Vector3(x, y, z);
        }
    }
    [System.Serializable]
    public class SerialQuaternion
    {
        public float x, y, z, w;

        public SerialQuaternion(Quaternion quaternion)
        {
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
            w = quaternion.w;
        }
        public Quaternion Get()
        {
            return new Quaternion(x, y, z, w);
        }
    }
}
