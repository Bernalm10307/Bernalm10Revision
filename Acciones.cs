using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PROYECTO_ULTIMA_UNIDAD.Mysql
{
    class Acciones
    {
        public static int Agregar(Producto add)
        {

            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(string.Format("insert into Tubos(id_Parte,Nombre,Modelo,Cantidad)values('{0}','{1}','{2}','{3}')", add.NumeroParte,add.Nombre,add.Modelo,add.Cantidad),Conexion.obtenerConexion());
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }
        public static List<Producto> mostrar()
        {
            List<Producto> ListaEmpleado = new List<Producto>();
            MySqlCommand micomando = new MySqlCommand(string.Format("select * from Tubos"),Conexion.obtenerConexion());
            MySqlDataReader reader = micomando.ExecuteReader();
            while (reader.Read())
            {
                Producto miproducto = new Producto();
                miproducto.NumeroParte = reader.GetString(0);
                miproducto.Nombre = reader.GetString(1);
                miproducto.Modelo = reader.GetString(2);
                miproducto.Cantidad = reader.GetInt16(3);
                ListaEmpleado.Add(miproducto);
            }
            return ListaEmpleado;


        }
        public static List<Producto> Buscar(string NumeroParte)
        {
            List<Producto> ListaBuscar = new List<Producto>();
            MySqlCommand micomando = new MySqlCommand(string.Format("select * from Tubos where id_Parte='{0}'",NumeroParte),Conexion.obtenerConexion());
            MySqlDataReader reader = micomando.ExecuteReader();
            while (reader.Read())
            {
                Producto miproducto = new Producto();
                miproducto.NumeroParte = reader.GetString(0);
                miproducto.Nombre = reader.GetString(1);
                miproducto.Modelo = reader.GetString(2);
                miproducto.Cantidad = reader.GetInt16(3);
                ListaBuscar.Add(miproducto);
            }
            return ListaBuscar;
        }
        public static Producto ObtenerProducto(string NParte)
        {
            Producto NProducto = new Producto();
            MySqlCommand micomando = new MySqlCommand(String.Format("select * from Tubos where id_Parte='{0}'",NParte),Conexion.obtenerConexion());
            MySqlDataReader reader = micomando.ExecuteReader();
            while(reader.Read())
            {
                NProducto.NumeroParte = reader.GetString(0);
                NProducto.Nombre = reader.GetString(1);
                NProducto.Modelo = reader.GetString(2);
                NProducto.Cantidad = reader.GetInt16(3);
            }
            return NProducto;
        }
        public static int Eliminar(string NumerodeParte)
        {
            MySqlCommand micomando = new MySqlCommand(String.Format("delete from Tubos where id_Parte='{0}'", NumerodeParte), Conexion.obtenerConexion());
            int Eliminado = micomando.ExecuteNonQuery();
            return Eliminado;
        }
        public static int Editar(int Cantidad,string NParte)
        {
            MySqlCommand micomando = new MySqlCommand(String.Format("update Tubos set Cantidad='{0}' where id_Parte='{1}'", Cantidad, NParte), Conexion.obtenerConexion());
            int Editado = micomando.ExecuteNonQuery();
            return Editado;
        }
    }
}
