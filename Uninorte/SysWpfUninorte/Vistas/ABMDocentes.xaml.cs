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
    /// Lógica de interacción para ABMDocentes.xaml
    /// </summary>
    public partial class ABMDocentes : Window
    {
        List<Docente> listaDocentes = new List<Docente>();
        DocenteDao DocenteDao;


        public ABMDocentes()
        {
            InitializeComponent();
            initConfig();
            cargarDocentes();
        }

        private void cargarDocentes()
        {
            listaDocentes = DocenteDao.GetDocentes();
            dgListaDocentes.ItemsSource = listaDocentes;
        }

        private void initConfig() {
            DocenteDao = new DocenteDao();
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
            Docente Docente = new Docente();
            Docente.Nombres = txtNombres.Text;
            Docente.Direccion = txtDireccion.Text;

            if (string.IsNullOrEmpty(txtCodigoDocente.Text))
            {
                if (DocenteDao.Insertar(Docente)){
                    MessageBox.Show("Docente Guardado exitosamente!");
                    cargarDocentes();
                }else{
                    MessageBox.Show("Error en el guardado");
                }
            }else{
                Docente.Id = Convert.ToInt64(txtCodigoDocente.Text);

                if (DocenteDao.actualizar(Docente)){
                    MessageBox.Show("Docente Guardado exitosamente!");
                    cargarDocentes();
                }else{
                    MessageBox.Show("Error en el guardado");
                }
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Int64 IdDocente = Convert.ToInt64(((Button)sender).Tag);
                Docente Docente;
                //Docente = DocenteDao.GetDocenteByCodigo(IdDocente);

                Docente = listaDocentes.Where(a => a.Id == IdDocente).FirstOrDefault();

                this.txtCodigoDocente.Text = Docente.Id.ToString();
                this.txtNombres.Text = Docente.Nombres;
                this.txtDireccion.Text = Docente.Direccion;
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
                Int64 IdDocente = Convert.ToInt64(((Button)sender).Tag);

                if (DocenteDao.eliminar(IdDocente))
                {
                    MessageBox.Show("Eliminado!", "ALERTA",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation
                    );
                    cargarDocentes();
                }else {
                    MessageBox.Show("No se pudo Eliminar", "ALERTA",
                       MessageBoxButton.OK, MessageBoxImage.Error
                   );
                }     
            }
        }
    }
}
