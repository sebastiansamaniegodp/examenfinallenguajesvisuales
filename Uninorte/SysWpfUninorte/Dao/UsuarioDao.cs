using System;
using System.Data.SqlClient;
using SysWpfUninorte.Entidades;
using SysWpfUninorte.Utilidades;

namespace SysWpfUninorte.Dao
{
    public class UsuarioDao
    {
        private Conexion con = new Conexion();
        private SqlDataReader dataReader;
        private SqlCommand cmd = new SqlCommand();

        public bool Ingresar(Usuario usuario)
        {
            try
            {
                cmd.Connection = con.AbrirConexion();
                cmd.CommandText = "SELECT username,password FROM usuarios WHERE nombres=@username and contraseña=@password";
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.AddWithValue("@username", usuario.Username);
                cmd.Parameters.AddWithValue("@password", usuario.Password);

                using (dataReader = cmd.ExecuteReader())
                {

                    if (dataReader.Read())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false;
            } finally
            {
                cmd.Parameters.Clear();
                con.CerrarConexion();
            }  
        }
    }
}
