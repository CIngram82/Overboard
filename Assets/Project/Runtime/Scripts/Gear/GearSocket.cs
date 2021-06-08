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

    public List<Connection> ConnectionsFrom;
    public List<Connection> ConnectionsTo;
    public Gear Gear
    {
        get
        {
            if (transform.childCount > 0)
            {
                if (!_gear)
                    return _gear = GetComponentInChildren<Gear>();
                else
                    return _gear;
            }
            return _gear = null;
        }
    }


    public List<Gear> PowerNextGear()
    {
        List<Gear> poweredSockets = new List<Gear>();
        foreach (var socket in ConnectionsTo)
        {
            // if current is powered and next socket has a gear.
            if (socket.To.Gear != null)
                if (Gear.IsPowered)
                {
                    // if gears fit together
                    if ((socket.To.Gear.Radius + Gear.Radius) == socket.Distance)
                    {
                        socket.To.Gear.IsPowered = true;
                        socket.To.Gear.Direction = Gear.Direction * -1;
                        poweredSockets.Add(socket.To.Gear);
                        if (socket.Distance != 0)
                            poweredSockets.AddRange(socket.To.PowerNextGear());
                    }
                    //socket.To.Gear.IsPowered = false;
                }
        }
        return poweredSockets;
    }
}





