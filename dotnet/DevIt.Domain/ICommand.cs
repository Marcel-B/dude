namespace DevIt.Domain;

public interface ICommand<T>
{
  public T Validate(T obj);
}
