using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PROYECTO_ULTIMA_UNIDAD.Mysql
{
    class Conexion
    {
        public static MySqlConnection obtenerConexion()
        {
            MySqlConnection conexion = new MySqlConnection("server=127.0.0.1;database=Productos;Uid=root;pwd=Vasconcelos1;");
            conexion.Open();
            return conexion;
        }
    }
}
