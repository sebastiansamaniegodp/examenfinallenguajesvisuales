using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SysWpfUninorte.Entidades;
using SysWpfUninorte.Utilidades;

namespace SysWpfUninorte.Dao
{
    public class AlumnoDao
    {
        private Conexion conexion = new Conexion();//objeto conexion
        private SqlDataReader dataReader;//declarar la variable
        private SqlCommand comando = new SqlCommand();//objeto comando


        /// <summary>
        /// METODO PARA RETORNAR LISTADO DE ALUMNOS
        /// </summary>
        /// <returns></returns>
        public List<Alumno> GetAlumnos()
        {
            List<Alumno> listaAlumnos = new List<Alumno>();//variable lista

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT id,nombres,direccion FROM alumnos";
            comando.CommandType = CommandType.Text;

            SqlDataReader dataReader = comando.ExecuteReader();

            //recorrer el resultado, cargar la entidad
            //agregar a la lista
            while (dataReader.Read()) //true
            {
                //crear el objeto entidad alumno
                Alumno alumno = new Alumno();
                alumno.Id = dataReader.GetInt64(0);
                alumno.Nombres = dataReader.GetString(1);
                alumno.Direccion = dataReader.GetString(2);

                listaAlumnos.Add(alumno);
            }

            //liberar recursos de la memoria
            dataReader.Close();
            conexion.CerrarConexion();

            //retornar lista alumnos
            return listaAlumnos;
        }


        /// <summary>
        /// METODO PARA RETORNAR UN ALUMNO A PARTIR DEL CODIGO DEL ALUMNO
        /// </summary>
        /// <returns></returns>
        public Alumno GetAlumnoByCodigo(long idAlumno)
        {
            Alumno alumno = null;

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT id,nombres,direccion FROM alumnos WHERE id=" + idAlumno;
            comando.CommandType = CommandType.Text;

            //comando.Parameters.AddWithValue("@id", idAlumno);

            dataReader = comando.ExecuteReader();

            if (dataReader.HasRows) //true
            {
                dataReader.Read();
                new Alumno();
                alumno.Id = dataReader.GetInt64(0);
                alumno.Nombres = dataReader.GetString(1);
                alumno.Direccion = dataReader.GetString(2);
            }

            //liberar recursos de la memoria
            dataReader.Close();
            conexion.CerrarConexion();

            return alumno;
        }

        /// <summary>
        /// METODO PARA INSERTAR NUEVO ALUMNO
        /// </summary>
        /// <param name="alumno"></param>
        /// <returns>true o false</returns>
        public bool Insertar(Alumno alumno)
        {
            int filasAfectadas = 0;
            string sql = "insert into  alumnos (nombres, direccion) values (@nombre, @direccion)";

            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = sql;
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@nombre", alumno.Nombres);
                comando.Parameters.AddWithValue("@direccion", alumno.Direccion);

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
        /// METODO PARA ELIMINAR UN ALUMNO POR EL CAMPO ID
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <returns></returns>

        public bool eliminar(long idAlumno)
        {
            int filasAfectadas = 0;
            string sql = "delete from alumnos where id=@id";

            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = sql;
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@id", idAlumno);

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
        /// METODO PARA ACTUALIZAR LOS DATOS DEL ALUMNO
        /// </summary>
        /// <param name="alumno"></param>
        /// <returns></returns>
        public bool actualizar(Alumno alumno)
        {
            int filasAfectadas = 0;
            string sql = "update alumnos set nombres=@nombres, direccion=@direccion where id=@idAlumno";

            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = sql;
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@nombres", alumno.Nombres);
                comando.Parameters.AddWithValue("@direccion", alumno.Direccion);
                comando.Parameters.AddWithValue("@idAlumno", alumno.Id);

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
