using proyecto_00.DB;
using proyecto_00.Modals;
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
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace proyecto_00.Vistas
{
    /// <summary>
    /// Lógica de interacción para Inicio_Sesion.xaml
    /// </summary>
    public partial class Inicio_Sesion : UserControl
    {
        private ProyectoBibliotecaBetaEntities db = new ProyectoBibliotecaBetaEntities();
        
        public Inicio_Sesion()
        {
            InitializeComponent();
        }
        private void Login(object sender, RoutedEventArgs e)
        {
            string username = TxtUsername.Text;
            string password = Txtpassword.Password;
            Usuarios user = db.Usuarios.FirstOrDefault(u =>
            (u.Nombre == username || u.Email == username) && u.Password == password
            );
            if (user != null)
            {
                if (user.Rango == "Bibliotecario")
                {
                    MessageBox.Show("Login Successful!");
                    Menu_bibliotecario menu_bibliotecario = new Menu_bibliotecario();
                    menu_bibliotecario.Show();
                    Window.GetWindow(this).Close();
                }
                if (user.Rango == "Usuario")
                {
                    MessageBox.Show("Login Successful!");
                    Menu_Usuario menu_Usuario = new Menu_Usuario();
                    menu_Usuario.Show();
                    Window.GetWindow(this).Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }

        }
    }
}
