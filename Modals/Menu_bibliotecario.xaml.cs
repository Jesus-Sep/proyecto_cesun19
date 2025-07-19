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
    /// Lógica de interacción para Menu_bibliotecario.xaml
    /// </summary>
    public partial class Menu_bibliotecario : Window
    {
        public Menu_bibliotecario()
        {
            InitializeComponent();
        }
        private void Salir(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void AceptarPrestamo(object sender, RoutedEventArgs e)
        {
            Aceptar_prestamos aceptar_Prestamos = new Aceptar_prestamos();
            aceptar_Prestamos.Show();
            Window.GetWindow(this).Close();
        }

        private void AgregarLibros(object sender, RoutedEventArgs e)
        {
            Prestamo prestamo = new Prestamo();
            prestamo.Show();
            Window.GetWindow(this).Close();
        }
        private void Imprimir(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }


    }
}
