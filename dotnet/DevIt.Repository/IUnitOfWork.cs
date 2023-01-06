using DevIt.Repository;

namespace DevIt.Persistence;
public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
  IProjektRepository Projekte { get; }
  IPbiRepository Pbis { get; }
  Task<int> CompleteAsync(CancellationToken cancellationToken);
  int Complete();
}
