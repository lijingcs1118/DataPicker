using DataPicker.Modules.AppSettings.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using DataPicker.Core;

namespace DataPicker.Modules.AppSettings
{
    public class AppSettingsModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public AppSettingsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ViewA");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA>();
        }
    }
}