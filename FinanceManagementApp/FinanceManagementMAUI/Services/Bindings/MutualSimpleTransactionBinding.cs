﻿using System.Collections.ObjectModel;
using Application.Abstractions.NotConsole;
using Domain.Entities.Transactions;
using FinanceManagementMAUI.Services.PreferencesServices;

namespace FinanceManagementMAUI.Services.Bindings;
public class MutualSimpleTransactionBinding
{
    private readonly ISimpleTransactionService _simpleTransactionService;
    private readonly IPreferencesService _preferencesService;

    public MutualSimpleTransactionBinding(ISimpleTransactionService simpleTransactionService, IPreferencesService preferencesService)
    {
        _simpleTransactionService = simpleTransactionService;
        _preferencesService = preferencesService;
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Load();
        });
    }

    private async Task Load()
    {
        var transactions = await _simpleTransactionService.ListAsync(st => st.UserId == _preferencesService.Get("id", -1), default,
                st => st.Category, st => st.SimpleAccount);
        foreach (var t in transactions)
        {
            SimpleTransactions.Add(t);
        }
    }

    public async Task Reload()
    {
        SimpleTransactions.Clear();
        await Load();
    }

    public ObservableCollection<SimpleTransaction> SimpleTransactions { get; set; } = new();

    public void Add(SimpleTransaction st)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            SimpleTransactions.Add(st);
        });
    }

    public void Edit(SimpleTransaction st, int index)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            SimpleTransactions[index] = st;
        });
    }

    public void Remove(SimpleTransaction st)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            SimpleTransactions.Remove(st);
        });
    }
}
