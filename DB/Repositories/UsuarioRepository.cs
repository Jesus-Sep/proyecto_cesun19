using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_00.DB.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuarios>, IUsuarioRepository
    {
        public UsuarioRepository(Prueba1DB context) : base(context) { }

        // Métodos específicos
        public Usuarios GetByEmail(string email) =>
            _context.Usuarios.FirstOrDefault(u => u.Correo == email);

        public bool ValidateLogin(string email, string password) =>
            _context.Usuarios.Any(u => u.Correo == email && u.Contraseña == password);
    }
}
