using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

// this is basic for now, make other shapes that can be loaded. but for now make zombie/virus cells cull population
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
                    cell.InfectedLifeSpan = 5;
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

        



    }
}
