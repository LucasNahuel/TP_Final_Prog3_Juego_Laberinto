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

namespace Laberinto
{
    //nuevo diseño full hd 4k
    public partial class Form2 : Form
    {
        Celda[][] laberinto;
        Celda entrada, jugador, salida;
        int altoDeCelda, anchoDeCelda, tiempo;
        bool esContrarreloj;

        FormAgente fa;

        public Form2(Celda[][] laberinto, Celda entrada, Celda salida, bool esContrarreloj)
        {

            
            this.entrada = entrada;
            jugador = entrada;
            this.salida = salida;
            this.laberinto = laberinto;
            this.esContrarreloj = esContrarreloj;



            
            
            InitializeComponent();
            if (esContrarreloj)
            {
                //establece el tiempo para resolver el laberinto
                tiempo = 64;
                //inicia el textbox
                label1.Visible = true;
                label1.Text = "Tiempo\nRestante: \n" + tiempo;
                //inicia el timer
                timer1.Enabled = true;
            }
            else
            {
                fa = new FormAgente(laberinto, entrada, salida);
                fa.Location = new Point(700, 0);
                fa.Show(this);
            }

            altoDeCelda = (panel1.Height / laberinto.Length) -1;
            anchoDeCelda = (panel1.Width / laberinto.Length) -1;
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

            for (int y=0; y<laberinto.Length; y++)
            {
                
                for (int x=0; x<laberinto.Length; x++)
                {
                    Celda actual = laberinto[y][x];
                    //borra los muros de las paredes de los vecinos accesibles
                    foreach(Celda v in actual.vecinos)
                    {

                        if (v.y == actual.y)
                        {
                            if (v.x < actual.x)
                            {
                                //linea derecha
                                g.DrawLine(Pens.Black, x * anchoDeCelda, y * altoDeCelda, x * anchoDeCelda , (y * altoDeCelda)+altoDeCelda);
                            }
                            else
                            {
                                //linea izquierda
                                g.DrawLine(Pens.Black, (x * anchoDeCelda)+anchoDeCelda, y * altoDeCelda, (x * anchoDeCelda) + anchoDeCelda, (y * altoDeCelda) + altoDeCelda);
                            }
                        }

                        if (v.x == actual.x)
                        {
                            if (v.y < actual.y)
                            {
                                //linea arriba
                                g.DrawLine(Pens.Black, x * anchoDeCelda, y * altoDeCelda, (x * anchoDeCelda)+anchoDeCelda, y * altoDeCelda);
                            }
                            else
                            {
                                //linea abajo
                                g.DrawLine(Pens.Black, x * anchoDeCelda, (y * altoDeCelda)+altoDeCelda, (x * anchoDeCelda) + anchoDeCelda, (y * altoDeCelda) + altoDeCelda);
                            }
                        }
                    }
                }
            }
            g.DrawLine(Pens.Black, entrada.x * anchoDeCelda, entrada.y * altoDeCelda, entrada.x * anchoDeCelda, (entrada.y * altoDeCelda) + altoDeCelda);
            g.DrawLine(Pens.Black, (salida.x * anchoDeCelda) + anchoDeCelda, salida.y * altoDeCelda, (salida.x * anchoDeCelda) + anchoDeCelda, (salida.y * altoDeCelda) + altoDeCelda);
            
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            Celda siguiente=null;

            switch (e.KeyCode)
            {
                case Keys.Right: siguiente = new Celda(jugador.y, jugador.x + 1);break;
                case Keys.Left: siguiente = new Celda(jugador.y, jugador.x - 1);break;
                case Keys.Up: siguiente = new Celda(jugador.y - 1, jugador.x);break;
                case Keys.Down: siguiente = new Celda(jugador.y + 1, jugador.x);break;
                default: return;
            }

            
                if((siguiente.y>=0 && siguiente.y < laberinto.Length)&& (siguiente.x >= 0 && siguiente.x < laberinto.Length))
                {
                    siguiente = laberinto[siguiente.y][siguiente.x];

                    if(jugador.vecinos.Contains(siguiente) || siguiente.vecinos.Contains(jugador))
                    {
                        
                        Rectangle r = new Rectangle((jugador.x * anchoDeCelda)+3, (jugador.y * altoDeCelda)+3, altoDeCelda-6, altoDeCelda-6);
                        Graphics g = panel1.CreateGraphics();
                        
                        g.DrawEllipse(Pens.Black, r);
                        jugador = siguiente;
                        r = new Rectangle((jugador.x * anchoDeCelda) + 3, (jugador.y * altoDeCelda) + 3, altoDeCelda - 6, altoDeCelda - 6);
                        g.DrawEllipse(Pens.Red, r);

                        if (salida.y == siguiente.y && salida.x == siguiente.x)
                        {
                            timer1.Enabled = false;
                            if (!esContrarreloj)
                            {
                                fa.Close();
                            }
                            MessageBox.Show("ganaste");
                            
                            this.Dispose();
                        }
                }
                }
            
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "Tiempo\nRestante: \n" + (tiempo--);
            if (tiempo == 0)
            {
                timer1.Enabled = false;
                MessageBox.Show("perdiste");
                this.Dispose();
            }
            
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
