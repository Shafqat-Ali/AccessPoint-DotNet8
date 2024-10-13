namespace AccessPoint.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ILoginHistoryRepository LoginHistory { get; }
        int Complete();
        Task CompleteAsync();
        Task<int> CompleteReturnAsync();
    }
}
