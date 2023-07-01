using System;
using System.Data;

namespace OughtsAndCrosses
{
    public class Field
    {
        public char PlayersSign { get; }
        public char BotsSign { get; }
        private int _activeRow, _activeColumn;
        char[,] _field;
        public char this[int row, int column]
        {
            get
            {
                if (row < 0 || row > 2)
                {
                    throw new ArgumentException("No such row");
                }
                if (column < 0 || column > 2)
                {
                    throw new ArgumentException("No such column");
                }
                return _field[row, column];
            }
            set
            {
                if (row < 0 || row > 2)
                {
                    throw new ArgumentException("No such row");
                }
                if (column < 0 || column > 2)
                {
                    throw new ArgumentException("No such column");
                }
                if (_field[row, column] != ' ')
                {
                    throw new ArgumentException("The cell is not empty");
                }
                _field[row, column] = IsPlayersMove ? PlayersSign : BotsSign;
                IsPlayersMove = !IsPlayersMove;

            }
        }
        public bool IsPlayersMove { get; private set; }

        public Field(char playersSign, char botsSign)
        {
            IsPlayersMove = true;
            PlayersSign = playersSign;
            BotsSign = botsSign;
            _field = new char[3, 3];
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    _field[i, j] = ' ';
                }
            }
            _activeRow = 1;
            _activeColumn = 1;
        }

        public int ActiveRow
        {
            get
            {
                return _activeRow;
            }
            set
            {
                if (value < 0 || value > 2)
                {
                    throw new ArgumentException("No such row");
                }
                _activeRow = value;
            }
        }
        public int ActiveColumn
        {
            get
            {
                return _activeColumn;
            }
            set
            {
                if (value < 0 || value > 2)
                {
                    throw new ArgumentException("No such column");
                }
                _activeColumn = value;
            }
        }

        /// <summary>
        /// Checks if someone won.
        /// </summary>
        /// <returns>Array of 3 x-coordinates and array of 3 y-coordinates of cells with the victory line if it exists, 
        /// otherwise two arrays of three -1.</returns>
        public (int[], int[]) Check()
        {
            if (this[0, 0] == this[1, 1] && this[1, 1] == this[2, 2] && this[0, 0] != ' ')
            {
                return (new int[] { 0, 1, 2 }, new int[] { 0, 1, 2 });
            } else if (this[0, 2] == this[1, 1] && this[1, 1] == this[2, 0] && this[0, 2] != ' ')
            {
                return (new int[] { 2, 1, 0 }, new int[] { 0, 1, 2 });
            }
            else
            {
                for (int i = 0; i < 3; ++i)
                {
                    if (this[i, 0] == this[i, 1] && this[i, 1] == this[i, 2] && this[i, 0] != ' ')
                    {
                        return (new int[] { i, i, i }, new int[] { 0, 1, 2 });
                    }
                }

                for (int j = 0; j < 3; ++j)
                {
                    if (this[0, j] == this[1, j] && this[1, j] == this[2, j] && this[0, j] != ' ')
                    {
                        return (new int[] { 0, 1, 2 }, new int[] { j, j, j });
                    }
                }
                return (new int[] { -1, -1, -1 }, new int[] { -1, -1, -1 });
            }
        }
    }
}
