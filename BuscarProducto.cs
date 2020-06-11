using PROYECTO_ULTIMA_UNIDAD.Mysql;
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
    public partial class BuscarProducto : Form
    {

        public BuscarProducto()
        {
            InitializeComponent();
        }
        public Producto productoSeleccionado { get; set; }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvBuscar.DataSource = Acciones.Buscar(txtBuscarParte.Text);
            }
            catch (Exception X)
            {
                MessageBox.Show("ERROR: " + X.Message);
            }
            foreach (Control X in gpbBuscar.Controls)
            {
                if (X is TextBox)
                {
                    X.ResetText();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAcp_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBuscar.SelectedRows.Count == 1)
                {
                    string NParte = Convert.ToString(dgvBuscar.CurrentRow.Cells[0].Value);
                    productoSeleccionado = Acciones.ObtenerProducto(NParte);
                    this.Close();
                }
                else { MessageBox.Show("Selecciona Una Fila"); }
            }
            catch (Exception X)
            {
                MessageBox.Show("ERROR: " + X.Message);
            }
        }

        private void dgvBuscar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
