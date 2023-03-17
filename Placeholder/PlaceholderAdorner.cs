using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Placeholder;

/// <summary>
/// プレースホルダーのための装飾
/// </summary>
class PlaceholderAdorner : Adorner
{
    /// <summary>
    /// グリッド
    /// </summary>
    private Grid grid_;

    /// <summary>
    /// 表示用テキストブロック
    /// </summary>
    public TextBlock TextBlock { get; private set; }

    /// <summary>
    /// 子Visualの数
    /// </summary>
    protected override int VisualChildrenCount
    {
        get { return 1; }
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="element">装飾先</param>
    public PlaceholderAdorner(UIElement element)
        : base(element)
    {
        grid_ = new Grid() { Background = Brushes.Transparent };
        TextBlock = new TextBlock() { Foreground = new SolidColorBrush(Colors.Gray) };
        TextBlock.Margin = new Thickness(5, 2, 0, 2);
        grid_.Children.Add(TextBlock);
    }

    /// <summary>
    /// 子コントロールを配置して、サイズを設定します
    /// </summary>
    /// <param name="finalSize">自分の最終サイズ</param>
    /// <returns>最終サイズ</returns>
    protected override Size ArrangeOverride(Size finalSize)
    {
        grid_.Arrange(new Rect(finalSize));
        return finalSize;
    }

    /// <summary>
    /// 指定されたインデックスの子Visualを取得します
    /// </summary>
    /// <param name="index">インデックス</param>
    /// <returns>Visual</returns>
    protected override Visual GetVisualChild(int index)
    {
        return grid_;
    }
}