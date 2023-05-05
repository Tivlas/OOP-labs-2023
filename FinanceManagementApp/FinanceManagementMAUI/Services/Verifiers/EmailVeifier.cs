using FinanceManagementMAUI.Private;
using RestSharp;

namespace FinanceManagementMAUI.Services
{
    public class EmailVeifier : IEmailVerifier
    {
        public async Task<bool> Verify(string email)
        {
            var client = new RestClient(PrivateConstants.EmailVerifierApiUrl);
            var request = new RestRequest();
            request.AddParameter("email", email);
            request.AddParameter("api_key", PrivateConstants.EmailVerifierApiKey);

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"API request failed with status code {response.StatusCode}");
            }

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<EmailVerificationResult>(response.Content);

            return result.Data.Result == "deliverable";
        }
    }

    internal class EmailVerificationResult
    {
        public EmailVerificationData Data { get; set; }
    }

    internal class EmailVerificationData
    {
        public string Result { get; set; }
    }
}
