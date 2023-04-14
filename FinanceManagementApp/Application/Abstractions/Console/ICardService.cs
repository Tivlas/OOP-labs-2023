using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Cards;
using Domain.Entities.Interfaces;

namespace Application.Abstractions.Console;
public interface ICardService : IBaseConsoleService<Card>
{
}
