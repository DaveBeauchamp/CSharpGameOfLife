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
            //try make a pause function

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
            CleanBoard = boardState; // clean board, yes it's overkill but havinga clean board will make life easier
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
            int localCellsHigh = cellsHigh - 1;
            int localCellsWide = cellsWide - 1;
            
            tempState = CleanBoard;

            for (int y = 0; y < cellsHigh; y++)
            {
                for (int x = 0; x < cellsWide; x++)
                {
                    //basic
                    int cellLeft = x - 1;
                    int cellRight = x + 1;
                    int cellTop = y - 1;
                    int cellBottom = y + 1;
                    
                    int liveCellCount = 0;
                    int deadCellCount = 0;
                    

                    if (cellTop < 0 && cellLeft < 0) //top left edge case
                    {
                        if (boardState[localCellsWide, localCellsHigh].IsAlive == true) // top left is bottom right
                        {
                            liveCellCount++;
                        }
                        if (boardState[localCellsWide, y].IsAlive == true) // left is right side
                        {
                            liveCellCount++;
                        }
                        if (boardState[localCellsWide, cellBottom].IsAlive == true) // below the one above
                        {
                            liveCellCount++;
                        }
                        if (boardState[x, localCellsHigh].IsAlive == true) // top is at bottom of screen
                        {
                            liveCellCount++;
                        }
                        if (boardState[cellRight, localCellsHigh].IsAlive == true) // next to the one above
                        {
                            liveCellCount++;
                        }
                        if (boardState[cellRight, y].IsAlive == true)
                        {
                            // right cell
                            liveCellCount++;
                        }
                        if (boardState[cellRight, cellBottom].IsAlive == true)
                        {
                            // bottom right
                            liveCellCount++;
                        }
                        if (boardState[x, cellBottom].IsAlive == true)
                        {
                            // bottom cell
                            liveCellCount++;
                        }

                    }
                    else if (cellTop < 0 && cellRight > localCellsWide) //top right edge case, put this before top case to hopefully skip top if is edge
                    {

                        if (boardState[0, localCellsHigh].IsAlive == true) // top right is bottom left
                        {
                            liveCellCount++;
                        }
                        if (boardState[0, y].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[0, cellBottom].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[cellLeft, localCellsHigh].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[x, localCellsHigh].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        // don't check top right
                        if (boardState[cellLeft, y].IsAlive == true)
                        {
                            // left cell
                            liveCellCount++;
                        }
                        if (boardState[cellLeft, cellBottom].IsAlive == true)
                        {
                            // bottom Left
                            liveCellCount++;
                        }
                        if (boardState[x, cellBottom].IsAlive == true)
                        {
                            // bottom cell
                            liveCellCount++;
                        }

                    }
                    
                    else if (cellBottom > localCellsHigh && cellLeft < 0) // bottom left edge case
                    {
                        if (boardState[x, 0].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[cellRight, 0].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[localCellsWide, 0].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[localCellsWide, cellTop].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[localCellsWide, y].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        // don't check bottom left
                        if (boardState[x, cellTop].IsAlive == true)
                        {
                            // check top
                            liveCellCount++;
                        }
                        if (boardState[cellRight, cellTop].IsAlive == true)
                        {
                            // check top right
                            liveCellCount++;
                        }
                        if (boardState[cellRight, y].IsAlive == true)
                        {
                            // right cell
                            liveCellCount++;
                        }
                    }
                    else if (cellBottom > localCellsHigh && cellRight > localCellsWide) // bottom right edge case
                    {

                        if (boardState[0, 0].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[cellLeft, 0].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[x, 0].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[0, cellTop].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[0, y].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        // don't check bottom right
                        if (boardState[x, cellTop].IsAlive == true)
                        {
                            // check top
                            liveCellCount++;
                        }
                        if (boardState[cellLeft, cellTop].IsAlive == true)
                        {
                            // check top left
                            liveCellCount++;
                        }
                        if (boardState[cellLeft, y].IsAlive == true)
                        {
                            // left cell
                            liveCellCount++;
                        }
                    }
                    else if (cellLeft < 0) // left edge case
                    {
                        if (boardState[localCellsWide, cellTop].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[localCellsWide, y].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[localCellsWide, cellBottom].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        // don't check Left
                        if (boardState[x, cellTop].IsAlive == true)
                        {
                            // check top
                            liveCellCount++;
                        }
                        if (boardState[cellRight, cellTop].IsAlive == true)
                        {
                            // check top right
                            liveCellCount++;
                        }
                        if (boardState[cellRight, y].IsAlive == true)
                        {
                            // right cell
                            liveCellCount++;
                        }
                        if (boardState[cellRight, cellBottom].IsAlive == true)
                        {
                            // bottom right
                            liveCellCount++;
                        }
                        if (boardState[x, cellBottom].IsAlive == true)
                        {
                            // bottom cell
                            liveCellCount++;
                        }
                    }
                    else if (cellTop < 0) //top edge case // error throws below, reading from right to left this can read index thatare out of range
                    {
                        if (boardState[cellLeft, localCellsHigh].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[x, localCellsHigh].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[cellRight, localCellsHigh].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[cellLeft, y].IsAlive == true)
                        {
                            // left cell
                            liveCellCount++;
                        }
                        if (boardState[cellRight, y].IsAlive == true)
                        {
                            // right cell
                            liveCellCount++;
                        }
                        if (boardState[cellRight, cellBottom].IsAlive == true)
                        {
                            // bottom right
                            liveCellCount++;
                        }
                        if (boardState[x, cellBottom].IsAlive == true)
                        {
                            // bottom cell
                            liveCellCount++;
                        }
                        if (boardState[cellLeft, cellBottom].IsAlive == true)
                        {
                            // bottom Left
                            liveCellCount++;
                        }

                    }
                    else if (cellRight > localCellsWide) // right edge case
                    {
                        if (boardState[0, cellTop].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[0, y].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[0, cellBottom].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        // don't check right
                        if (boardState[x, cellTop].IsAlive == true)
                        {
                            // check top
                            liveCellCount++;
                        }
                        if (boardState[cellLeft, cellTop].IsAlive == true)
                        {
                            // check top left
                            liveCellCount++;
                        }
                        if (boardState[cellLeft, y].IsAlive == true)
                        {
                            // left cell
                            liveCellCount++;
                        }
                        if (boardState[cellLeft, cellBottom].IsAlive == true)
                        {
                            // bottom left
                            liveCellCount++;
                        }
                        if (boardState[x, cellBottom].IsAlive == true)
                        {
                            // bottom cell
                            liveCellCount++;
                        }
                    }
                    else if (cellBottom > localCellsHigh) // bottom edge case
                    {
                        if (boardState[cellLeft, 0].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[x, 0].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        if (boardState[cellRight, 0].IsAlive == true)
                        {
                            liveCellCount++;
                        }
                        // don't check bottom
                        if (boardState[x, cellTop].IsAlive == true)
                        {
                            // check top
                            liveCellCount++;
                        }
                        if (boardState[cellLeft, cellTop].IsAlive == true)
                        {
                            // check top left
                            liveCellCount++;
                        }
                        if (boardState[cellLeft, y].IsAlive == true)
                        {
                            // left cell
                            liveCellCount++;
                        }
                        if (boardState[cellRight, cellTop].IsAlive == true)
                        {
                            // check top right
                            liveCellCount++;
                        }
                        if (boardState[cellRight, y].IsAlive == true)
                        {
                            // right cell
                            liveCellCount++;
                        }
                    }
                    else 
                    {
                        // checks everything, will have to be a better way of doing this (I feel else ifs are coming)
                        if (boardState[x, cellTop].IsAlive == true)
                        {
                            // check top
                            liveCellCount++;
                            deadCellCount++;
                        }
                        if (boardState[cellLeft, cellTop].IsAlive == true)
                        {
                            // check top left
                            liveCellCount++;
                            deadCellCount++;
                        }
                        if (boardState[cellRight, cellTop].IsAlive == true)
                        {
                            // check top right
                            liveCellCount++;
                            deadCellCount++;
                        }
                        if (boardState[cellLeft, y].IsAlive == true)
                        {
                            // left cell
                            liveCellCount++;
                            deadCellCount++;
                        }
                        if (boardState[cellRight, y].IsAlive == true)
                        {
                            // right cell
                            liveCellCount++;
                            deadCellCount++;
                        }
                        if (boardState[cellRight, cellBottom].IsAlive == true)
                        {
                            // bottom right
                            liveCellCount++;
                            deadCellCount++;
                        }
                        if (boardState[x, cellBottom].IsAlive == true)
                        {
                            // bottom cell
                            liveCellCount++;
                            deadCellCount++;
                        }
                        if (boardState[cellLeft, cellBottom].IsAlive == true)
                        {
                            // bottom Left
                            liveCellCount++;
                            deadCellCount++;
                        }
                    }

                    // handle infected cell, to infect other cells
                    if (boardState[x, y].IsInfected == true) 
                    {
                        // checks everything, will have to be a better way of doing this (I feel else ifs are coming)
                        if (boardState[x, cellTop].IsAlive == true)
                        {
                            // check top
                            
                        }
                        if (boardState[cellLeft, cellTop].IsAlive == true)
                        {
                            // check top left
                            
                        }
                        if (boardState[cellRight, cellTop].IsAlive == true)
                        {
                            // check top right
                            
                        }
                        if (boardState[cellLeft, y].IsAlive == true)
                        {
                            // left cell
                            
                        }
                        if (boardState[cellRight, y].IsAlive == true)
                        {
                            // right cell
                            
                        }
                        if (boardState[cellRight, cellBottom].IsAlive == true)
                        {
                            // bottom right
                            
                        }
                        if (boardState[x, cellBottom].IsAlive == true)
                        {
                            // bottom cell
                            
                        }
                        if (boardState[cellLeft, cellBottom].IsAlive == true)
                        {
                            // bottom Left
                            
                        }
                    }

                    if (boardState[x, y].IsInfected == true)
                    {
                        boardState[x, y].InfectedLifeSpan++;
                        if (boardState[x, y].InfectedLifeSpan == 5)
                        {
                            boardState[x, y].IsAlive = false;
                            boardState[x, y].IsInfected = false;
                        }
                    }

                    // make a counter to count how many cells are alive
                    tempState[x, y] = CellCountRule(boardState[x, y], tempState[x, y], liveCellCount);
                    tempState[x, y] = InfectCell(boardState[x, y], tempState[x, y], infectChance, deadCellCount);   // infect current cell
                    // set back to false for next round
                    liveCellCount = 0;
                    deadCellCount = 0;
                }
            }
            
        }

        private Cells CellCountRule(Cells current, Cells temp, int otherCellsState)
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
            }
            else if (otherCellsState == 3 && current.IsAlive == false)
            {
                temp.IsAlive = true;
            }

            
            return temp;
        }

        private Cells InfectCell(Cells current, Cells temp, Random infectChance, int localDeadCellCount)
        {
            int ic = infectChance.Next(1, 26);

            if (localDeadCellCount == 8 && current.IsAlive == false)
            {
                if (ic == 3)
                {
                    temp.IsInfected = true;
                }
            }

            return temp;
            
        }

        //private Cells InfectAdjacentCells(Cells current, Cells temp, Random infectChance)
        //{
        //    // run this with the checks above somehow to infect adjacent cells
        //    //remember that they only live a short while

        //}




        // this is a test pause button
        private void btnTest_Click_1(object sender, EventArgs e)
        {
            if (pauseLife == false)
                pauseLife = true;
            else
                pauseLife = false;
        }


    }




}
