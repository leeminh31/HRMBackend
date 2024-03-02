namespace HRMBackend.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUnitOfWorkContext UnitOfWorkContext { get; init; }
        void SaveChanges();
        Task SaveChangesAsync();

        Task RollBackAsync();
    }
}
