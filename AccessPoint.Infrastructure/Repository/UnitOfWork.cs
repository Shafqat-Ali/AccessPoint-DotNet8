using AccessPoint.Application.Interfaces;

namespace AccessPoint.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            LoginHistory = new LoginHistoryRepository(_context);
        }

        public IUserRepository Users { get; }
        public ILoginHistoryRepository LoginHistory { get; }

        public int Complete()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            return _context.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<int> CompleteReturnAsync()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
            // Save changes asynchronously and return the number of entities saved
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _context?.Dispose();
            }
            _disposed = true;
        }
    }
}