using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PipePuzzle
{
    public class Pathfinding
    {
        // A* pathfinding https://en.wikipedia.org/wiki/A*_search_algorithm
        // The files Pathfinding, pathNode are both used for A* check the wiki for more details. 

        private const int MOVE_COST = 10;
        private PFGrid<Pipe> grid;
        private List<Pipe> openList;
        private List<Pipe> closedList;


        public Pathfinding(PFGrid<Pipe> puzzle)
        {
            grid = puzzle;
        }

        public List<Pipe> FindPath(int startX, int startY, int endX, int endY)
        {
            // set up the open and closed lists used for tracking nodes as we search
            Pipe startNode = grid.GetObject(startX, startY);
            Pipe endNode = grid.GetObject(endX, endY);
            openList = new List<Pipe> { startNode };
            closedList = new List<Pipe>();

            // loop over and set up all nodes to be ready for the search
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    Pipe pathNode = grid.GetObject(x, y);
                    pathNode.gCost = int.MaxValue;
                    pathNode.CalculateFCost();
                    pathNode.previousNode = null;
                }
            }

            // set up the starting node. 
            startNode.gCost = 0;
            startNode.hCost = CalculateDistanceCost(startNode, endNode);
            startNode.CalculateFCost();

            while (openList.Count > 0)
            {
                Pipe currentNode = GetLowestFCostNode(openList);
                if (currentNode == endNode)
                {
                    return CalculatePath(endNode);
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach (Pipe neighbourNode in GetNeighborsList(currentNode))
                {
                    if (closedList.Contains(neighbourNode)) continue;

                    int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                    if (tentativeGCost < neighbourNode.gCost)
                    {
                        neighbourNode.previousNode = currentNode;
                        neighbourNode.gCost = tentativeGCost;
                        neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                        neighbourNode.CalculateFCost();

                        if (!openList.Contains(neighbourNode))
                        {
                            openList.Add(neighbourNode);
                        }
                    }
                }
            }
            // out of nodes on the open list. No path was found. 
            return null;
        }

        private List<Pipe> GetNeighborsList(Pipe currentNode)
        {
            // Only adding orthogonal neighbors Check and see if both pipes have an opening facing each other. 
            List<Pipe> neighborHood = new List<Pipe>();
            if (currentNode.Y + 1 < grid.Height )  // North
            {
                Pipe other = GetNode(currentNode.X, currentNode.Y + 1);
                if(currentNode.exits[0] == 1 && other.exits[2] == 1)
                neighborHood.Add(other);
            }
            if (currentNode.X + 1 < grid.Width) // East
            {
                Pipe other = GetNode(currentNode.X + 1, currentNode.Y);
                if (currentNode.exits[1] == 1 && other.exits[3] == 1)
                neighborHood.Add(other);
            }
            if (currentNode.Y - 1 >= 0 && currentNode.exits[2] == 1) // south
            {
                Pipe other = GetNode(currentNode.X, currentNode.Y - 1);
                if (currentNode.exits[2] == 1 && other.exits[0] == 1)
                neighborHood.Add(other);
            }
            if (currentNode.X - 1 >= 0 && currentNode.exits[3] == 1) // West
            {
                Pipe other = GetNode(currentNode.X - 1, currentNode.Y);
                if (currentNode.exits[3] == 1 && other.exits[1] == 1)
                neighborHood.Add(other);
            }
            return neighborHood;
        }
        private Pipe GetNode(int x, int y)
        {
            return grid.GetObject(x, y);
        }
        private List<Pipe> CalculatePath(Pipe endNode)
        {
            List<Pipe> path = new List<Pipe>();
            path.Add(endNode);
            Pipe currentNode = endNode;

            while (currentNode.previousNode != null)
            {
                path.Add(currentNode.previousNode);
                currentNode = currentNode.previousNode;
            }
            path.Reverse();
            return path;
        }
        // Using the Manhattan distance heuristic for A*
        private int CalculateDistanceCost(Pipe a, Pipe b)
        {
            int dx = Mathf.Abs(a.X - b.X);
            int dy = Mathf.Abs(a.Y - b.Y);
            return MOVE_COST * (dx * dy);
        }

        /// <summary>
        /// Loops over a list of PathNodes and finds the one with the lowest F cost. 
        /// </summary>
        /// <param name="pathNodes">A list of pathNodes</param>
        /// <returns>the PathNode with the lowest F cost</returns>
        private Pipe GetLowestFCostNode(List<Pipe> pathNodes)
        {
            Pipe lowestF = pathNodes[0];
            for (int i = 1; i < pathNodes.Count; i++)
            {
                if (pathNodes[i].fCost < lowestF.fCost)
                {
                    lowestF = pathNodes[i];
                }
            }
            return lowestF;
        }
    }
}
