using System.Collections.Generic;
using System.Linq;
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

    void On_Drag(GameObject dragObject)
    {
        List<Gear> poweredGears = _startGear.PowerNextGear();
        poweredGears.Add(StartGear);
        var unPoweredGears = _gears.Where(ps => poweredGears.All(gs => gs != ps));
        foreach (var gear in unPoweredGears)
        {
            gear.IsPowered = false;
        }
    }

    void SubToEvents(bool subscribe)
    {
        DragObjectHandler.DragStarted -= On_Drag;
        DragObjectHandler.DragEnded -= On_Drag;

        if (subscribe)
        {
            DragObjectHandler.DragStarted += On_Drag;
            DragObjectHandler.DragEnded += On_Drag;
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
        On_Drag(default);
    }
    void Awake()
    {
        StartGear.gameObject.GetComponent<DragObjectHandler>().enabled = false;
        EndGear.gameObject.GetComponent<DragObjectHandler>().enabled = false;
    }
}





