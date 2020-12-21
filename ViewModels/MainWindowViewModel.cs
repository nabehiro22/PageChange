using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace PageChange.ViewModels
{
	public class MainWindowViewModel : BindableBase, INotifyPropertyChanged
	{
		public ReactivePropertySlim<string> Title { get; } = new ReactivePropertySlim<string>("ページ切り替え");

		/// <summary>
		/// ページ切り替えに必要
		/// </summary>
		private readonly IRegionManager RegionManager;

		/// <summary>
		/// Disposeが必要な処理をまとめてやる
		/// </summary>
		private CompositeDisposable Disposable { get; } = new CompositeDisposable();

		/// <summary>
		/// MainWindowのCloseイベント
		/// </summary>
		public ReactiveCommand ClosedCommand { get; } = new ReactiveCommand();

		/// <summary>
		/// ページ1表示ボタン
		/// </summary>
		public ReactiveCommand Page1View { get; }

		/// <summary>
		/// ページ2表示ボタン
		/// </summary>
		public ReactiveCommand Page2View { get; }

		/// <summary>
		/// 現在表示されているページ(ページ切り替えボタンの抑制)
		/// </summary>
		private ReactivePropertySlim<int> Page { get; } = new ReactivePropertySlim<int>(1);

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="regionManager"></param>
		public MainWindowViewModel(IRegionManager regionManager)
		{
			RegionManager = regionManager;

			/***** ページの初期表示 *****/
			_ = RegionManager.RegisterViewWithRegion("ContentRegion", typeof(Views.Page1));

			/***** 変数Pageの数値でボタンの有効/無効を切り替える *****/
			Page1View = Page.Select(p => p != 1).ToReactiveCommand();
			Page2View = Page.Select(p => p != 2).ToReactiveCommand();

			/***** ページを表示する2つのパターン *****/
			_ = Page1View.Subscribe(_ =>
			{
				RegionManager.RequestNavigate("ContentRegion", nameof(Views.Page1));
				Page.Value = 1;
			}).AddTo(Disposable);
			//_ = Page1View.Subscribe(SetPage1).AddTo(Disposable);

			_ = Page2View.Subscribe(_ =>
			{
				RegionManager.RequestNavigate("ContentRegion", nameof(Views.Page2));
				Page.Value = 2;
			}).AddTo(Disposable);
			//_ = Page2View.Subscribe(SetPage2).AddTo(Disposable);

			_ = ClosedCommand.Subscribe(Close).AddTo(Disposable);
		}

		/// <summary>
		/// アプリが閉じられる時
		/// </summary>
		private void Close()
		{
			// RegionManagerの削除
			foreach (var region in RegionManager.Regions)
			{
				region.RemoveAll();
			}
			Disposable.Dispose();
		}

		/// <summary>
		/// ページ切り替えをメソッドで行う場合はReactiveCommandのSubscribeに登録
		/// </summary>
		private void SetPage1()
		{
			// 引数1 xamlのContentControlのRegionNameを指定 引数2 表示するXaml
			RegionManager.RequestNavigate("ContentRegion", nameof(Views.Page1));
			// この値でボタンが無効になる
			Page.Value = 1;
		}

		/// <summary>
		/// ページ切り替えをメソッドで行う場合はReactiveCommandのSubscribeに登録
		/// </summary>
		private void SetPage2()
		{
			// 引数1 xamlのContentControlのRegionNameを指定 引数2 表示するXaml
			RegionManager.RequestNavigate("ContentRegion", nameof(Views.Page2));
			// この値でボタンが無効になる
			Page.Value = 2;
		}
	}
}
