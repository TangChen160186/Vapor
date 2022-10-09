// <copyright file="Program.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop
{
    using System;
    using System.Windows.Forms;
    using DarkUI.Win32;
    using FinalEngine.ECS;
    using FinalEngine.Editor.Desktop.Views;
    using FinalEngine.Editor.Services.Resources;
    using FinalEngine.Editor.ViewModels;
    using FinalEngine.Editor.Views;
    using FinalEngine.Editor.Views.Interactions;
    using FinalEngine.IO;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.OpenGL;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Resources;
    using Microsoft.Extensions.DependencyInjection;
    using ApplicationContext = FinalEngine.Editor.Desktop.Interactions.ApplicationContext;

    internal static class Program
    {
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IFileSystem, FileSystem>();
            services.AddSingleton<IOpenGLInvoker, OpenGLInvoker>();
            services.AddSingleton<IRenderDevice, OpenGLRenderDevice>();
            services.AddSingleton<IResourceManager>(ResourceManager.Instance);
            services.AddSingleton<IResourceLoaderRegistrar, ResourceLoaderRegistrar>();
            services.AddSingleton<IEntityWorld, EntityWorld>();
            services.AddSingleton<IApplicationContext, ApplicationContext>();
            services.AddSingleton<ViewModelFactory>();
            services.AddSingleton<IApplicationStarter, MainForm>();
            services.AddSingleton<IMainView, MainForm>();

            return services.BuildServiceProvider();
        }

        [STAThread]
        private static void Main()
        {
            Application.AddMessageFilter(new ControlScrollFilter());
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ConfigureServices().GetRequiredService<IApplicationStarter>().StartApplication();
        }
    }
}
