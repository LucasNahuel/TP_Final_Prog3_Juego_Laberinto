using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laberinto
{
    public class Celda
    {
        public int y, x;

        public List<Celda> adyacente = new List<Celda>();

        public List<Celda> vecinos = new List<Celda>();

        public bool visitado = false;

        public Celda(int y, int x)
        {
            this.y = y;
            this.x = x;
        }

        public override string ToString()
        {
            return x + "," + y;
        }
    }
}
