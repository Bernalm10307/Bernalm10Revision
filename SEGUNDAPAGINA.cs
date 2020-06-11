using MySql.Data.MySqlClient;
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
    public partial class SEGUNDAPAGINA : Form
    {
        Producto miproducto = new Producto();
        ArchivoSecuencialSerializadoBinario<Producto> miarchivo = new ArchivoSecuencialSerializadoBinario<Producto>(@"C:\Archivos\ArchivoSeralizado.dat");
        public SEGUNDAPAGINA()
        {
            InitializeComponent();
            dgvArchivos.ReadOnly = true;
            dgvArchivos.AllowUserToAddRows = false;
            dgvArchivos.AllowUserToDeleteRows = false;
            dgvArchivos.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;

            dgvArchivos.Columns.Add("Numero de Parte", "Numero de Parte");
            dgvArchivos.Columns.Add("Nombre", "Nombre");
            dgvArchivos.Columns.Add("Modelo", "Modelo");
            dgvArchivos.Columns.Add("Cantidad", "Cantidad");
        }

        private void SEGUNDAPAGINA_Load(object sender, EventArgs e)
        {
            btnEliminar.Enabled = false;
            btnEditar.Enabled = false;
        }
        public Producto ProductoActual { get; set; }

        

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            miproducto = new Producto(txtNumeroParte.Text, txtNombre.Text, txtModelo.Text,int.Parse(txtCantidad.Text));
            try {
                int retorno = Acciones.Agregar(miproducto);
                if (retorno > 0) { MessageBox.Show("SE AGREGO CON EXITO."); }
                else { MessageBox.Show("NO SE AGREGO"); }
            }
            catch(Exception X) { MessageBox.Show("ERROR: " + X.Message); }
            try
            {
                miproducto.NumeroParte = txtNumeroParte.Text;
                miproducto.Nombre = txtNombre.Text;
                miproducto.Modelo = txtModelo.Text;
                miproducto.Cantidad = int.Parse(txtCantidad.Text);
            }
            catch (Exception X)
            {
                MessageBox.Show("ERROR: " + X.Message);
            }
            try
            {
                miarchivo.AbrirEnModoEscritura();
                miarchivo.GrabarObjeto(miproducto);
                MessageBox.Show("Datos almacenados correctamente");
            }
            catch (Exception X)
            {
                MessageBox.Show("ERROR: " + X.Message);
            }
            finally
            {
                miarchivo.Cerrar();
            }
            MostrarDatos();
            foreach (Control X in gpbDatosRegistro.Controls)
            {
                if(X is TextBox)
                {
                    X.ResetText();
                }
            }
        }
        public void MostrarDatos()
        {
            dgvArchivos.Rows.Clear();
            try
            {
                miarchivo.AbrirEnModoLectura();
                while (!miarchivo.FinArchivo)
                {
                    miproducto = miarchivo.LeerObjeto();
                    dgvArchivos.Rows.Add(miproducto.NumeroParte, miproducto.Nombre, miproducto.Modelo, miproducto.Cantidad);
                }
            }
            catch (Exception X)
            {
                MessageBox.Show("ERROR: " + X.Message);
            }
            finally
            {
                miarchivo.Cerrar();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                MostrarDatos mimostrador = new MostrarDatos();
                this.Hide();
                mimostrador.ShowDialog();
                this.Show();
            }
            catch(Exception X)
            {
                MessageBox.Show("ERROR: " + X.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                BuscarProducto mibuscador = new BuscarProducto();
                mibuscador.ShowDialog();
                if (mibuscador.productoSeleccionado != null)
                {
                    ProductoActual = mibuscador.productoSeleccionado;
                    txtNumeroParte.Text = ProductoActual.NumeroParte;
                    txtNombre.Text = ProductoActual.Nombre;
                    txtModelo.Text = ProductoActual.Modelo;
                    btnEliminar.Enabled = true;
                    btnEditar.Enabled = true;
                }
                this.Show();
            }
            catch(Exception X)
            {
                MessageBox.Show("ERROR: " + X.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int ProductoEliminado = Acciones.Eliminar(txtNumeroParte.Text);
                if (ProductoEliminado > 0)
                {
                    MessageBox.Show("Se Elimino el Producto con Exito.");
                }
                else
                {
                    MessageBox.Show("Eliminacion Fallida.");
                }
            }
            catch(Exception X)
            {
                MessageBox.Show("ERROR: " + X.Message);
            }
            string strNombreArchivoTemporal = @"C:\Archivos\ArchivoTemporal.tmp";
            ArchivoSecuencialSerializadoBinario<Producto> archivoTemporal = new ArchivoSecuencialSerializadoBinario<Producto>(strNombreArchivoTemporal);
            string _strNumeroParte = " ";
            if (dgvArchivos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un Producto de la lista");
                return;
            }
            try
            {
                _strNumeroParte = dgvArchivos.CurrentRow.Cells[0].Value.ToString();

            }
            catch (Exception X)
            {
                MessageBox.Show("ERROR: " + X.Message);
                return;
            }
            if (MessageBox.Show("¿Estas seguro de eliminar el producto con el Numero de Parte " + _strNumeroParte + " ?", "ELIMINAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            try
            {
                miarchivo.AbrirEnModoLectura();
                archivoTemporal.AbrirEnModoEscritura();
                while (true)
                {
                    miproducto = miarchivo.LeerObjeto();
                    if (miproducto.NumeroParte != _strNumeroParte)
                    {
                        archivoTemporal.GrabarObjeto(miproducto);
                    }
                }
            }
            catch (Exception X)
            {
                MessageBox.Show("ERROR: " + X.Message);
            }
            finally
            {
                miarchivo.Cerrar();
                archivoTemporal.Cerrar();
            }
            miarchivo.EliminarArchivo();
            miarchivo.RenombrarArchivo(strNombreArchivoTemporal);
            MostrarDatos();
            foreach (Control X in gpbDatosRegistro.Controls)
            {
                if(X is TextBox)
                {
                    X.ResetText();
                }
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            try
            {
                int ProductoEditado = Acciones.Editar(int.Parse(txtCantidad.Text), txtNumeroParte.Text);
                if (ProductoEditado > 0)
                {
                    MessageBox.Show("Se Edito el Producto con Exito.");
                }
                else
                {
                    MessageBox.Show("Edicion Fallida.");
                }
            }
            catch (Exception X)
            {
                MessageBox.Show("ERROR: " + X.Message);
            }
            // El objeto miproducto se suscribe al evento
            miproducto.PropertyChanged += new PropertyChangedEventHandler(MetodoGestor);
            miproducto.NumeroParte = txtNumeroParte.Text;
            miproducto.Nombre = txtNombre.Text;
            miproducto.Modelo = txtModelo.Text;
            miproducto.Cantidad = int.Parse(txtCantidad.Text);
            foreach (Control X in gpbDatosRegistro.Controls)
            {
                if (X is TextBox)
                {
                    X.ResetText();
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Método gestor ejecutado al modificar la Cantidad
        private void MetodoGestor(object sender, PropertyChangedEventArgs e)
        {
            MessageBox.Show("Se ha cambiado el valor de " + e.PropertyName);
        }

        private void btnVaciarArchivo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Eliminar de Forma Permanente los productos Almacenados? ", "VACIAR ARCHIVO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            try
            {
                miarchivo.EliminarArchivo();
            }
            catch (Exception X)
            {
                MessageBox.Show("ERROR: " + X.Message);

            }
            MostrarDatos();
            MessageBox.Show("Se ha Eliminado Todo lo Almacenado");
        }

        private void dgvArchivos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvArchivos.ReadOnly = true;
            dgvArchivos.AllowUserToAddRows = false;
            dgvArchivos.AllowUserToDeleteRows = false;
            dgvArchivos.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
