using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class frmMain : Form
    {
        Cells cell = new Cells();
        private static int cellsWide = 75;
        private static int cellsHigh = 75;
        private static int cellWidth = 9;
        private static int cellHeight = 9;
        private Cells[,] boardState = new Cells[cellsWide, cellsHigh];
        private Cells[,] tempState = new Cells[cellsWide, cellsHigh];
        private Cells[,] CleanBoard = new Cells[cellsWide, cellsHigh];
        int infectedLifeSpan = 5;
        bool startFrame = true;
        bool pauseLife = false;
        bool gameOutOfMoves = false;

        public frmMain()
        {
            InitializeComponent();
        }

        private void pbxGoLBoard_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            if (startFrame == true)
            {
                DrawCells(g);
                DeadState(g);
                RandomState(g);
                startFrame = false;
            }
            else
            {
                CheckSurroundingState();
                ReDrawCells(g);
            }
            RunLife();
        }

        private void RunLife()
        {
            if (pauseLife == false)
                this.Refresh();
        }

        public void DrawCells(Graphics g)
        {
            for (int y = 0; y < cellsHigh; y++)
            {
                for (int x = 0; x < cellsWide; x++)
                {
                    int xpos = x * 10 + 10;
                    int ypos = y * 10 + 10;
                    boardState[x, y] = cell.Cell(g, xpos, ypos, cellWidth, cellHeight);
                }
            }
            CleanBoard = boardState; 
            tempState = CleanBoard;
        }

        public void ReDrawCells(Graphics g)
        {
            for (int y = 0; y < cellsHigh; y++)
            {
                for (int x = 0; x < cellsWide; x++)
                {
                    boardState[x, y] = cell.CellState(g, tempState[x, y], tempState[x, y].IsAlive, tempState[x, y].IsInfected);
                }
            }
        }

        public void DeadState(Graphics g)
        {
            for (int y = 0; y < cellsHigh; y++)
            {
                for (int x = 0; x < cellsWide; x++)
                {
                    boardState[x, y] = cell.CellState(g, boardState[x, y], false);
                }
            }
        }

        public void RandomState(Graphics g)
        {
            Random rn = new Random();
            for (int y = 0; y < cellsHigh; y++)
            {
                for (int x = 0; x < cellsWide; x++)
                {
                    boardState[x, y] = cell.CellRandomState(g, boardState[x, y], rn);
                }
            }
        }

        public void GiveLifeToRandomCell(Graphics g)
        {
            Random rn = new Random();
            int randomY = rn.Next(0, cellsHigh - 1);
            int randomX = rn.Next(0, cellsWide - 1);
            boardState[randomX, randomY] = cell.CellRandomState(g, boardState[randomX, randomY], rn);
        }

        public void CheckSurroundingState()
        {
            Random infectChance = new Random();

            for (int y = 0; y < cellsHigh; y++)
            {
                for (int x = 0; x < cellsWide; x++)
                {
                    int liveCellCount = 0;
                    foreach (var adjacent in cell.GetSurroundingCells())
                    {
                        int adjacentX = x + adjacent.Item1;
                        int adjacentY = y + adjacent.Item2;

                        if (cell.CheckIfCellIsInBounds(adjacentX, adjacentY, cellsWide, cellsHigh))
                        {
                            if ((bool)boardState[adjacentX, adjacentY].IsAlive)
                            {
                                liveCellCount++;
                            }
                        }
                    }
                    if (boardState[x, y].IsInfected == true)
                    {
                        tempState[x, y].IsInfected = boardState[x, y].IsInfected;
                        --tempState[x, y].InfectedLifeSpan;
                        if (tempState[x, y].InfectedLifeSpan <= 0)
                        {
                            tempState[x, y].IsInfected = false;
                        }
                        foreach (var adjacent in cell.GetSurroundingCells()) // make a dead zone around the virus cell 
                        {
                            int adjacentX = x + adjacent.Item1;
                            int adjacentY = y + adjacent.Item2;

                            if (cell.CheckIfCellIsInBounds(adjacentX, adjacentY, cellsWide, cellsHigh)) 
                            {
                                if ((bool)boardState[adjacentX, adjacentY].IsAlive)
                                {
                                    tempState[adjacentX, adjacentY].IsAlive = false;
                                }
                            }
                        }
                        continue;
                    }
                    else
                    {
                        tempState[x, y] = CellCountRule(boardState[x, y], tempState[x, y], liveCellCount, infectChance);
                        liveCellCount = 0;
                    }
                }
            }
        }

        private Cells CellCountRule(Cells current, Cells temp, int otherCellsState, Random infectChance)
        {
            if (current.IsInfected == false)
            {
                if (otherCellsState == 0 && current.IsAlive == true || otherCellsState == 1 && current.IsAlive == true)
                {
                    temp.IsAlive = false;
                }
                else if (otherCellsState == 2 && current.IsAlive == true || otherCellsState == 3 && current.IsAlive == true)
                {
                    temp.IsAlive = true;
                }
                else if (otherCellsState > 3 && current.IsAlive == true)
                {
                    temp.IsAlive = false;
                    temp = cell.InfectCell(current, infectChance, infectedLifeSpan);
                }
                else if (otherCellsState == 3 && current.IsAlive == false)
                {
                    temp.IsAlive = true;
                }
            }
            return temp;
        }
        
        private void btnPauseLife_Click(object sender, EventArgs e)
        {
            pauseLife = pauseLife == false ? true : false;
            RunLife();
        }

    }
   
}
