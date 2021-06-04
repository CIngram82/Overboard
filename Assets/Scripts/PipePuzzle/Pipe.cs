using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PipePuzzle
{
    public class Pipe : MonoBehaviour
    {
        private Grid<Pipe> grid;
        public int X { get; private set; }
        public int Y { get; private set; }

        public int gCost;
        public int hCost;
        public int fCost;

        public Pipe previousNode;
        public Pipe(Grid<Pipe> grid, int x, int y)
        {
            this.grid = grid;
            this.X = x;
            this.Y = y;
        }
        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }

        // Set exits in inspector 1 for exit 0 for no exit.
        [Header("N,E,S,W; 1 open 0 close")]
        public int[] exits;
        [SerializeField]
        private float rotationSpeed = 0.1f;
        private float rotationDirection = 0;

        [SerializeField]
        private bool canTurn = true;
        [SerializeField]
        private Sprite emptySprite, filledSprite;
        private SpriteRenderer mySpriteRenderer;

        private PipeGameManager pipeGM;
        [HideInInspector]
        public Pipe north, south, east, west;
        private void Start()
        {
            mySpriteRenderer = GetComponent<SpriteRenderer>();
            pipeGM = FindObjectOfType<PipeGameManager>();

        }

        private void Update()
        {
            if (transform.rotation.eulerAngles.z != rotationDirection)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, rotationDirection), rotationSpeed);
            }
        }

        private void OnMouseDown()
        {
            if (canTurn)
            {
                RotatePipe();
            }
        }
        public void RotatePipe()
        {
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