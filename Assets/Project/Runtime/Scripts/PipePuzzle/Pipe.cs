using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PipePuzzle
{
    public class Pipe : MonoBehaviour
    {
        public int X { get; set; }
        public int Y { get; set; }

        #region pathfinding
        public int gCost;
        public int hCost;
        public int fCost;

        public Pipe previousNode;

        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }
        #endregion


        // Set exits in inspector 1 for exit 0 for no exit.
        [Header("N,E,S,W; 1 open 0 close")]
        public int[] exits;
        [SerializeField]
        private float rotationSpeed = 0.1f;
        [SerializeField]
        private float rotationDirection = 0;

        [SerializeField]
        private bool canTurn = true;
        private bool isRotating = false;

        private PipeGameManager pipeGM;
        [HideInInspector]
        public Pipe north, south, east, west;
        private void Start()
        {
            pipeGM = FindObjectOfType<PipeGameManager>();
        }

        private void Update()
        {
            if (transform.rotation.eulerAngles.z != rotationDirection && isRotating)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotationDirection), rotationSpeed);
            }
            else if (isRotating)
            {
                isRotating = false;
                pipeGM.CheckConnections();
            }
        }

        private void OnMouseDown()
        {
            if (isRotating) return;
            RotatePipe(); 
        }
        public void RotatePipe()
        {
            if (!canTurn) return;

            // set the new direction the image will turn to.
            rotationDirection += 90;
            if (rotationDirection == 360)
            {
                rotationDirection = 0;
            }
            // update the exit values to match the image. 
            var temp = exits[0];
            for (int i = 0; i < exits.Length - 1; i++)
            {
                exits[i] = exits[i + 1];
            }
            exits[exits.Length - 1] = temp;

        }
        public static void MakeEastWestNeighbors(Pipe east, Pipe west)
        {
            Debug.Assert(
                west.east == null && east.west == null, "Redefined neighbors!"
            );
            west.east = east;
            east.west = west;
        }
        public static void MakeNorthSouthNeighbors(Pipe north, Pipe south)
        {
            Debug.Assert(
                south.north == null && north.south == null, "Redefined neighbors!"
            );
            south.north = north;
            north.south = south;
        }

    }
}