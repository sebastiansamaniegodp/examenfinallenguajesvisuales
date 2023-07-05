using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SysWpfUninorte.Vistas
{
    /// <summary>
    /// Lógica de interacción para frmDashboard.xaml
    /// </summary>
    public partial class frmDashboard : Window
    {
        ABMAlumnos pantallaAlumnos = null;
        ABMDocentes pantallaDocentes = null;



        public frmDashboard()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

        private void MenuAlumno_Click(object sender, RoutedEventArgs e)
        {
            if (pantallaAlumnos == null || !pantallaAlumnos.IsActive)
            {
                pantallaAlumnos = new ABMAlumnos ();
                pantallaAlumnos.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                pantallaAlumnos.Show();
            }
            else
            {
                pantallaAlumnos.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                pantallaAlumnos.Focus();
            }
        }

        private void menuDocentes_Click(object sender, RoutedEventArgs e)
        {
            if (pantallaDocentes == null || !pantallaDocentes.IsActive)
            {
                pantallaDocentes = new ABMDocentes ();
                pantallaDocentes.WindowStartupLocation= WindowStartupLocation.CenterScreen;
                pantallaDocentes.Show();
            }
            else
            {
                pantallaDocentes.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                pantallaDocentes.Focus();
            }
        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

    }
}
