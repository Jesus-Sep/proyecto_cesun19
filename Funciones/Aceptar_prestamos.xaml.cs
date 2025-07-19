using proyecto_00.DB;
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
using System.Data.Entity;



namespace proyecto_00.Funciones
{
    /// <summary>
    /// Lógica de interacción para Aceptar_prestamos.xaml
    /// </summary>
    public partial class Aceptar_prestamos : Window
    {
        private ProyectoBibliotecaBetaEntities DB = new ProyectoBibliotecaBetaEntities();
        private PrestamosPendientes _prestamoSeleccionado;

        public Aceptar_prestamos()
        {
            InitializeComponent();
            CargarPrestamosPendientes();
        }

        private void CargarPrestamosPendientes()
        {
            try
            {
                // Cargar préstamos pendientes con información relacionada
                var prestamos = DB.PrestamosPendientes
                    .Include(p => p.Usuarios)
                    .Include(p => p.Libros)
                    .Where(p => p.Libros.Stock > 0)
                    .OrderBy(p => p.FechaCompromiso)
                    .ToList();

                PrestamosGrid.ItemsSource = prestamos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar préstamos: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PrestamosGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _prestamoSeleccionado = PrestamosGrid.SelectedItem as PrestamosPendientes;
            AceptarButton.IsEnabled = _prestamoSeleccionado != null;
            RechazarButton.IsEnabled = _prestamoSeleccionado != null;
        }

        private void AceptarButton_Click(object sender, RoutedEventArgs e)
        {
            if (_prestamoSeleccionado == null) return;

            try
            {
                // Confirmar acción
                var confirmacion = MessageBox.Show(
                    $"¿Aprobar préstamo del libro '{_prestamoSeleccionado.Libros.Nombre}' " +
                    $"al usuario '{_prestamoSeleccionado.Usuarios.Nombre}'?",
                    "Confirmar Aprobación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (confirmacion == MessageBoxResult.Yes)
                {
                    // Actualizar estado en la base de datos
                    _prestamoSeleccionado.Libros.Stock = -1;
                    DB.SaveChanges();

                    // Registrar en la tabla de préstamos activos (si existe)
                    var nuevoPrestamo = new Prestamos
                    {
                        LibroID = _prestamoSeleccionado.LibroID,
                        UsuarioID = _prestamoSeleccionado.UsuarioID,
                        FechaPrestamo = DateTime.Now,
                        FechaCompromiso = _prestamoSeleccionado.FechaCompromiso,
                        Estado = "Activo"
                    };
                    DB.Prestamos.Add(nuevoPrestamo);
                    DB.SaveChanges();

                    // Actualizar disponibilidad del libro
                    var libro = DB.Libros.Find(_prestamoSeleccionado.LibroID);
                    if (libro != null)
                    {
                        libro.Stock--;
                        DB.SaveChanges();
                    }

                    MessageBox.Show("Préstamo aprobado exitosamente", "Éxito",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarPrestamosPendientes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aprobar préstamo: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RechazarButton_Click(object sender, RoutedEventArgs e)
        {
            if (_prestamoSeleccionado == null) return;

            try
            {
                var confirmacion = MessageBox.Show(
                    $"¿Rechazar préstamo del libro '{_prestamoSeleccionado.Libros.Nombre}' " +
                    $"al usuario '{_prestamoSeleccionado.Usuarios.Nombre}'?",
                    "Confirmar Rechazo",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (confirmacion == MessageBoxResult.Yes)
                {
                    // Actualizar estado en la base de datos
                    //_prestamoSeleccionado.Estado = "Rechazado";
                    DB.SaveChanges();

                    MessageBox.Show("Préstamo rechazado exitosamente", "Éxito",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarPrestamosPendientes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al rechazar préstamo: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ActualizarButton_Click(object sender, RoutedEventArgs e)
        {
            CargarPrestamosPendientes();
        }

        private void CerrarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
