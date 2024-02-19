using Prism.Ioc;
using Syncfusion.Licensing;
using System.Windows;
using WPF_Application.Views;

namespace WPF_Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly string syncfusionKey = "Ngo9BigBOggjHTQxAR8/V1NHaF5cXmVCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdgWH9ed3RcQ2ReVU1/Wkc=";

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
             
        }

        public App()
        {
            SyncfusionLicenseProvider.RegisterLicense(syncfusionKey);
        }
    }
}
