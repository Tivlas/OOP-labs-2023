using System.Reflection;
using Application.Abstractions.NotConsole;
using Application.Services.NotConsole;
using CommunityToolkit.Maui;
using Domain.Abstractions.NotForConsoleAsync;
using FinanceManagementMAUI.Pages;
using FinanceManagementMAUI.Services;
using FinanceManagementMAUI.Services.PreferencesServices;
using FinanceManagementMAUI.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Persistence.Data;
using Persistence.UnitOfWork;

namespace FinanceManagementMAUI;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        string settingsStream = "FinanceManagementMAUI.appsettings.json";
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream(settingsStream);
        builder.Configuration.AddJsonStream(stream);


#if DEBUG
        builder.Logging.AddDebug();
#endif

        SetupServices(builder.Services);
        SetupViewModels(builder.Services);
        SetupPages(builder.Services);
        SetupDbContext(builder);
        DropDbForDevelopmentPurposes(builder.Services.BuildServiceProvider());
        return builder.Build();
    }

    private static void SetupServices(IServiceCollection services)
    {
        services.AddSingleton<ISimpleAccountService, SimpleAccountService>();
        services.AddSingleton<ISimpleTransactionService, SimpleTransactionService>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<ITransactionCategoryService, TransactionCategoryService>();
        services.AddSingleton<IUnitOfWork, EfUnitOfWork>();
        services.AddSingleton<IPasswordValidator, PasswordValidator>();
        services.AddSingleton<IEmailValidator, EmailValidator>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IPopupService, PopupService>();
        services.AddSingleton<IEmailVerifier, EmailVeifier>();
        services.AddSingleton<IPreferencesService, PreferencesService>();
    }

    private static void SetupPages(IServiceCollection services)
    {
        services.AddSingleton<MainPage>();
        services.AddSingleton<LoginPage>();
        services.AddSingleton<AddCategoryPage>();
    }

    private static void SetupViewModels(IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<LoginViewModel>();
        services.AddSingleton<AddCategoryViewModel>();
    }

    private static void SetupDbContext(MauiAppBuilder builder)
    {
        var connectionString = builder.Configuration
            .GetConnectionString("SqliteConnection");
        string dataDirectory = string.Empty;
#if ANDROID
        dataDirectory = FileSystem.AppDataDirectory + Path.DirectorySeparatorChar;
#else
        dataDirectory = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar;
#endif

        connectionString = string.Format(connectionString, dataDirectory);
        var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connectionString).Options;
        builder.Services.AddSingleton((s) => new AppDbContext(options));
    }

    private async static void DropDbForDevelopmentPurposes(IServiceProvider provider)
    {
        var unitOfWork = provider.GetRequiredService<IUnitOfWork>();
        var u = await unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Email == "tima051003@gmail.com");
        await unitOfWork.RemoveDatbaseAsync();
        Preferences.Default.Remove("id");
    }
}
