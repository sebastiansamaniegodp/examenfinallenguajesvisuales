using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SysWpfUninorte.Utilidades
{
    public class Conexion
    {
        private SqlConnection sqlConexion = new SqlConnection(ConfigurationManager.
            ConnectionStrings["SysWpfUninorte.Properties.Settings.uninorteConnectionString"].ConnectionString);

        public SqlConnection AbrirConexion()
        {
            if (sqlConexion.State == ConnectionState.Closed)
                sqlConexion.Open();
            return sqlConexion;
        }


        public SqlConnection CerrarConexion()
        {
            if (sqlConexion.State == ConnectionState.Open)
                sqlConexion.Close();
            return sqlConexion;
        }

    }
}
