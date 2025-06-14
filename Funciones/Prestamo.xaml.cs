using proyecto_00.DB;
using proyecto_00.Modals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Prestamo : Window, INotifyPropertyChanged
    {
        private ProyectoBibliotecaBetaEntities DB = new ProyectoBibliotecaBetaEntities();

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Prestamo()
        {
            InitializeComponent();
            GetLibros();
            this.DataContext = this; 
        }

        private void GetLibros()
        {
            Libros = DB.Libros.ToList();

            // DEBUG: Verificar datos
            System.Diagnostics.Debug.WriteLine($"Libros cargados: {Libros?.Count ?? 0}");
        }

        private void CerrarButton_Click(object sender, RoutedEventArgs e) => Close();

        private void VolverButton(object sender, RoutedEventArgs e)
        {
            new Menu_Usuario().Show();
            Close();
        }
    }
}

