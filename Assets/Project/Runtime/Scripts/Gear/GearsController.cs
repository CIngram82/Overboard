using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearsController : MonoBehaviour
{
    [SerializeField] GearSocket _startGear;
    [SerializeField] GearSocket _endGear;
    [SerializeField] List<Gear> _gears;
    [SerializeField] List<GearSocket> _gearSocket;
    bool _isSolved = false;

    public Gear StartGear => _startGear.Gear;
    public Gear EndGear => _startGear.Gear;


    public bool CheckedPuzzleState()
    {
        return _isSolved = EndGear.IsPowered;
    }

    void On_Object_Received() => _startGear.PowerNextGear();

    void SubToEvents(bool subscribe)
    {
        DropObjectHandler.ObjectReceived -= On_Object_Received;

        if (subscribe)
        {
            DropObjectHandler.ObjectReceived += On_Object_Received;
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
    void Start()
    {
        On_Object_Received();
    }
    private void Awake()
    {
        StartGear.gameObject.GetComponent<DragObjectHandler>().enabled = false;
        EndGear.gameObject.GetComponent<DragObjectHandler>().enabled = false;
    }
}





