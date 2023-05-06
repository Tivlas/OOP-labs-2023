using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementMAUI.Services.PreferencesServices;
public interface IPreferencesService
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Only: Bollean, Double, Int32, Single, Int64, String, DateTime</typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    void Set<T>(string key, T value);
    T Get<T>(string key, T defaultValue);
    void SetUserData(int id, string email);
    void RemoveUserData();
}
