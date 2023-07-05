using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SysWpfUninorte.Dao;
using SysWpfUninorte.Entidades;

namespace SysWpfUninorte.Vistas
{
    /// <summary>
    /// Lógica de interacción para ABMAlumnos.xaml
    /// </summary>
    public partial class ABMAlumnos : Window
    {
        List<Alumno> listaAlumnos = new List<Alumno>();
        AlumnoDao alumnoDao;


        public ABMAlumnos()
        {
            InitializeComponent();
            initConfig();
            cargarAlumnos();
        }

        private void cargarAlumnos()
        {
            listaAlumnos = alumnoDao.GetAlumnos();
            dgListaAlumnos.ItemsSource = listaAlumnos;
        }

        private void initConfig() {
            alumnoDao = new AlumnoDao();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Alumno alumno = new Alumno();
            alumno.Nombres = txtNombres.Text;
            alumno.Direccion = txtDireccion.Text;

            if (string.IsNullOrEmpty(txtCodigoAlumno.Text))
            {
                if (alumnoDao.Insertar(alumno)){
                    MessageBox.Show("Alumno Guardado exitosamente!");
                    cargarAlumnos();
                }else{
                    MessageBox.Show("Error en el guardado");
                }
            }else{
                alumno.Id = Convert.ToInt64(txtCodigoAlumno.Text);

                if (alumnoDao.actualizar(alumno)){
                    MessageBox.Show("Alumno Guardado exitosamente!");
                    cargarAlumnos();
                }else{
                    MessageBox.Show("Error en el guardado");
                }
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Int64 IdAlumno = Convert.ToInt64(((Button)sender).Tag);
                Alumno alumno;
                //alumno = alumnoDao.GetAlumnoByCodigo(IdAlumno);

                alumno = listaAlumnos.Where(a => a.Id == IdAlumno).FirstOrDefault();

                this.txtCodigoAlumno.Text = alumno.Id.ToString();
                this.txtNombres.Text = alumno.Nombres;
                this.txtDireccion.Text = alumno.Direccion;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "ALERTA",
                    MessageBoxButton.OK,MessageBoxImage.Error
                );
            }

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(
                "Estas seguro?", "Confirmación", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes) {
                Int64 IdAlumno = Convert.ToInt64(((Button)sender).Tag);

                if (alumnoDao.eliminar(IdAlumno))
                {
                    MessageBox.Show("Eliminado!", "ALERTA",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation
                    );
                    cargarAlumnos();
                }else {
                    MessageBox.Show("No se pudo Eliminar", "ALERTA",
                       MessageBoxButton.OK, MessageBoxImage.Error
                   );
                }     
            }
        }
    }
}
