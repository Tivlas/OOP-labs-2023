using Domain.Entities.Interfaces;

namespace Domain.Entities.Transactions
{
    public class TransactionCategory : INamedEntity, IRelatedToUser
    {
        private static int s_IdController = 0;

        public TransactionCategory()
        {

        }
        public TransactionCategory(string name, int userId)
        {
            Name = name;
            UserId = userId;
            Id = s_IdController;
            ++s_IdController;
        }

        public string Name { get; set; }

        public int Id { get; init; }

        public int UserId { get; init; }

        public User? User { get; set; }

        public IEnumerable<(string PropName, object propValue)> GetInfo()
        {
            List<(string PropName, object propValue)> info = new();
            info.Add(("Name", Name));
            return info;
        }
    }
}