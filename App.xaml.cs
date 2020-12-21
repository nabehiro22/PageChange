using PageChange.Views;
using Prism.Ioc;
using System.Windows;

namespace PageChange
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
			// ページを登録
			containerRegistry.RegisterForNavigation<Page1>();
			containerRegistry.RegisterForNavigation<Page2>();
		}
	}
}
