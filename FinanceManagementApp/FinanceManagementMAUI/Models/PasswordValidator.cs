using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementMAUI.Models;
public class PasswordValidator : IPasswordValidator
{
    public bool IsValid(string? password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            return false;
        }
        return true;
    }
}
