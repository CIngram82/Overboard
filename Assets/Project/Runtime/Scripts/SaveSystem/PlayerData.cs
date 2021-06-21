using UnityEngine;

namespace SaveSystem.Data
{
    [System.Serializable]
    public class PlayerData
    {
        public Transform Transform;

        public PlayerData(Transform transform)
        {
            Transform = transform;
        }
        public PlayerData()
        {
        }
    }
}
