using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroWorld
{
    class ModelGame
    {
        private bool[,] field;
        private readonly int cols;
        private readonly int rows;

        public ModelGame(int rows, int cols, int density)
        {
            this.rows = rows;
            this.cols = cols;
            field = new bool[cols, rows];
            Random random = new Random();
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    field[x, y] = random.Next(density) == 0;
                }
            }
        }
        public void NextGen()
        {
            var newField = new bool[cols, rows];

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    var countNeighbors = Count(x, y);
                    var hasLife = field[x, y];
                    if (!hasLife && countNeighbors == 3)
                    {
                        newField[x, y] = true;
                    }
                    else if (hasLife && (countNeighbors < 2 || countNeighbors > 3))
                    {
                        newField[x, y] = false;
                    }
                    else
                    {
                        newField[x, y] = field[x, y];
                    }
                }
            }
            field = newField;
        }
        public bool[,] GetGen()
        {
            var result = new bool[cols, rows];
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    result[x, y] = field[x, y];
                }
            }
            return result;
        }
        private int Count(int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var col = (x + i + cols) % cols;
                    var row = (y + j + rows) % rows;
                    var cecking = col == x && row == y;
                    var hasLife = field[col, row];
                    if (hasLife && !cecking)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private bool Validate(int x, int y)
        {
            return x >= 0 && y >= 0 && x < cols && y < rows;
        }
        private void UpdateCell(int x, int y, bool state)
        {
            if (Validate(x, y)) 
                field[x, y] = state;
        }
        public void AddCell(int x, int y)
        {
            UpdateCell(x, y, state: true);
        }
        public void RemoveCell(int x, int y)
        {
            UpdateCell(x, y, state: false);
        }
    }
}
