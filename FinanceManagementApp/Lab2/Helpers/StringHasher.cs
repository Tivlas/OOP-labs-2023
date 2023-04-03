using System.Text;

namespace Lab2.Services
{
    public static class StringHasher
    {
        public static string GetHash(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                byte[] textData = Encoding.UTF8.GetBytes(s);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", string.Empty);
            }
        }
    }
}
