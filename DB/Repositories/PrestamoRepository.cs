using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_00.DB.Repositories
{
    public class PrestamoRepository : GenericRepository<Prestamos>
    {
        public PrestamoRepository(Prueba1DB context) : base(context) { }

        // Métodos específicos para préstamos:
        public IEnumerable<Prestamos> GetActiveLoans()
            => _context.Prestamos.Where(p => p.FechaDevolucion == null).ToList();

        public int CountLoansByUser(int usuarioId)
            => _context.Prestamos.Count(p => p.UsuarioID == usuarioId);
    }
}
