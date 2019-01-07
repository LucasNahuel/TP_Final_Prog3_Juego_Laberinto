using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Laberinto
{
    class MiAgente : IAgent.ISolverAgent
    {
        Stack<Point> recorrido = new Stack<Point>();
        List<Point> visitados = new List<Point>();


        Point actual;

        bool primeraVez = true;

        public System.Windows.Point moveAgent(params System.Windows.Point[] directions)
        {

            if (primeraVez)
            {
                actual = directions[0];
                visitados.Add(actual);
                recorrido.Push(actual);
                primeraVez = false;
                return actual;
            }
            else
            {

                if (directions.Contains(Derecha()) && !visitados.Contains(Derecha())){
                    actual = Derecha();
                }
                else
                {
                    if (directions.Contains(Abajo()) && !visitados.Contains(Abajo()))
                    {
                        actual = Abajo();
                    }
                    else
                    {
                        if (directions.Contains(Izquierda()) && !visitados.Contains(Izquierda()))
                        {
                            actual = Izquierda();
                        }
                        else
                        {
                            if (directions.Contains(Arriba()) && !visitados.Contains(Arriba()))
                            {
                                actual = Arriba();
                            }
                            else
                            {
                                actual = recorrido.Pop();
                                return actual;
                            }
                        }
                    }
                }

                recorrido.Push(actual);
                visitados.Add(actual);
                return actual;
            }
        }

        private Point Derecha()
        {
            return new Point(actual.X - 1, actual.Y);
        }

        private Point Izquierda()
        {
            return new Point(actual.X + 1, actual.Y );
        }

        private Point Abajo()
        {
            return new Point(actual.X,actual.Y+1 );
        }

        private Point Arriba()
        {
            return new Point(actual.X,actual.Y - 1  );
        }
    }
}