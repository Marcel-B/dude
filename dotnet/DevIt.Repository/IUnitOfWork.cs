namespace DevIt.Repository;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
  IProjektRepository Projekte { get; }
  IPbiRepository Pbis { get; }
  IEintragRepository Eintraege { get; }
  Task<int> CompleteAsync(CancellationToken cancellationToken);
  int Complete();
}
