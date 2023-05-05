using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementMAUI.Models
{
    public interface IHasher
    {
        string Hash(string value);
        bool Verify(string hashedValue, string originalValue);
    }
}
