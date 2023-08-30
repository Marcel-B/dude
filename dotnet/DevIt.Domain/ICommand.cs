namespace com.b_velop.DevIt.Domain;

public interface ICommand<T>
{
  public T Validate(T obj);
}
