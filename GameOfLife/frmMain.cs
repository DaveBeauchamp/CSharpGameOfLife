using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            CleanBoard = boardState; // clean board, yes it's overkill but having a clean board will make life easier
            tempState = CleanBoard;
        }

        public void ReDrawCells(Graphics g)
        {
            for (int y = 0; y < cellsHigh; y++)
            {
                for (int x = 0; x < cellsWide; x++)
                {
                    boardState[x, y] = cell.CellState(g, tempState[x, y], tempState[x, y].IsAlive);
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
                    tempState[x, y] = CellCountRule(boardState[x, y], tempState[x, y], liveCellCount, infectChance);
                    liveCellCount = 0;
                }
            }
        }

        private Cells CellCountRule(Cells current, Cells temp, int otherCellsState, Random infectChance)
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
                temp = InfectCell(temp, infectChance);

            }
            else if (otherCellsState == 3 && current.IsAlive == false)
            {
                temp.IsAlive = true;
            }
            return temp;
        }

        private Cells InfectCell(Cells cell, Random infectChance)
        {
            int ic = infectChance.Next(1, 26);
            if (ic == 3)
                cell.IsInfected = true;
            return cell;
        }

        //private Cells InfectAdjacentCells(Cells current, Cells temp, Random infectChance)
        //{
        //    // run this with the checks above somehow to infect adjacent cells
        //    //remember that they only live a short while

        //}

        private void btnPauseLife_Click_1(object sender, EventArgs e)
        {
            pauseLife = pauseLife == false ? true : false;
            RunLife();
        }

        // for testing just about anything
        private void btnTest_Click_1(object sender, EventArgs e)
        {

        }

        
    }


}
