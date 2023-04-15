namespace Persistence.Data;

public interface IDbEmulatorContext
{
    IEnumerable<T>? GetList<T>();
}