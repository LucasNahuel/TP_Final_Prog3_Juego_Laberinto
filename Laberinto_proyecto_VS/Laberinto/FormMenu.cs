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
    public partial class FormMenu : Form
    {
        

        public FormMenu()
        {
            InitializeComponent();
            Program.dimension = trackBar1.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.mdj = ModoDeJuego.JUGADORVSAGENTE;
            this.Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.mdj = ModoDeJuego.CONTRARRELOJ;
            this.Close();
            

        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (trackBar1.Value % 10 > 0)
            {
                trackBar1.Value = (trackBar1.Value + 10) - (trackBar1.Value % 10);
            }
            Program.dimension = trackBar1.Value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }
    }
}
