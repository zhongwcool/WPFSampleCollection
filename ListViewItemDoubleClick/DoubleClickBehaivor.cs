using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ListViewItemDoubleClick
{
	/// <summary>
	/// ダブルクリックした時にコマンドを実行する添付ビヘイビア
	/// </summary>
	class DoubleClickBehaivor
	{
		/// <summary>
		/// Commandの依存関係プロパティ
		/// </summary>
		public static DependencyProperty CommandProperty =
			DependencyProperty.RegisterAttached( "Command", typeof( ICommand ), typeof( DoubleClickBehaivor ), new UIPropertyMetadata( OnChangeCommand ) );

		/// <summary>
		/// コマンドパラメータの依存関係プロパティ
		/// </summary>
		public static DependencyProperty ParameterProperty =
			DependencyProperty.RegisterAttached( "Parameter", typeof( object ), typeof( DoubleClickBehaivor ) );

		/// <summary>
		/// コマンドを設定します（添付ビヘイビア）
		/// </summary>
		/// <param name="target">対象</param>
		/// <param name="value">コマンド</param>
		public static void SetCommand(DependencyObject target, object value)
		{
			target.SetValue( CommandProperty, value );
		}

		/// <summary>
		/// コマンドを取得します（添付ビヘイビア）
		/// </summary>
		/// <param name="target">対象</param>
		/// <returns>コマンド</returns>
		public static ICommand GetCommand(DependencyObject target)
		{
			return ( ICommand )target.GetValue( CommandProperty );
		}

		/// <summary>
		/// コマンドパラメータを設定します（添付ビヘイビア）
		/// </summary>
		/// <param name="target">対象</param>
		/// <param name="value">パラメータ</param>
		public static void SetParameter(DependencyObject target, object value)
		{
			target.SetValue( ParameterProperty, value );
		}

		/// <summary>
		/// コマンドパラメータを取得します（添付プロパティ）
		/// </summary>
		/// <param name="target">対象</param>
		/// <returns>コマンドパラメータ</returns>
		public static object GetParameter(DependencyObject target)
		{
			return target.GetValue( ParameterProperty );
		}

		/// <summary>
		/// コマンドプロパティが変更された際の処理
		/// </summary>
		/// <param name="target">対象</param>
		/// <param name="e">イベント情報</param>
		private static void OnChangeCommand(DependencyObject target, DependencyPropertyChangedEventArgs e)
		{
			Control control = target as Control;
			if( control != null ) {
				if( e.OldValue == null && e.NewValue != null ) {
					control.MouseDoubleClick += OnDoubleClick;
				}
				else if( e.OldValue != null && e.NewValue == null ) {
					control.MouseDoubleClick -= OnDoubleClick;
				}
			}
		}

		/// <summary>
		/// ダブルクリック時の処理
		/// </summary>
		/// <param name="sender">送り先</param>
		/// <param name="e">イベント情報</param>
		private static void OnDoubleClick(object sender, MouseButtonEventArgs e)
		{
			Control control = sender as Control;
			if( control != null ) {
				ICommand command = ( ICommand )control.GetValue( CommandProperty );
				object param = control.GetValue( ParameterProperty );
				command.Execute( param );
			}
		}
	}
}
