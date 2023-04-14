namespace Persistence.Data;

public interface IDbEmulatorContext
{
    List<T>? GetList<T>();
}