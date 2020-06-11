using EO.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PROYECTO_ULTIMA_UNIDAD.Mysql
{
    public partial class MostrarDatos : Form
    {
        public MostrarDatos()
        {
            InitializeComponent();
        }

        private void btnRegistros_Click(object sender, EventArgs e)
        {
            try
            {
                dgvRegistros.DataSource = Acciones.mostrar();
            }
            catch(Exception X)
            {
                MessageBox.Show("ERROR: " + X.Message);
            }
            
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
