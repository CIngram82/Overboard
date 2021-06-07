using System;
using System.Collections.Generic;
using UnityEngine;

public class GearSocket : MonoBehaviour
{
    [Serializable]
    public struct Connection
    {
        [SerializeField] GearSocket _from;
        [SerializeField] GearSocket _to;
        [SerializeField] float _distance;

        public GearSocket From => _from;
        public GearSocket To => _to;
        public float Distance => _distance;

        public Connection(GearSocket from, GearSocket to, float distance)
        {
            _from = from;
            _to = to;
            _distance = distance;
        }
    }

    Gear _gear;

    public List<Connection> Connections;
    public Gear Gear
    {
        get
        {
            if (!_gear)
            {
                if (transform.childCount > 0)
                {
                    return _gear = GetComponentInChildren<Gear>();
                }
                return null;
            }
            return _gear;
        }
    }


    public void PowerNextGear()
    {
        foreach (var socket in Connections)
        {
            // if current is powered and next socket has a gear.
            if (Gear.IsPowered && socket.To.Gear != null)
            {
                // if gears fit together
                if ((socket.To.Gear.Radius + Gear.Radius) == socket.Distance)
                {
                    socket.To.Gear.IsPowered = true;
                    socket.To.Gear.Direction = Gear.Direction * -1;
                    if (socket.Distance != 0)
                        socket.To.PowerNextGear();
                }
            }
            else
            if (socket.To.Gear != null)
            {
                socket.To.Gear.IsPowered = false;
            }
        }
    }

 
}





