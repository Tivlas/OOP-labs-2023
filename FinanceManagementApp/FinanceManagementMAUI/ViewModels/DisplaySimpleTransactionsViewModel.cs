using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using FinanceManagementMAUI.Services.Bindings;

namespace FinanceManagementMAUI.ViewModels;
public partial class DisplaySimpleTransactionsViewModel : ObservableObject
{
    public DisplaySimpleTransactionsViewModel(MutualSimpleTransactionBinding mutualSimpleTransactionBinding)
    {
        MutualSimpleTransactionBinding = mutualSimpleTransactionBinding;
    }

    public MutualSimpleTransactionBinding MutualSimpleTransactionBinding { get; }
}
