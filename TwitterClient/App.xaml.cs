using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TwitterClient.Views;
using Microsoft.Practices.Unity;
using Prism.Mvvm;


namespace TwitterClient
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        //// アプリで管理するコンテナ
        //private IUnityContainer Container { get; } = new UnityContainer();

        //private void Application_Startup(object sender, StartupEventArgs e)
        //{
        //    ViewModelLocationProvider.SetDefaultViewModelFactory(x => this.Container.Resolve(x));
        //    this.Container.Resolve<MainWindow>().Show();
        //}

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bs = new Bootstrapper();
            bs.Run();
        }
    }
}
