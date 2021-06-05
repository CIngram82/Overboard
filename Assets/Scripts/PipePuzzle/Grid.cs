using System;
using System.Collections.Generic;
using UnityEngine;

namespace PipePuzzle
{
    [System.Serializable]
    public class Grid<Pipe>
    {

        public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
        public class OnGridValueChangedEventArgs : EventArgs
        {
            public int x;
            public int y;
        }

        private int width;
        private int height;
        private float cellSize;
        private Vector3 originPos;
        public Pipe[,] gridArray;

        public Grid(int width, int height, float cellSize, Vector3 originPos)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPos = originPos;
            this.gridArray = new Pipe[width, height];

        }
        public int Height
        {
            get { return height; }
        }
        public int Width
        {
            get { return width; }
        }
        public float CellSize
        {
            get { return cellSize; }
        }
        private Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * cellSize + originPos;
        }

        private Vector2Int GetXY(Vector3 worldPosition)
        {
            Vector2Int posXY = new Vector2Int();
            posXY.x = Mathf.FloorToInt((worldPosition.x - originPos.x)/ cellSize);
            posXY.y = Mathf.FloorToInt((worldPosition.y - originPos.y)/ cellSize);

            return posXY;
        }

        public void SetValue(int x, int y, Pipe value)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                gridArray[x, y] = value;
            }
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }

        public void TriggerGridObjectChanged(int x, int y)
        {
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }

        public void SetValue(Vector3 worldPosition, Pipe value)
        {
            Vector2Int posXY = GetXY(worldPosition);
            SetValue(posXY.x, posXY.y, value);
        }

        public Pipe GetObject(int x, int y)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                return gridArray[x, y];
            }
            else
            {
                return default;
            }
        }

        public Pipe GetObject(Vector3 worldPosition)
        {
            Vector2Int posXY = GetXY(worldPosition);
            return GetObject(posXY.x, posXY.y);
        }
    }
}
