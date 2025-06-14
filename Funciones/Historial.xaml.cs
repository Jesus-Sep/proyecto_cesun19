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
using proyecto_00.DB;
using proyecto_00.Modals;

namespace proyecto_00.Funciones
{
    /// <summary>
    /// Lógica de interacción para Historial.xaml
    /// </summary>
    public partial class Historial : Window
    {
        private ProyectoBibliotecaBetaEntities DB= new ProyectoBibliotecaBetaEntities();
        public List<HistorialPrestamos> HistorialPrestamo { get; set; }
        public Historial()
        {
            GetHistorialPrestamos();
            this.DataContext = this;
            InitializeComponent();
        }
        private void GetHistorialPrestamos()
        {
            HistorialPrestamo = DB.HistorialPrestamos.ToList();
        }
        private void CerrarButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void VolverButton(object sender, RoutedEventArgs e)
        {
            Menu_Usuario menu_Usuario = new Menu_Usuario();
            menu_Usuario.Show();
            Window.GetWindow(this).Close();
        }
    }
}
