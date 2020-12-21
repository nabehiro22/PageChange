using Prism.Mvvm;
using Prism.Navigation;

namespace PageChange.ViewModels
{
	// IDestructibleを継承すると終了時Destroyメソッドが実行されるので色々破棄する。
	public class Page1ViewModel : BindableBase, IDestructible
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Page1ViewModel()
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
