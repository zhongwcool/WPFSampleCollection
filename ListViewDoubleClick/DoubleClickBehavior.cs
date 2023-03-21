using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ListViewDoubleClick;

/// <summary>
/// ダブルクリックした時にコマンドを実行する添付ビヘイビア
/// </summary>
internal class DoubleClickBehavior
{
    /// <summary>
    /// Commandの依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(DoubleClickBehavior),
            new UIPropertyMetadata(OnChangeCommand));

    /// <summary>
    /// コマンドパラメータの依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty ParameterProperty =
        DependencyProperty.RegisterAttached("Parameter", typeof(object), typeof(DoubleClickBehavior));

    /// <summary>
    /// コマンドを設定します（添付ビヘイビア）
    /// </summary>
    /// <param name="target">対象</param>
    /// <param name="value">コマンド</param>
    public static void SetCommand(DependencyObject target, object value)
    {
        target.SetValue(CommandProperty, value);
    }

    /// <summary>
    /// コマンドを取得します（添付ビヘイビア）
    /// </summary>
    /// <param name="target">対象</param>
    /// <returns>コマンド</returns>
    public static ICommand GetCommand(DependencyObject target)
    {
        return (ICommand)target.GetValue(CommandProperty);
    }

    /// <summary>
    /// コマンドパラメータを設定します（添付ビヘイビア）
    /// </summary>
    /// <param name="target">対象</param>
    /// <param name="value">パラメータ</param>
    public static void SetParameter(DependencyObject target, object value)
    {
        target.SetValue(ParameterProperty, value);
    }

    /// <summary>
    /// コマンドパラメータを取得します（添付プロパティ）
    /// </summary>
    /// <param name="target">対象</param>
    /// <returns>コマンドパラメータ</returns>
    public static object GetParameter(DependencyObject target)
    {
        return target.GetValue(ParameterProperty);
    }

    /// <summary>
    /// コマンドプロパティが変更された際の処理
    /// </summary>
    /// <param name="target">対象</param>
    /// <param name="e">イベント情報</param>
    private static void OnChangeCommand(DependencyObject target, DependencyPropertyChangedEventArgs e)
    {
        if (target is not Control control) return;
        if (e.OldValue == null && e.NewValue != null)
        {
            control.MouseDoubleClick += OnDoubleClick;
        }
        else if (e.OldValue != null && e.NewValue == null)
        {
            control.MouseDoubleClick -= OnDoubleClick;
        }
    }

    /// <summary>
    /// ダブルクリック時の処理
    /// </summary>
    /// <param name="sender">送り先</param>
    /// <param name="e">イベント情報</param>
    private static void OnDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is not Control control) return;
        var command = (ICommand)control.GetValue(CommandProperty);
        var param = control.GetValue(ParameterProperty);
        command.Execute(param);
    }
}