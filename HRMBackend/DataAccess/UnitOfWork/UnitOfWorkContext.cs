using Npgsql;

namespace HRMBackend.DataAccess.UnitOfWork
{
    public class UnitOfWorkContext : IUnitOfWorkContext
    {
        #region Properties
        public NpgsqlConnection Context { get; init; }
        public NpgsqlTransaction Transaction { get; init; }

        public string Id { get; init; } = Guid.NewGuid().ToString();
        #endregion

        #region Constructor
        public UnitOfWorkContext(string connectionString)
        {
            this.Context = new NpgsqlConnection(connectionString);

            try
            {
                this.Context.Open();
                this.Transaction = Context.BeginTransaction();
            }
            catch (Exception ex)
            {
                NpgsqlConnection.ClearAllPools();
                this.Context.Open();
                this.Transaction = Context.BeginTransaction();
            }
        }
        #endregion

        #region Method
        public void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
            }

            if (Context != null)
            {
                Context.Close();
                Context.Dispose();
            }

            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
