using DataPicker.Modules.CsvModule;
using DataPicker.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using DataPicker.Modules.CsvModule.Services;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Prism.DryIoc;


namespace DataPicker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterSingleton<IMessageService, MessageService>();
            //containerRegistry.RegisterSingleton<IFileParserService, CsvFileParserService>();
            //containerRegistry.RegisterSingleton<IFileSearcherService, CsvFileSearcherService>();
            //containerRegistry.RegisterSingleton<IFileReaderService, CsvFileReaderService>();
            //containerRegistry.RegisterSingleton<IDataParserService, DataParserService>();
            //containerRegistry.RegisterSingleton<IDataSenderService, DatagramSenderService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleNameModule>();
        }

        protected override IContainerExtension CreateContainerExtension()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(configure =>
            {
                configure.ClearProviders();
                configure.SetMinimumLevel(LogLevel.Trace);
                configure.AddNLog();
            });

            return new DryIocContainerExtension(new Container(CreateContainerRules())
                .WithDependencyInjectionAdapter(serviceCollection));
        }
    }
}
