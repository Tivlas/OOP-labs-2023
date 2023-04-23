using Application.Abstractions.Console;
using Domain.Abstractions.ConsoleSync;
using Domain.Entities;

namespace Application.Services.Console;
public class ConsoleUserService : IConsoleUserService
{
    private IConsoleUnitOfWork _unit;

    public ConsoleUserService(IConsoleUnitOfWork unit)
    {
        _unit = unit;
    }

    public bool Exists(Func<User, bool> filter)
    {
        return _unit.UsersRepository.Exists(filter);
    }

    public IReadOnlyList<User> ListAll() => throw new NotImplementedException();
    public IReadOnlyList<User> List(Func<User, bool> filter) => throw new NotImplementedException();
    public void Add(User entity)
    {
        _unit.UsersRepository.Add(entity);
    }
    public void Update(User entity)
    {
        _unit.UsersRepository.Update(entity);
    }
    public void Delete(User entity)
    {
        _unit.UsersRepository.Delete(entity);
    }
    public User FirstOrDefault(Func<User, bool> filter)
    {
        return _unit.UsersRepository.FirstOrDefault(filter);
    }
}
