using DevIt.Repository;

namespace DevIt.Persistence.Repositories;

public abstract class RepositoryBase<T> : IRepository<T> where T : class
{
  private readonly ApplicationContext _context;

  protected RepositoryBase(ApplicationContext context)
  {
    _context = context;
  }

  public IQueryable<T> Query()
  {
    return _context.Set<T>().AsQueryable();
  }
}
