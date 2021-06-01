using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PipePuzzle
{
    public class PipeGameManager : MonoBehaviour
    {

        public bool firstOutputPowered;
        public bool secondOutputPowered;
        public bool thirdOutputPowered;
        public bool forthOutputPowered;

        public PipePuzzle puzzle;
        private GameObject[] allPipes;

        // Start is called before the first frame update
        void Start()
        {
            firstOutputPowered = false;
            secondOutputPowered = false;
            thirdOutputPowered = false;
            forthOutputPowered = false;

            allPipes = GameObject.FindGameObjectsWithTag("Pipe");
            SetUpPuzzle();
            MakeNeighbors();
            Shuffle();

        }

       
        private void SetUpPuzzle()
        {
            puzzle.pipes = new Pipe[puzzle.Width, puzzle.Height];
            foreach (var item in allPipes)
            {
                int x = (int)item.transform.position.x;
                int y = (int)item.transform.position.y;
                puzzle.pipes[x, y] = item.GetComponent<Pipe>();
            }
        }

        private void MakeNeighbors()
        {
            for (int y = 0; y < puzzle.Height; y++)
            {
                for (int x = 0; x < puzzle.Width; x++)
                {
                    Pipe tile = puzzle.pipes[x, y];
                    if (x > 0)
                    {
                        Pipe.MakeEastWestNeighbors(tile, puzzle.pipes[x - 1, y]);
                    }
                    if (y > 0)
                    {
                        Pipe.MakeNorthSouthNeighbors(tile, puzzle.pipes[x, y - 1]);
                    }
                }
            }
        }

        private void Shuffle()
        {
            foreach (var pipe in puzzle.pipes)
            {
                int rotAmount = Random.Range(0, 4);
                for (int i = 0; i < rotAmount; i++)
                {
                    pipe.RotatePipe();
                }
            }
        }


    }
}