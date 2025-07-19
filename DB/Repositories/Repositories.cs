using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace proyecto_00.DB.Repositories
{
    // ==================== REPOSITORIO BASE ====================
    public class GenericRepository<T> where T : class
    {
        protected readonly Prueba1DB _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(Prueba1DB context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public virtual int Save()
        {
            return _context.SaveChanges();
        }
    }

    // ==================== REPOSITORIO DE USUARIOS ====================
    public class UsuarioRepository : GenericRepository<Usuarios>
    {
        public UsuarioRepository(Prueba1DB context) : base(context) { }

        public Usuarios GetByEmail(string email)
        {
            return _context.Usuarios
                   .FirstOrDefault(u => u.Correo == email);
        }

        public bool ValidateCredentials(string email, string password)
        {
            return _context.Usuarios
                   .Any(u => u.Correo == email && u.Contraseña == password);
        }
    }

    // ==================== REPOSITORIO DE LIBROS ====================
    public class LibroRepository : GenericRepository<Libros>
    {
        public LibroRepository(Prueba1DB context) : base(context) { }

        public List<Libros> SearchByTitle(string title)
        {
            return _context.Libros
                   .Where(l => l.Titulo.Contains(title))
                   .ToList();
        }

        public List<Libros> GetAvailableBooks()
        {
            return _context.Libros
                   .Where(l => l.Disponible)
                   .ToList();
        }
    }

    // ==================== UNIDAD DE TRABAJO ====================
    public class UnitOfWork : IDisposable
    {
        private readonly Prueba1DB _context = new Prueba1DB();
        private bool _disposed = false;

        private UsuarioRepository _usuarioRepo;
        private LibroRepository _libroRepo;

        public UsuarioRepository Usuarios =>
            _usuarioRepo ?? (_usuarioRepo = new UsuarioRepository(_context));

        public LibroRepository Libros =>
            _libroRepo ?? (_libroRepo = new LibroRepository(_context));

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _context.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}