using System.Threading.Tasks;
using System.Windows;
using Coursework1.Core;
using Coursework1.Relational;
using Dna;

namespace Coursework1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Custom start up so we load the IoC immediately before anything else
        /// </summary>
        /// <param name="e"></param>
        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Setup the main application 
            await ApplicationSetupAsync();

            // Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }

        /// <summary>
        /// Configures our application ready for use
        /// </summary>
        private Task ApplicationSetupAsync()
        {
            // Setup the Dna Framework
            new DefaultFrameworkConstruction()
                .UseClientDataStore()
                .Build();

            // Setup IoC
            IoC.Setup();

            // Ensure the client data store 
            //await IoC.ClientDataStore.EnsureDataStoreAsync();
            var clientDataStore = Framework.Service<IClientDataStore>();

            // Load new settings
            //await IoC.Settings.LoadAsync();
        }
    }
}
