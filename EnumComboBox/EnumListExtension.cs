using System;
using System.Windows.Markup;

namespace EnumComboBox
{
	/// <summary>
	/// 列挙体からリストを作成するマークアップ拡張
	/// </summary>
	public class EnumListExtension : MarkupExtension
	{
		/// <summary>
		/// 列挙体タイプ
		/// </summary>
		private Type enumType_;

		/// <summary>
		/// コストラクタ
		/// </summary>
		public EnumListExtension(Type type)
		{
			enumType_ = type;
		}

		/// <summary>
		/// プロパティ値の提供
		/// </summary>
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Enum.GetValues(enumType_);
		}
	}
}
