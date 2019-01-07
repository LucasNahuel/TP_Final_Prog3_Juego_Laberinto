using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using IAgent;
namespace Laberinto
{
    //nuevo diseño full hd 4k
    public partial class FormAgente : Form
    {
        Celda[][] laberinto;
        Celda entrada;
        Celda jugador;
        Celda salida;
        int altoDeCelda;
        int anchoDeCelda;
        ISolverAgent agente;

        public FormAgente(Celda[][] laberinto, Celda entrada, Celda salida)
        {
            agente = new MiAgente();

            this.entrada = entrada;
            jugador = entrada;
            this.salida = salida;
            this.laberinto = laberinto;

            InitializeComponent();

            altoDeCelda = (panel1.Height / laberinto.Length) - 1;
            anchoDeCelda = (panel1.Width / laberinto.Length) - 1;
            panel1.BackColor = Color.Black;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int y = 0; y < laberinto.Length; y++)
            {

                for (int x = 0; x < laberinto.Length; x++)
                {
                    //dibuja un rectangulo en cada celda
                    Rectangle r = new Rectangle(x * anchoDeCelda, y * altoDeCelda, anchoDeCelda, altoDeCelda);
                    g.DrawRectangle(Pens.Lime, r);
                }
            }

            for (int y = 0; y < laberinto.Length; y++)
            {

                for (int x = 0; x < laberinto.Length; x++)
                {
                    Celda actual = laberinto[y][x];
                    //borra los muros de las paredes de los vecinos accesibles
                    foreach (Celda v in actual.vecinos)
                    {

                        if (v.y == actual.y)
                        {
                            if (v.x < actual.x)
                            {
                                //linea derecha
                                g.DrawLine(Pens.Black, x * anchoDeCelda, y * altoDeCelda, x * anchoDeCelda, (y * altoDeCelda) + altoDeCelda);
                            }
                            else
                            {
                                //linea izquierda
                                g.DrawLine(Pens.Black, (x * anchoDeCelda) + anchoDeCelda, y * altoDeCelda, (x * anchoDeCelda) + anchoDeCelda, (y * altoDeCelda) + altoDeCelda);
                            }
                        }

                        if (v.x == actual.x)
                        {
                            if (v.y < actual.y)
                            {
                                //linea arriba
                                g.DrawLine(Pens.Black, x * anchoDeCelda, y * altoDeCelda, (x * anchoDeCelda) + anchoDeCelda, y * altoDeCelda);
                            }
                            else
                            {
                                //linea abajo
                                g.DrawLine(Pens.Black, x * anchoDeCelda, (y * altoDeCelda) + altoDeCelda, (x * anchoDeCelda) + anchoDeCelda, (y * altoDeCelda) + altoDeCelda);
                            }
                        }
                    }
                }
            }
            g.DrawLine(Pens.Black, entrada.x * anchoDeCelda, entrada.y * altoDeCelda, entrada.x * anchoDeCelda, (entrada.y * altoDeCelda) + altoDeCelda);
            g.DrawLine(Pens.Black, (salida.x * anchoDeCelda) + anchoDeCelda, salida.y * altoDeCelda, (salida.x * anchoDeCelda) + anchoDeCelda, (salida.y * altoDeCelda) + altoDeCelda);

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            System.Windows.Point[] vecinos = new System.Windows.Point[jugador.vecinos.Count];

            for (int i = 0; i < jugador.vecinos.Count; i++)
            {
                vecinos[i] = new System.Windows.Point(jugador.vecinos.ElementAt(i).x, jugador.vecinos.ElementAt(i).y);
            }



            System.Windows.Point siguiente = pasarArgsAlAgente(vecinos);
            

            Rectangle r = new Rectangle((jugador.x * anchoDeCelda) + 3, (jugador.y * altoDeCelda) + 3, altoDeCelda - 6, altoDeCelda - 6);
            Graphics g = panel1.CreateGraphics();
            g.DrawEllipse(Pens.Black, r);

            jugador = laberinto[(int)siguiente.Y][(int)siguiente.X];

            r = new Rectangle((jugador.x * anchoDeCelda) + 3, (jugador.y * altoDeCelda) + 3, altoDeCelda - 6, altoDeCelda - 6);
            g.DrawEllipse(Pens.Red, r);

            if(jugador.y ==salida.y && jugador.x == salida.x)
            {
                timer1.Enabled = false;
                
                MessageBox.Show("el agente ha ganado");
                this.Dispose();
                
            }
        }

        private System.Windows.Point pasarArgsAlAgente(System.Windows.Point[] p)
        {
            System.Windows.Point siguiente=new System.Windows.Point();

            switch (p.Length)
            {
                case 4: siguiente = agente.moveAgent(p[0], p[1], p[2], p[3]);break;
                case 3: siguiente = agente.moveAgent(p[0], p[1], p[2]); break;
                case 2: siguiente = agente.moveAgent(p[0], p[1]); break;
                case 1: siguiente = agente.moveAgent(p[0]); break;
            }
            return siguiente;
        }

        
    }
}
