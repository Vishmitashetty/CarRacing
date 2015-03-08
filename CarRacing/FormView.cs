using System;
using System.Drawing;
using System.Windows.Forms;

namespace CarRacing
{
    public partial class FormView : Form
    {
        #region Fields

        private int _col;

        private int _elementSize;
        private int[,] _gameMatrix;
        private int _row;
        //the start point of board
        private int _startX;
        private int _startY;
        //the position of car in time
        private int _carX;
        private int _carY;

        private Random _random;
        private int _myCarPosition;

        #endregion

        public FormView()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            _row = 16;
            _col = 6;
            _startX = 50;
            _startY = 50;
            _elementSize = 15;

            _carX = _carY = 0;
            _gameMatrix = new int[_row, _col];
            ResetGameBoard();

            _random = new Random();
            _myCarPosition = 0;
            DrawACar(12, _myCarPosition, 2);
        }

        private void FormView_Paint(object sender, PaintEventArgs e)
        {
            DrawGameBoard(e.Graphics);
        }

        private void DrawGameBoard(Graphics g)
        {
            for (int i = 0; i < _row; i++)
            {
                for (int j = 0; j < _col; j++)
                {
                    g.DrawRectangle(new Pen(Brushes.Brown), _startX + j * _elementSize,
                                    _startY + i * _elementSize, _elementSize, _elementSize);
                    if (_gameMatrix[i, j] == 1)
                    {
                        g.FillRectangle(Brushes.DarkCyan, _startX + j * _elementSize,
                                        _startY + i * _elementSize, _elementSize, _elementSize);
                    }
                    if (_gameMatrix[i, j] == 2)
                    {
                        g.FillRectangle(Brushes.DeepSkyBlue, _startX + j * _elementSize,
                                        _startY + i * _elementSize, _elementSize, _elementSize);
                    }
                }
            }
        }

        #region Functions

        private void ResetGameBoard()
        {
            for (int i = 0; i < _row; i++)
            {
                for (int j = 0; j < _col; j++)
                {
                    _gameMatrix[i, j] = 0;
                }
            }
        }

        private void DrawACar(int x, int y, int value)
        {
            DrawAPoint(x, y + 1, value);
            DrawAPoint(x + 1, y + 1, value);
            DrawAPoint(x + 2, y + 1, value);
            DrawAPoint(x + 3, y + 1, value);
            DrawAPoint(x + 1, y, value);
            DrawAPoint(x + 1, y + 2, value);
            DrawAPoint(x + 3, y, value);
            DrawAPoint(x + 3, y + 2, value);
        }

        private void DrawAPoint(int x, int y, int value)
        {
            if (x < _row && x >= 0 && y < _col && y >= 0)
            {
                _gameMatrix[x, y] = value;
            }
        }

        #endregion

        private void tmrRacing_Tick(object sender, EventArgs e)
        {
            ResetGameBoard();
            DrawACar(12, _myCarPosition, 2);
            DrawACar(_carX, _carY, 1);
            Invalidate();

            _carX++;
            if (_carX == _row)
            {
                _carX = 0;
                _carY = _random.Next() % 2 == 0 ? 0 : 3;
            }

            CheckGame();
        }

        private void CheckGame()
        {
            if (_carX+3>12&&_carY==_myCarPosition)
            {
                tmrRacing.Enabled = false;
            }
        }

        private void FormView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Left&&_myCarPosition==3)
            {
                _myCarPosition = 0;
            }
            else if (e.KeyCode == Keys.Right && _myCarPosition == 0)
            {
                _myCarPosition = 3;
            }
        }

        private void FormView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _carX = _carY = 0;
            _myCarPosition = 0;
            tmrRacing.Enabled = true;
        }

        private void FormView_Load(object sender, EventArgs e)
        {

        }

    }
}