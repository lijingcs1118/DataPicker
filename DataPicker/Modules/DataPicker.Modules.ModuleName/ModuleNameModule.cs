using DataPicker.Core;
using DataPicker.Modules.CsvModule.Services;
using DataPicker.Modules.CsvModule.ViewModels;
using DataPicker.Modules.CsvModule.Views;
using MediatR.Pipeline;
using MediatR;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.ComponentModel;
using System.Reflection;
using Prism.DryIoc;
using DryIoc;
using System;

namespace DataPicker.Modules.CsvModule
{
    public class ModuleNameModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleNameModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            SettingManager.LoadInitialSettings();
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(HomeView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HomeView>();
            containerRegistry.RegisterSingleton<HomeViewModel>();
            containerRegistry.RegisterDialog<AppSettingView, AppSettingViewModel>();
            containerRegistry.RegisterSingleton<IFileParserService, CsvFileParserService>();
            containerRegistry.RegisterSingleton<IFileSearcherService, CsvFileSearcherService>();
            containerRegistry.RegisterSingleton<IFileReaderService, CsvFileReaderService>();
            containerRegistry.RegisterSingleton<IDataParserService, DataParserService>();
            containerRegistry.RegisterSingleton<IDataSenderService, DatagramSenderService>();

            var container = containerRegistry.GetContainer();
            container.RegisterDelegate<ServiceFactory>(r => r.Resolve);
            container.RegisterMany(new[] { typeof(IMediator).GetAssembly(), typeof(FileName).GetAssembly() }, Registrator.Interfaces);
            container.Resolve<IMediator>();
        }
    }
}