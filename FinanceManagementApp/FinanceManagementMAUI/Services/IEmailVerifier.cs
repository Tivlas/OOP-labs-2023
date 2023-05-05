using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementMAUI.Services
{
    public interface IEmailVerifier
    {
        Task<bool> Verify(string email);
    }
}
