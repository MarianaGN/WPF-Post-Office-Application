using Coursework1.Core;
using Dna;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coursework1.Relational
{
    public static class FrameworkConstructionExtensionMethods
    {/// <summary>
        /// Default constructor
        /// </summary>
        public static FrameworkConstruction UseClientDataStore(this FrameworkConstruction construction)
        {
            // Inject our SQLite EF data store
            construction.Services.AddDbContext<ClientDataStoreDbContext>(options =>
            {
                // Setup connection string
                options.UseSqlite(construction.Configuration.GetConnectionString("ClientDataStoreConnection"));
            });

            // Add client data store for easy access/use of the backing data store
            // Make it scoped so we can inject the scoped DbContext
            construction.Services.AddScoped<IClientDataStore>(
                provider => new ClientDataStore(provider.GetService<ClientDataStoreDbContext>()));

            // Return framework for chaining
            return construction;
        }
    }
}
