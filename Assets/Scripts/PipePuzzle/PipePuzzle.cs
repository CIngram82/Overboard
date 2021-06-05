using UnityEngine;

namespace PipePuzzle
{
    [System.Serializable]
    public class PipePuzzle 
    {
        private int _width = 5;
        private int _height = 5;
        public int Width
        {
            get { return _width; }
        }
        public int Height
        {
            get { return _height; }
        }

        public Pipe[,] pipes;
    }
}