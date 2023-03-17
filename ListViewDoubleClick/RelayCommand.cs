using System;
using System.Windows.Input;

namespace ListViewDoubleClick;

/// <summary>
/// コマンド
/// </summary>
class RelayCommand<T> : ICommand
{
    /// <summary>
    /// 実行可能状態変更イベント
    /// </summary>
#pragma warning disable 67
    public event EventHandler CanExecuteChanged;
#pragma warning restore 67

    /// <summary>
    /// アクション
    /// </summary>
    private readonly Action<T> _action;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="action">コマンド実行時アクション</param>
    public RelayCommand(Action<T> action)
    {
        _action = action;
    }

    /// <summary>
    /// 実行可能かを取得します
    /// </summary>
    /// <param name="parameter">パラメータ</param>
    /// <returns>実行可能な場合、trueを返します</returns>
    public bool CanExecute(object parameter)
    {
        return true;
    }

    /// <summary>
    /// コマンドを実行します
    /// </summary>
    /// <param name="parameter">パラメータ</param>
    public void Execute(object parameter)
    {
        if (_action != null)
        {
            _action.Invoke((T)parameter);
        }
    }
}