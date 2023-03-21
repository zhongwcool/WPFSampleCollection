using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Xaml.Behaviors;

namespace Placeholder;

/// <summary>
/// Adornerを利用してプレースホルダーを提供するビヘイビア
/// </summary>
internal class PlaceholderBehavior : Behavior<TextBox>
{
    /// <summary>
    /// プレースホルダーとして表示する文字列の依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.RegisterAttached("Text", typeof(string), typeof(PlaceholderBehavior),
            new UIPropertyMetadata(OnTextChanged));

    /// <summary>
    /// 装飾
    /// </summary>
    private PlaceholderAdorner _adorner;

    /// <summary>
    /// 表示テキスト
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// アタッチ時の処理
    /// </summary>
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.Loaded += OnLoaded;
        AssociatedObject.GotFocus += OnGotFocus;
        AssociatedObject.LostFocus += OnLostFocus;
    }

    /// <summary>
    /// デタッチ時の処理
    /// </summary>
    protected override void OnDetaching()
    {
        AssociatedObject.LostFocus -= OnLostFocus;
        AssociatedObject.GotFocus -= OnGotFocus;
        AssociatedObject.Loaded -= OnLoaded;
        base.OnDetaching();
    }

    /// <summary>
    /// 読み込み時の処理
    /// Adornerを生成、設定します
    /// </summary>
    /// <param name="sender">イベント元</param>
    /// <param name="e">イベントパラメータ</param>
    private void OnLoaded(object sender, System.EventArgs e)
    {
        // AdornerLayerはこのタイミング以降から取得可能
        var adornerLayer = AdornerLayer.GetAdornerLayer(AssociatedObject);
        _adorner = new PlaceholderAdorner(AssociatedObject);
        _adorner.TextBlock.Text = Text;
        adornerLayer.Add(_adorner);

        if (AssociatedObject.IsFocused)
        {
            _adorner.Visibility = Visibility.Hidden;
        }
    }

    /// <summary>
    /// フォーカスを取得した時の処理
    /// プレースホルダを非表示にします
    /// </summary>
    /// <param name="sender">イベント元</param>
    /// <param name="e">イベントパラメータ</param>
    private void OnGotFocus(object sender, RoutedEventArgs e)
    {
        if (_adorner != null)
        {
            _adorner.Visibility = Visibility.Hidden;
        }
    }

    /// <summary>
    /// フォーカスを失った時の処理
    /// プレースホルダを表示します
    /// </summary>
    /// <param name="sender">イベント元</param>
    /// <param name="e">イベントパラメータ</param>
    private void OnLostFocus(object sender, RoutedEventArgs e)
    {
        if (_adorner == null) return;
        if (string.IsNullOrEmpty(AssociatedObject.Text))
        {
            _adorner.Visibility = Visibility.Visible;
        }
        else
        {
            _adorner.Visibility = Visibility.Hidden;
        }
    }

    /// <summary>
    /// テキストプロパティが変更された際の処理
    /// </summary>
    /// <param name="target">対依存関係プロパティ</param>
    /// <param name="e">イベント</param>
    private static void OnTextChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
    {
        var self = target as PlaceholderBehavior;
        if (self != null && self._adorner != null)
        {
            self._adorner.TextBlock.Text = (string)e.NewValue;
        }
    }
}