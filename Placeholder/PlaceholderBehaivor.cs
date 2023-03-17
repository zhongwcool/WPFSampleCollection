using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Xaml.Behaviors;

namespace Placeholder
{
    /// <summary>
    /// Adornerを利用してプレースホルダーを提供するビヘイビア
    /// </summary>
    class PlaceholderBehaivor : Behavior<TextBox>
    {
        /// <summary>
        /// プレースホルダーとして表示する文字列の依存関係プロパティ
        /// </summary>
        public static DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached(
                "Text",
                typeof(string),
                typeof(PlaceholderBehaivor),
                new UIPropertyMetadata(OnTextChanged));

        /// <summary>
        /// 装飾
        /// </summary>
        private PlaceholderAdorner adorner_;

        /// <summary>
        /// 表示テキスト
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
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
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(AssociatedObject);
            adorner_ = new PlaceholderAdorner(AssociatedObject);
            adorner_.TextBlock.Text = Text;
            adornerLayer.Add(adorner_);

            if (AssociatedObject.IsFocused)
            {
                adorner_.Visibility = Visibility.Hidden;
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
            if (adorner_ != null)
            {
                adorner_.Visibility = Visibility.Hidden;
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
            if (adorner_ != null)
            {
                if (string.IsNullOrEmpty(AssociatedObject.Text))
                {
                    adorner_.Visibility = Visibility.Visible;
                }
                else
                {
                    adorner_.Visibility = Visibility.Hidden;
                }
            }
        }

        /// <summary>
        /// テキストプロパティが変更された際の処理
        /// </summary>
        /// <param name="target">対依存関係プロパティ</param>
        /// <param name="e">イベント</param>
        private static void OnTextChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            PlaceholderBehaivor self = target as PlaceholderBehaivor;
            if (self != null && self.adorner_ != null)
            {
                self.adorner_.TextBlock.Text = (string)e.NewValue;
            }
        }
    }
}