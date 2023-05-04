using Application.Abstractions.NotConsole;
using Application.Services.NotConsole;
using CommunityToolkit.Maui;
using Domain.Abstractions.NotForConsoleAsync;
using FinanceManagementMAUI.Pages;
using FinanceManagementMAUI.ViewModels;
using Microsoft.Extensions.Logging;
using Persistence.Repository;
using Persistence.UnitOfWork;

namespace FinanceManagementMAUI;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif
        SetupServices(builder.Services);
        SetupPages(builder.Services);
        SetupViewModels(builder.Services);

        return builder.Build();
    }

    private static void SetupServices(IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWork, EfUnitOfWork>();
        services.AddSingleton<ISimpleAccountService, SimpleAccountService>();
        services.AddSingleton<ISimpleTransactionService, SimpleTransactionService>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<ITransactionCategoryService, TransactionCategoryService>();
    }

    private static void SetupPages(IServiceCollection services)
    {
        services.AddSingleton<LoginPage>();
    }

    private static void SetupViewModels(IServiceCollection services)
    {
        services.AddSingleton<LoginViewModel>();
    }
}
