using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PipePuzzle
{
    public class PipeGameManager : MonoBehaviour
    {
        public static System.Action<bool> PuzzleCompleted;
        
        public bool isComplete;
        [SerializeField]private Pipe power;
        [SerializeField]private Pipe firstOutput;

        public PFGrid<Pipe> puzzle;
        private List<Pipe> allPipes;

        private Pathfinding pathfinding;

        // Start is called before the first frame update
        void Start()
        {
            isComplete = false;

            allPipes = new List<Pipe>( FindObjectsOfType<Pipe>() );
            SetUpGrid();
            SetUpPuzzle();
            MakeNeighbors();
            pathfinding = new Pathfinding(puzzle);
            do
            {
                Shuffle();
                CheckConnections();
            }
            while (isComplete);
        }

        private void SetUpPuzzle()
        {
            foreach (var item in allPipes)
            {
                int x = (int)item.transform.localPosition.x;
                int y = (int)item.transform.localPosition.y;
                item.GetComponent<Pipe>().X = x;
                item.GetComponent<Pipe>().Y = y;

                puzzle.gridArray[x, y] = item.GetComponent<Pipe>();
            }
        }

        private void SetUpGrid()
        {
            Vector2 size = new Vector2(0, 0);
            Vector2 pos = new Vector2(0, 0);
            foreach (var pipe in allPipes)
            {
                if (pipe.transform.localPosition.x > size.x)
                {
                    size.x = pipe.transform.localPosition.x;
                }
                if (pipe.transform.localPosition.y > size.y)
                {
                    size.y = pipe.transform.localPosition.y;
                }
                if (pipe.transform.localPosition.x < pos.x)
                {
                    pos.x = pipe.transform.localPosition.x;
                }
                if (pipe.transform.localPosition.y < pos.y)
                {
                    pos.y = pipe.transform.localPosition.y;
                }
            }
            puzzle = new PFGrid<Pipe>((int)size.x+1, (int)size.y+1, pos);
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

        public void CheckConnections()
        {
            List<Pipe> pathOne = pathfinding.FindPath(power.X, power.Y, firstOutput.X, firstOutput.Y);
            if (null == pathOne)
            {
                foreach (var pipe in puzzle.gridArray)
                {
                    // Replace with changing out sprites or object for empty pipe 
                    pipe.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                }
                isComplete = false;
            }
            else
            {
                foreach (Pipe pipe in pathOne)
                {
                    // Replace with changing out sprites or object for full pipe 
                    pipe.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                }
                isComplete = true;
                PuzzleCompleted?.Invoke(isComplete);
            }
        }
    }
}