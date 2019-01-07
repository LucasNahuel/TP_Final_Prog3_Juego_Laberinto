using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laberinto
{
    static class Program
    {
        static Random r = new Random();
        public static int dimension;
        static Celda entrada;
        static Celda salida;
        static public ModoDeJuego mdj;
        
        [STAThread]
        static void Main()
        {
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMenu());



            Celda[][] laberinto= new Celda[dimension][];

            for(int i=0; i<dimension; i++)
            {
                laberinto[i] = new Celda[dimension];
            }

            for(int y=0; y<dimension; y++)
            {
                for(int x=0; x<dimension; x++)
                {
                    laberinto[y][x] = new Celda(y, x);
                }
            }

            for (int y = 0; y < dimension; y++)
            {
                for (int x = 0; x < dimension; x++)
                {
                    AñadirAdyacentes(laberinto[y][x], laberinto);
                }
            }

            //selecciona una celda aleatoria para comenzar a armar el laberinto
            Celda inicio = laberinto[r.Next(0, dimension)][r.Next(0, dimension)];


            //genera el laberinto a partir de la celda de inicio
            GenerarLaberinto(inicio, laberinto);


            //elige un punto de entrada
            entrada = laberinto[r.Next(0, dimension)][0];
            //elige un punto de salida
            salida = laberinto[r.Next(0, dimension)][dimension-1];


            switch (mdj)
            {
                case ModoDeJuego.CONTRARRELOJ:
                    Form2 f2 = new Form2(laberinto, entrada, salida, true);
                    Application.Run(f2);break;
                case ModoDeJuego.JUGADORVSAGENTE:
                    Form2 f3 = new Form2(laberinto, entrada, salida, false);
                    Application.Run(f3);break;

            }

        }

        private static void AñadirAdyacentes(Celda c, Celda[][] laberinto)
        {
            if(c.y>0 && c.y < dimension - 1)
            {
                c.adyacente.Add(laberinto[c.y + 1][c.x]);
                c.adyacente.Add(laberinto[c.y - 1][c.x]);
            }
            else
            {
                if (c.y == 0)
                {
                    c.adyacente.Add(laberinto[c.y + 1][c.x]);
                }
                else
                {
                    c.adyacente.Add(laberinto[c.y - 1][c.x]);
                }
            }

            if (c.x > 0 && c.x < dimension - 1)
            {
                c.adyacente.Add(laberinto[c.y][c.x+1]);
                c.adyacente.Add(laberinto[c.y][c.x-1]);
            }
            else
            {
                if (c.x == 0)
                {
                    c.adyacente.Add(laberinto[c.y][c.x+1]);
                }
                else
                {
                    c.adyacente.Add(laberinto[c.y][c.x - 1]);
                }
            }
        }

        private static void GenerarLaberinto(Celda actual, Celda[][] laberinto)
        {
            actual.visitado = true;

            while (actual.adyacente.Count > 0)
            {
                int siguiente = r.Next(0, actual.adyacente.Count);
                Celda c = actual.adyacente[siguiente];
                actual.adyacente.RemoveAt(siguiente);

                if (!c.visitado)
                {
                    actual.vecinos.Add(c);
                    c.vecinos.Add(actual);
                    c.adyacente.Remove(actual);
                    GenerarLaberinto(c, laberinto);
                }
            }

        }



    }



}
