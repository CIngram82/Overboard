using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    [SerializeField] bool _isPowered;
    [SerializeField] float _radius;
    [SerializeField] float _speed;
    [Range(-1, 1)]
    [SerializeField] int _direction = 1;

    public bool IsPowered { get => _isPowered; set => _isPowered = value; }
    public float Radius => _radius;
    public float Speed => _speed;
    public int Direction { get => _direction; set => _direction = value; }


    void RotateGear()
    {
        transform.Rotate(Vector3.down, _speed * Direction * Time.deltaTime);
    }

    #region Mono
    void Update()
    {
        if (IsPowered)
            RotateGear();
    }

    void Start()
    {

    }
    #endregion
}





