using Domain.Entities.Interfaces;
using SQLite;
namespace Domain.Entities.Transactions
{
    public class TransactionCategory : INamedEntity, IRelatedToUser
    {
        public TransactionCategory()
        {

        }
        public TransactionCategory(string name, int userId)
        {
            Name = name;
            UserId = userId;
        }

        public string Name { get; set; }

        [PrimaryKey, Indexed, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }

        public List<SimpleTransaction> SimpleTransactions { get; set; } = new();

        public IEnumerable<(string PropName, object propValue)> GetInfo()
        {
            List<(string PropName, object propValue)> info = new();
            info.Add(("Name", Name));
            return info;
        }
    }
}