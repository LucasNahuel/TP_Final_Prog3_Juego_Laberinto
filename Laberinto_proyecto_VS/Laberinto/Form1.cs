using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laberinto
{
    public partial class Form1 : Form
    {
        Celda[][] laberinto;
        public Form1(Celda[][] laberinto)
        {
            InitializeComponent();
            
            this.laberinto = laberinto;
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.ColumnCount = laberinto.Length;
            tableLayoutPanel1.RowCount = laberinto.Length;

            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();

            for (int i = 0; i < laberinto.Length; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, tableLayoutPanel1.Width / laberinto.Length));
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, tableLayoutPanel1.Height / laberinto.Length));
            }

            for (int y = 0; y < laberinto.Length; y++)
            {
                for (int x = 0; x < laberinto.Length; x++)
                {
                    Celda actual = laberinto[y][x];


                    Panel p = new Panel();
                    p.Margin = new Padding(0);

                    Label arriba = LabelArriba();
                    Label abajo = LabelAbajo();
                    Label izquierda = LabelIzquierda();
                    Label derecha = LabelDerecha();

                    p.Controls.Add(arriba);
                    p.Controls.Add(abajo);
                    p.Controls.Add(izquierda);
                    p.Controls.Add(derecha);

                    foreach (Celda v in actual.vecinos)
                    {

                        if (v.x == actual.x)
                        {
                            if (v.y < actual.y)
                            {
                                arriba.Dispose();
                            }
                            else
                            {
                                abajo.Dispose();
                            }
                        }

                        if (v.y == actual.y)
                        {
                            if (v.x < actual.x)
                            {
                                izquierda.Dispose();
                            }
                            else
                            {
                                derecha.Dispose();
                            }
                        }
                        
                    }
                    tableLayoutPanel1.Controls.Add(p, x, y);
                }
            }
            tableLayoutPanel1.ResumeLayout();
        }
        public Label CrearLabel()
        {
            Label l = new Label();
            l.Margin = new Padding(0);
            l.BackColor = Color.Black;
            return l;
        }

        private Label LabelArriba()
        {
            Label l = CrearLabel();
            l.Dock = DockStyle.Top;
            l.Height = 2;
            return l;
        }
        private Label LabelAbajo()
        {
            Label l = CrearLabel();
            l.Dock = DockStyle.Bottom;
            l.Height = 2;
            return l;
        }
        private Label LabelIzquierda()
        {
            Label l = CrearLabel();
            l.Dock = DockStyle.Left;
            l.Width = 2;
            return l;
        }
        private Label LabelDerecha()
        {
            Label l = CrearLabel();
            l.Dock = DockStyle.Right;
            l.Width = 2;
            return l;
        }
    }
}
