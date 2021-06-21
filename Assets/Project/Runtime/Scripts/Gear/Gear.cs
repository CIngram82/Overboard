using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GearPuzzle
{
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

        void On_Drag_Started(GameObject gear)
        {
            gear.GetComponent<Gear>().IsPowered = false;
        }

        void SubToEvents(bool subscribe)
        {
            DragObjectHandler doh = GetComponent<DragObjectHandler>();
            doh.ObjectPickedUp -= On_Drag_Started;

            if (subscribe)
            {
                doh.ObjectPickedUp += On_Drag_Started;
            }
        }

        void OnEnable()
        {
            SubToEvents(true);
        }
        void OnDisable()
        {
            SubToEvents(false);
        }
        void Update()
        {
            if (IsPowered)
                RotateGear();
        }
    }
}
