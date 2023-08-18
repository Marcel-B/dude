using System.Data;
using DevIt.Persistence.Repositories;
using DevIt.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DevIt.Persistence;

public class UnitOfWork : IUnitOfWork
{
  private readonly ApplicationContext _context;

  public UnitOfWork(ApplicationContext context)
  {
    _context = context;
    Pbis = new PbiRepository(_context);
    Projekte = new ProjektRepository(_context);
    Eintraege = new EintragRepository(_context);
  }

  public IPbiRepository Pbis { get; }
  public IProjektRepository Projekte { get; }
  public IEintragRepository Eintraege { get; }

  public int Complete()
  {
    return _context.SaveChanges();
  }

  public async Task<int> CompleteAsync(CancellationToken cancellationToken)
  {
    //await using var transaction = await GetSqlTransactionAsync(cancellationToken);
    try
    {
      var result = await _context.SaveChangesAsync(cancellationToken);
      //transaction.CommitAsync(cancellationToken);
      return result;
    }
    catch (Exception ex)
    {
      //transaction.RollbackAsync(cancellationToken);
      throw;
    }
    finally
    {
      _context.ChangeTracker.Clear();
    }
  }

  public void Dispose()
  {
    _context.ChangeTracker.Clear();
    _context.Dispose();
  }

  public ValueTask DisposeAsync()
  {
    _context.ChangeTracker.Clear();
    return _context.DisposeAsync();
  }

  private async ValueTask<SqlTransaction> GetSqlTransactionAsync(CancellationToken cancellationToken)
  {
    var connection = (SqlConnection) _context.Database.GetDbConnection();
    if (connection.State != ConnectionState.Open)
    {
      await connection.OpenAsync(cancellationToken);
    }

    return connection.BeginTransaction();
  }
}
