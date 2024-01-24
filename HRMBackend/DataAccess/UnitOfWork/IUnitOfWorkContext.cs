using Npgsql;

namespace HRMBackend.DataAccess.UnitOfWork
{
    public interface IUnitOfWorkContext : IDisposable
    {
        NpgsqlConnection Context { get; init; }
        NpgsqlTransaction Transaction { get; init; }
    }
}
