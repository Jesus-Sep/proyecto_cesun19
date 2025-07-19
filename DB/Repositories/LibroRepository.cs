using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_00.DB.Repositories
{
    public class LibroRepository : GenericRepository<Libros>
    {
        public LibroRepository(Prueba1DB context) : base(context) { }

        // Métodos específicos para libros:
        public IEnumerable<Libros> GetAvailableBooks()
            => _context.Libros.Where(l => l.Disponible).ToList();

        public IEnumerable<Libros> SearchByTitle(string title)
            => _context.Libros.Where(l => l.Titulo.Contains(title)).ToList();
    }
}
