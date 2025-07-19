using proyecto_00.DB;
using proyecto_00.Modals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace proyecto_00.Funciones
{
    public partial class Prestamo : Window, INotifyPropertyChanged
    {
        private ProyectoBibliotecaBetaEntities DB = new ProyectoBibliotecaBetaEntities();
        private Libros _libroSeleccionado;

        private List<Libros> _libros;
        public List<Libros> Libros
        {
            get => _libros;
            set
            {
                _libros = value;
                OnPropertyChanged(nameof(Libros));
            }
        }

        public Libros LibroSeleccionado
        {
            get => _libroSeleccionado;
            set
            {
                _libroSeleccionado = value;
                OnPropertyChanged(nameof(LibroSeleccionado));
                SolicitarButton.IsEnabled = value != null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Prestamo()
        {
            InitializeComponent();
            GetLibros();
            this.DataContext = this;
        }

        private void GetLibros()
        {
            try
            {
                // Obtener solo libros disponibles
                Libros = DB.Libros.Where(l => l.Stock > 0).ToList();
                System.Diagnostics.Debug.WriteLine($"Libros cargados: {Libros?.Count ?? 0}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar libros: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LibrosDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LibroSeleccionado = LibrosDataGrid.SelectedItem as Libros;
        }

        private void SolicitarButton_Click(object sender, RoutedEventArgs e)
        {
            if (LibroSeleccionado == null) return;

            try
            {
                // Verificar disponibilidad
                if (LibroSeleccionado.Stock <= 0)
                {
                    MessageBox.Show("Este libro ya no está disponible", "Advertencia",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Aquí iría la lógica para registrar el préstamo en la base de datos
                // Por ahora solo mostramos un mensaje de confirmación

                MessageBox.Show($"Préstamo solicitado:\n\n" +
                              $"Título: {LibroSeleccionado.Nombre}\n" +
                              $"Autor: {LibroSeleccionado.AutorID}\n\n" +
                              "Su solicitud ha sido registrada.",
                              "Préstamo Solicitado",
                              MessageBoxButton.OK, MessageBoxImage.Information);

                // Actualizar la lista de libros
                GetLibros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al solicitar préstamo: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CerrarButton_Click(object sender, RoutedEventArgs e) => Close();

        private void VolverButton(object sender, RoutedEventArgs e)
        {
            new Menu_Usuario().Show();
            Close();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}