using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PipePuzzle
{
    public class PathNode
    {
        private Grid<PathNode> grid;
        public int X { get; private set; }
        public int Y { get; private set; }

        public int gCost;
        public int hCost;
        public int fCost;


        public PathNode previousNode;
        public PathNode(Grid<PathNode> grid, int x, int y)
        {
            this.grid = grid;
            this.X = x;
            this.Y = y;
        }
        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }
    }
}