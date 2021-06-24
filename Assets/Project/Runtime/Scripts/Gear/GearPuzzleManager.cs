using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GearPuzzle
{
    public class GearPuzzleManager : MonoBehaviour
    {
        public static System.Action<bool> PuzzleCompleted;

        [SerializeField] CameraTransition _cameraTransition;
        [SerializeField] GameObject _missingGear;

        [SerializeField] Transform _gearsParent;
        [SerializeField] GearSocket _startGear;
        [SerializeField] GearSocket _endGear;
        [SerializeField] List<Gear> _gears;
        [SerializeField] List<GearSocket> _gearSocket;

        //public bool IsSolved { get; private set; }
        public Gear StartGear => _startGear.Gear;
        public Gear EndGear => _startGear.Gear;


        public void CheckedCompletion()
        {
            PuzzleCompleted?.Invoke(EndGear.IsPowered);
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
            CheckedCompletion();
        }

        void On_Game_Enter(GameObject player)
        {
            if (player.TryGetComponent(out Inventory.Inventory inventory))
            {
                if (inventory.Items.Contains(inventory.ItemDatabase.GetInventoryItem("Gear")))
                    _missingGear.SetActive(true);
            }
        }

        void SubToEvents(bool subscribe)
        {
            DragObjectHandler.DragStarted -= On_Drag;
            DragObjectHandler.DragEnded -= On_Drag;
            _cameraTransition.CameraEntered -= On_Game_Enter;

            if (subscribe)
            {
                DragObjectHandler.DragStarted += On_Drag;
                DragObjectHandler.DragEnded += On_Drag;
                _cameraTransition.CameraEntered += On_Game_Enter;
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
            StartGear.gameObject.GetComponent<DragObjectHandler>().Enabled = false;
            EndGear.gameObject.GetComponent<DragObjectHandler>().Enabled = false;
            DragObjectHandler.Parent = _gearsParent;
        }
    }
}
