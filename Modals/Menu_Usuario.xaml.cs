using proyecto_00.DB;
using proyecto_00.Funciones;
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


namespace proyecto_00.Modals
{
    /// <summary>
    /// Lógica de interacción para Menu_Usuario.xaml
    /// </summary>
    public partial class Menu_Usuario : Window
    {
        private ProyectoBibliotecaBetaEntities db = new ProyectoBibliotecaBetaEntities();
        public List<Vista_PrestamosActivosPorUsuario> vista_PrestamosActivosPorUsuarios { get;set; }
        public Menu_Usuario()
        {

            InitializeComponent();
        }
        private void Salir(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Historial(object sender, RoutedEventArgs e)
        {
            Historial historial = new Historial();
            historial.Show();
            Window.GetWindow(this).Close();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            Prestamo prestamo = new Prestamo();
            prestamo.Show();
            Window.GetWindow(this).Close();
        }
    }
}
