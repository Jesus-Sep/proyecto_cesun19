using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using proyecto_00.Vistas;
using System.Data.SqlClient;

namespace proyecto_00.Vistas
{
    public partial class CrearCuenta : UserControl
    {
        // Cadena de conexión a la base de datos
        private string connectionString = "Server=localhost;Database=PrestamosLibros;Integrated Security=True;";

        public CrearCuenta()
        {
            InitializeComponent();
        }

        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            // Validar campos vacíos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                txtPassword.Password.Length == 0)
            {
                MessageBox.Show("Por favor complete todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Conectar a la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Insertar nuevo usuario
                    string query = @"INSERT INTO Usuarios (Nombre, Email, Password, Rango, FechaRegistro) 
                                    VALUES (@Nombre, @Email, @Password, @Rango, @FechaRegistro)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Email", txtEmail.Text);
                        command.Parameters.AddWithValue("@Password", HashPassword(txtPassword.Password)); // Hashear la contraseña
                        command.Parameters.AddWithValue("@Rango", "Usuario"); // Rango por defecto
                        command.Parameters.AddWithValue("@FechaRegistro", DateTime.Now);

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Registro exitoso!", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                            // Limpiar campos después del registro
                            txtNombre.Text = "";
                            txtEmail.Text = "";
                            txtPassword.Password = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Método simple para hashear la contraseña (en producción usa algo más robusto)
        private string HashPassword(string password)
        {
            // En un proyecto real, usa BCrypt o similar
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // Navegar a la ventana de login
            MainWindow loginWindow = new MainWindow ();
            loginWindow.Show();
        }
    }
}
