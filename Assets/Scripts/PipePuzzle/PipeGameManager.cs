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

        public Grid<Pipe> puzzle;
        private List<Pipe> allPipes;

        // Start is called before the first frame update
        void Start()
        {
            firstOutputPowered = false;
            secondOutputPowered = false;
            thirdOutputPowered = false;
            forthOutputPowered = false;

            allPipes = new List<Pipe>( FindObjectsOfType<Pipe>() );
            SetUpGrid();
            SetUpPuzzle();
            MakeNeighbors();
            Shuffle();

        }


        private void SetUpPuzzle()
        {
            foreach (var item in allPipes)
            {
                int x = (int)item.transform.position.x;
                int y = (int)item.transform.position.y;
                puzzle.gridArray[x, y] = item.GetComponent<Pipe>();
            }
        }

        private void SetUpGrid()
        {
            Vector2 size = new Vector2(0, 0);
            Vector2 pos = new Vector2(0, 0);
            foreach (var pipe in allPipes)
            {
                if (pipe.transform.position.x > size.x)
                {
                    size.x = pipe.transform.position.x;
                }
                if (pipe.transform.position.y > size.y)
                {
                    size.y = pipe.transform.position.y;
                }
                if (pipe.transform.position.x < pos.x)
                {
                    pos.x = pipe.transform.position.x;
                }
                if (pipe.transform.position.y < pos.y)
                {
                    pos.y = pipe.transform.position.y;
                }
            }
            puzzle = new Grid<Pipe>((int)size.x, (int)size.y, allPipes[0].transform.localScale.x, pos);
        }

        private void MakeNeighbors()
        {
            for (int y = 0; y < puzzle.Height; y++)
            {
                for (int x = 0; x < puzzle.Width; x++)
                {
                    Pipe tile = puzzle.gridArray[x, y];
                    if (x > 0)
                    {
                        Pipe.MakeEastWestNeighbors(tile, puzzle.gridArray[x - 1, y]);
                    }
                    if (y > 0)
                    {
                        Pipe.MakeNorthSouthNeighbors(tile, puzzle.gridArray[x, y - 1]);
                    }
                }
            }
        }

        private void Shuffle()
        {
            foreach (var pipe in puzzle.gridArray)
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