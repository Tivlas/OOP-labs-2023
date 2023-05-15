namespace FinanceManagementMAUI.Services.PreferencesServices;
public class PreferencesService : IPreferencesService
{
    public void Set<T>(string key, T value)
    {
        Preferences.Default.Set(key, value);
    }

    public void SetUserData(int id, string email)
    {
        Preferences.Default.Set("id", id);
        Preferences.Default.Set("email", email);
    }

    public void RemoveUserData()
    {
        Preferences.Default.Remove("id");
        Preferences.Default.Remove("email");
    }

    public T Get<T>(string key, T defaultValue)
    {
        return Preferences.Default.Get(key, defaultValue);
    }
}
