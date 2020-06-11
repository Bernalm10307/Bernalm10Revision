using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROYECTO_ULTIMA_UNIDAD
{
    public partial class FRMINICIO : Form
    {
        public FRMINICIO()
        {
            InitializeComponent();
        }

        private void btnApariencia_Click(object sender, EventArgs e)
        {
            Form siguientepagina = new SEGUNDAPAGINA();
            siguientepagina.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FRMINICIO_Load(object sender, EventArgs e)
        {

        }
    }
}
