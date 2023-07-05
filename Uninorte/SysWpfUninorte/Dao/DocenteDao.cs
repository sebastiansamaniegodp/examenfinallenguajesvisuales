using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using SysWpfUninorte.Entidades;
using SysWpfUninorte.Utilidades;

namespace SysWpfUninorte.Dao
{
    internal class DocenteDao
    {
        private Conexion conexion = new Conexion();//objeto conexion
        private SqlDataReader dataReader;//declarar la variable
        private SqlCommand comando = new SqlCommand();//objeto comando


        /// <summary>
        /// METODO PARA RETORNAR LISTADO DE DOCENTES
        /// </summary>
        /// <returns></returns>
        public List<Docente> GetDocentes()
        {
            List<Docente> listaDocentes = new List<Docente>();//variable lista

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT id,nombres,direccion FROM docentes";
            comando.CommandType = CommandType.Text;

            SqlDataReader dataReader = comando.ExecuteReader();

            //recorrer el resultado, cargar la entidad
            //agregar a la lista
            while (dataReader.Read()) //true
            {
                //crear el objeto entidad docente
                Docente docente = new Docente();
                docente.Id = dataReader.GetInt64(0);
                docente.Nombres = dataReader.GetString(1);
                docente.Direccion = dataReader.GetString(2);

                listaDocentes.Add(docente);
            }

            //liberar recursos de la memoria
            dataReader.Close();
            conexion.CerrarConexion();

            //retornar lista docentes
            return listaDocentes;
        }


        /// <summary>
        /// METODO PARA RETORNAR UN DOCENTE A PARTIR DEL CODIGO DEL DOCENTE
        /// </summary>
        /// <returns></returns>
        public Docente GetDocenteByCodigo(long idDocente)
        {
            Docente docente = null;

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT id,nombres,direccion FROM docentes WHERE id=" + idDocente;
            comando.CommandType = CommandType.Text;

            //comando.Parameters.AddWithValue("@id", idDocente);

            dataReader = comando.ExecuteReader();

            if (dataReader.HasRows) //true
            {
                dataReader.Read();
                new Docente();
                docente.Id = dataReader.GetInt64(0);
                docente.Nombres = dataReader.GetString(1);
                docente.Direccion = dataReader.GetString(2);
            }

            //liberar recursos de la memoria
            dataReader.Close();
            conexion.CerrarConexion();

            return docente;
        }

        /// <summary>
        /// METODO PARA INSERTAR NUEVO DOCENTE
        /// </summary>
        /// <param name="docente"></param>
        /// <returns>true o false</returns>
        public bool Insertar(Docente docente)
        {
            int filasAfectadas = 0;
            string sql = "insert into  docentes (nombres, direccion) values (@nombre, @direccion)";

            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = sql;
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@nombre", docente.Nombres);
                comando.Parameters.AddWithValue("@direccion", docente.Direccion);

                filasAfectadas = comando.ExecuteNonQuery();
                //liberar recusos
                comando.Parameters.Clear();
                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                throw new Exception("Hay un error al insertar:" + ex.Message);
            }

            return (filasAfectadas > 0) ? true : false;
        }


        /// <summary>
        /// METODO PARA ELIMINAR UN DOCENTE POR EL CAMPO ID
        /// </summary>
        /// <param name="idDocente"></param>
        /// <returns></returns>

        public bool eliminar(long idDocente)
        {
            int filasAfectadas = 0;
            string sql = "delete from docentes where id=@id";

            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = sql;
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@id", idDocente);

                filasAfectadas = comando.ExecuteNonQuery();

                comando.Parameters.Clear();
                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                throw new Exception("Hay un error al eliminar:" + ex.Message);
            }

            return (filasAfectadas > 0) ? true : false;
        }

        /// <summary>
        /// METODO PARA ACTUALIZAR LOS DATOS DEL DOCENTE
        /// </summary>
        /// <param name="docente"></param>
        /// <returns></returns>
        public bool actualizar(Docente docente)
        {
            int filasAfectadas = 0;
            string sql = "update docentes set nombres=@nombres, direccion=@direccion where id=@idDocente";

            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = sql;
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@nombres", docente.Nombres);
                comando.Parameters.AddWithValue("@direccion", docente.Direccion);
                comando.Parameters.AddWithValue("@idDocente", docente.Id);

                filasAfectadas = comando.ExecuteNonQuery();

                comando.Parameters.Clear();
                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                throw new Exception("Hay un error al actualizar:" + ex.Message);
            }

            return (filasAfectadas > 0) ? true : false;
        }
    }
}
