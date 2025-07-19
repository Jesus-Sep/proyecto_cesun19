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

namespace proyecto_00.Funciones
{
    /// <summary>
    /// Lógica de interacción para AgregarLibros.xaml
    /// </summary>
    public partial class AgregarLibros : Window
    {
        public AgregarLibros()
        {
            InitializeComponent();
        }
        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            // Validar campos vacíos
            if (string.IsNullOrWhiteSpace(txtTitulo.Text) ||
                string.IsNullOrWhiteSpace(txtAutor.Text) ||
                string.IsNullOrWhiteSpace(txtAnio.Text) ||
                string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Por favor complete todos los campos", "Advertencia",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Mostrar mensaje de confirmación (sin insertar realmente)
            MessageBox.Show($"Libro agregado (simulación):\n\n" +
                           $"Título: {txtTitulo.Text}\n" +
                           $"Autor: {txtAutor.Text}\n" +
                           $"Año: {txtAnio.Text}\n" +
                           $"Cantidad: {txtCantidad.Text}",
                           "Libro Agregado",
                           MessageBoxButton.OK, MessageBoxImage.Information);

            // Limpiar campos después de "agregar"
            txtTitulo.Text = "";
            txtAutor.Text = "";
            txtAnio.Text = "";
            txtCantidad.Text = "";
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
