using Application.Abstractions.Console;
using Domain.Abstractions.ConsoleSync;
using Domain.Cards;

namespace Application.Services.Console;
public class CardService : ICardService
{
    private IConsoleUnitOfWork _unit;

    public CardService(IConsoleUnitOfWork unit)
    {
        _unit = unit;
    }


    public bool Exists(Func<Card, bool> filter)
    {
        return _unit.CardsRepository.Exists(filter);
    }

    public IReadOnlyList<Card> ListAll()
    {
        return _unit.CardsRepository.ListAll();
    }

    public IReadOnlyList<Card> List(Func<Card, bool> filter)
    {
        return _unit.CardsRepository.List(filter);
    }

    public void Add(Card entity)
    {
        _unit.CardsRepository.Add(entity);
    }

    public void Update(Card entity)
    {
        _unit.CardsRepository.Update(entity);
    }

    public void Delete(Card entity)
    {
        _unit.CardsRepository.Delete(entity);
    }

    public Card FirstOrDefault(Func<Card, bool> filter)
    {
        return _unit.CardsRepository.FirstOrDefault(filter);
    }
}
