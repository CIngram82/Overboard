using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearsController : MonoBehaviour
{
    [SerializeField] GearSocket _startGear;
    [SerializeField] GearSocket _endGear;
    bool _isSolved = false;

    public Gear StartGear => _startGear.Gear;
    public Gear EndGear => _startGear.Gear;


    public bool CheckedPuzzleState()
    {
        return _isSolved = EndGear.IsPowered;
    }

    #region Mono
    void Update()
    {

    }

    void Start()
    {

    }
    private void Awake()
    {
        StartGear.gameObject.GetComponent<DragObjectHandler>().enabled = false;
        EndGear.gameObject.GetComponent<DragObjectHandler>().enabled = false;
    }
    #endregion
}





