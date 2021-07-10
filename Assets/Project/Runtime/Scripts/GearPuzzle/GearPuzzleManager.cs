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

        public Gear StartGear => _startGear.Gear;
        public Gear EndGear => _endGear.Gear;


        public void CheckedCompletion()
        {
            bool isComplete = EndGear.IsPowered;
            if (isComplete)
            {
                PuzzleCompleted?.Invoke(isComplete);
                On_Camera_Transition();
                _cameraTransition.LockCamera(isComplete);
                AudioScript._instance.PlaySoundEffect("Gear Spin");
            }
        }

        void On_Drag(GameObject dragObject)
        {
            List<Gear> poweredGears = _startGear.PowerNextGear();
            poweredGears.Add(StartGear);
            // Searches all gears inside _gears list, checks against current list of poweredGears and returns all non-powered gears
            var unPoweredGears = _gears.Where(ps => poweredGears.All(gs => gs != ps)); 
            foreach (var gear in unPoweredGears)
            {
                gear.IsPowered = false;
            }
            CheckedCompletion();
        }

        void On_Camera_Transition()
        {
            CameraController.Camera.orthographic = !CameraController.Camera.orthographic;
        }
        void On_Play_Sound(GameObject gameObject)
        {
            AudioScript._instance.PlaySoundEffect("Gears");
        }

        void SubToEvents(bool subscribe)
        {
            DragObjectHandler.DragStarted -= On_Drag;
            DragObjectHandler.DragEnded -= On_Drag;
            DragObjectHandler.DragEnded -= On_Play_Sound;
            _cameraTransition.CameraEntered -= On_Camera_Transition;

            if (subscribe)
            {
                DragObjectHandler.DragStarted += On_Drag;
                DragObjectHandler.DragEnded += On_Drag;
                DragObjectHandler.DragEnded += On_Play_Sound;
                _cameraTransition.CameraEntered += On_Camera_Transition;
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
        void Awake()
        {
            StartGear.gameObject.GetComponent<DragObjectHandler>().Enabled = false;
            EndGear.gameObject.GetComponent<DragObjectHandler>().Enabled = false;
            DragObjectHandler.Parent = _gearsParent;
        }
    }
}
