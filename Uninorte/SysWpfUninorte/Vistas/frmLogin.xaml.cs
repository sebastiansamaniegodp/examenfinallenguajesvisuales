using System;
using System.Windows;
using SysWpfUninorte.Dao;
using SysWpfUninorte.Entidades;

namespace SysWpfUninorte.Vistas
{
    /// <summary>
    /// Lógica de interacción para frmLogin.xaml
    /// </summary>
    public partial class frmLogin : Window
    {
        UsuarioDao usuarioDao = new UsuarioDao();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string nombres = txtUser.Text.ToString();
            string password = txtPass.Password.ToString();

            if (!string.IsNullOrEmpty(nombres) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    Usuario usuario = new Usuario();
                    usuario.Username = nombres;
                    usuario.Password = password;

                    if (usuarioDao.Ingresar(usuario))
                    {
                        frmDashboard mainWindow = new frmDashboard();
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El usuario o contraseña son incorrectos.");
                        txtUser.Text = "";
                        txtPass.Password = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                } 
            } else
            {
                MessageBox.Show("Por favor complete todos los campos.");
            }
        }
    }
}
