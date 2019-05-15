using System.Linq;
using System.Threading.Tasks;
using Coursework1.Core;

namespace Coursework1.Relational
{
    public class ClientDataStore : IClientDataStore
    {
        #region Protected Members

        protected ClientDataStoreDbContext mDbContext;

        #endregion

        #region Constructor

        public ClientDataStore(ClientDataStoreDbContext context)
        {
            mDbContext = context;
        }

        #endregion

        #region Interface Implementation

        public async Task<bool> HasCredentials()
        {
            return await GetLoginCredentialsAsync() != null;
        }

        public async Task EnsureDataStoreAsync()
        {
            await mDbContext.Database.EnsureCreatedAsync();
        }

        public Task<LoginCredentialsDataModel> GetLoginCredentialsAsync()
        {
            return Task.FromResult(mDbContext.LoginCredentials.FirstOrDefault());
        }

        public async Task SaveLoginCredentialsAsync(LoginCredentialsDataModel loginCredentials)
        {
            // Clear all entries
            mDbContext.LoginCredentials.RemoveRange(mDbContext.LoginCredentials);

            // Add new one
            mDbContext.LoginCredentials.Add(loginCredentials);

            // Save changes
            await mDbContext.SaveChangesAsync();
        }

        #endregion
    }
}