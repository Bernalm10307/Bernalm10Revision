using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

namespace PROYECTO_ULTIMA_UNIDAD
{
    [Serializable]
    public class Producto : INotifyPropertyChanged
    {
        private string _strNumeroParte;
        private string _strNombre;
        private string _strModelo;
        private int _intCantidad;
        public string NumeroParte { get { return _strNumeroParte; } set { _strNumeroParte = value; } }
        public string Nombre { get { return _strNombre; } set { _strNombre = value; } }
        public string Modelo { get { return _strModelo; } set { _strModelo = value; } }
        public int Cantidad
        {
            get { return _intCantidad; }
            set
            {
                if (value == 0)
                {
                    throw new Exception("No deje en blanco la Cantidad");
                }
     
                else if(value>_intCantidad)
                {
                    _intCantidad = value;
                    NotificarCambioPropiedad("Cantidad");
                }
            }
        }

        public Producto() { }
        public Producto(string _strNumeroParte, string _strNombre, string _strModelo, int _intCantidad)
        {
            this._strNumeroParte = _strNumeroParte;
            this._strNombre = _strNombre;
            this._strModelo = _strModelo;
            this._intCantidad = _intCantidad;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotificarCambioPropiedad(string strNombrePropiedadCambiada)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new
                PropertyChangedEventArgs(strNombrePropiedadCambiada));
        }



    }

}
