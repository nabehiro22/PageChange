using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions;

namespace PageChange.ViewModels
{
	// IRegionMemberLifetimeを継承して「KeepAlive」をfalseとする事でページが切り替わる時(非表示になる時)破棄される。
	// IDestructibleを継承するとページが切り替わる時(非表示になる時)やアプリ終了時Destroyメソッドが実行されるので色々破棄する。
	public class Page2ViewModel : BindableBase, IRegionMemberLifetime, IDestructible
	{
		/// <summary>
		/// ページが切り替わる時破棄する設定
		/// </summary>
		public bool KeepAlive => false;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Page2ViewModel()
		{

		}

		/// <summary>
		/// IDestructibleを継承すると終了時呼ばれる
		/// </summary>
		public void Destroy()
		{

		}
	}
}
