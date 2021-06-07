using System;
using System.Collections.Generic;
using UnityEngine;

public class GearSocket : MonoBehaviour
{
    [Serializable]
    public struct Connection
    {
        [SerializeField] GameObject _from;
        [SerializeField] GameObject _to;
        [SerializeField] float _distance;

        public GameObject From => _from;
        public GameObject To => _to;
        public float Distance => _distance;
        
        public Connection(GameObject to, GameObject from, float distance)
        {
            _from = from;
            _to = to;
            _distance = distance;
        }
    }
  
    [SerializeField]
    public List<Connection> Connections;

    public GameObject Gear
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
        set 
        {
            
        }
    }


    #region Mono
    void Update()
    {
        
    }

    void Start()
    {
        
    }
#endregion
}





