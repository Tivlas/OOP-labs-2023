using Application.Abstractions.Console;
using Domain.Abstractions.ConsoleSync;
using Domain.Entities.Accounts;

namespace Application.Services.Console;
public class SimpleAccountService : ISimpleAccountService<SimpleAccount>
{
    private IConsoleUnitOfWork _unit;

    public SimpleAccountService(IConsoleUnitOfWork unit)
    {
        _unit = unit;
    }

    public bool Exists(Func<SimpleAccount, bool> filter)
    {
        return _unit.SimpleAccountsRepository.Exists(filter);
    }

    public IReadOnlyList<SimpleAccount> ListAll()
    {
        return _unit.SimpleAccountsRepository.ListAll();
    }

    public IReadOnlyList<SimpleAccount> List(Func<SimpleAccount, bool> filter)
    {
        return _unit.SimpleAccountsRepository.List(filter);
    }

    public void Add(SimpleAccount entity)
    {
        _unit.SimpleAccountsRepository.Add(entity);
    }

    public void Update(SimpleAccount entity)
    {
        _unit.SimpleAccountsRepository.Update(entity);
    }

    public void Delete(SimpleAccount entity)
    {
        _unit.SimpleAccountsRepository.Delete(entity);
    }

    public SimpleAccount FirstOrDefault(Func<SimpleAccount, bool> filter)
    {
        return _unit.SimpleAccountsRepository.FirstOrDefault(filter);
    }
}
