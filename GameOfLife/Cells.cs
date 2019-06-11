using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameOfLife
{
    public class Cells
    {
        public Rectangle BoardCell { get; set; }
        public bool? IsAlive { get; set; }
        public bool? IsInfected { get; set; }
        public int InfectedLifeSpan { get; set; }

        public Cells Cell(Graphics g, int left, int top, int width, int height)
        {
            Cells cell = new Cells();
            using (Pen blackPen = new Pen(Color.Black))
            {
                cell.BoardCell = new Rectangle(left, top, width, height);
                cell.IsAlive = false;
                cell.IsInfected = false;
                g.DrawRectangle(blackPen, BoardCell);
                return cell;
            }
        }

        public Cells CellState(Graphics g, Cells cell, bool? DoA = null, bool? infect = null) // made this nullable to use in more places
        {
            if (DoA != null)
                cell.IsAlive = DoA;
            if (infect == null)
                cell.IsInfected = false;
            else if (infect == true)
            {
                cell.IsAlive = false;
                cell.IsInfected = true;
            }

            using (SolidBrush blueBrush = new SolidBrush(Color.Blue))
            using (SolidBrush whiteBrush = new SolidBrush(Color.White))
            using (SolidBrush greenBrush = new SolidBrush(Color.Green))
            {
                if (cell.IsAlive == true)
                {
                    g.FillRectangle(blueBrush, cell.BoardCell);
                    if (cell.IsAlive == true) cell.InfectedLifeSpan = 0;
                }
                else if (cell.IsInfected == true)
                {
                    g.FillRectangle(greenBrush, cell.BoardCell);
                }
                else
                {
                    g.FillRectangle(whiteBrush, cell.BoardCell);
                }
            }
            return cell;
        }

        public Cells CellRandomState(Graphics g, Cells cell, Random rn)
        {
            int random = rn.Next(1, 16);

            if (random == 1)
            {
                cell.IsAlive = true;
            }
            else
            {
                cell.IsAlive = false;
            }

            using (SolidBrush blueBrush = new SolidBrush(Color.Blue))
            using (SolidBrush whiteBrush = new SolidBrush(Color.White))
            {
                if (cell.IsAlive == true)
                {
                    g.FillRectangle(blueBrush, cell.BoardCell);
                }
                else
                {
                    g.FillRectangle(whiteBrush, cell.BoardCell);
                }
            }
            return cell;
        }

        public Cells InfectCell(Cells cell, Random infectChance, int infectedLifeSpan)
        {
            int ic = infectChance.Next(1, 26);
            if (ic == 3)
            {
                cell.IsInfected = true;
                cell.InfectedLifeSpan = infectedLifeSpan;
            }
            return cell;
        }

        public List<Tuple<int, int>> GetSurroundingCells()
        {
            List<Tuple<int, int>> surroundingCells = new List<Tuple<int, int>>();
            Tuple<int, int> topLeft = new Tuple<int, int>(-1, -1);
            Tuple<int, int> top = new Tuple<int, int>(-1, 0);
            Tuple<int, int> topRight = new Tuple<int, int>(-1, +1);
            Tuple<int, int> left = new Tuple<int, int>(0, -1);
            Tuple<int, int> right = new Tuple<int, int>(0, +1);
            Tuple<int, int> bottomLeft = new Tuple<int, int>(+1, -1);
            Tuple<int, int> bottom = new Tuple<int, int>(+1, 0);
            Tuple<int, int> bottomRight = new Tuple<int, int>(+1, +1);
            surroundingCells.Add(topLeft);
            surroundingCells.Add(top);
            surroundingCells.Add(topRight);
            surroundingCells.Add(left);
            surroundingCells.Add(right);
            surroundingCells.Add(bottomLeft);
            surroundingCells.Add(bottom);
            surroundingCells.Add(bottomRight);
            return surroundingCells;
        }

        public bool CheckIfCellIsInBounds(int xpos, int ypos, int cellsWide, int cellsHigh)
        {
            bool cellIsInBounds = false;
            if (xpos >= 0 && xpos < cellsWide && ypos >= 0 && ypos < cellsHigh)
            {
                cellIsInBounds = true;
            }
            return cellIsInBounds;
        }


    }
}
