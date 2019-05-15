using System.Threading.Tasks;

namespace Coursework1.Core
{
    public interface IClientDataStore
    {
        Task<bool> HasCredentials();

        Task EnsureDataStoreAsync();

        Task<LoginCredentialsDataModel> GetLoginCredentialsAsync();

        Task SaveLoginCredentialsAsync(LoginCredentialsDataModel loginCredentials);
    }
}