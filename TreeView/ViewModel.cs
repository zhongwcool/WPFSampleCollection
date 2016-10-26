using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TreeView
{
	/// <summary>
	/// ツリー構造のノード
	/// </summary>
	class Node : ObservableCollection<Node>
	{
		/// <summary>
		/// プロパティ変更イベント
		/// </summary>
		protected override event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// ノード名
		/// </summary>
		private string name_;

		/// <summary>
		/// ノード名
		/// </summary>
		public string Name
		{
			get { return name_; }
			set
			{
				name_ = value;
				if( PropertyChanged != null ) {
					PropertyChanged.Invoke( this, new PropertyChangedEventArgs( "Name" ) );
				}
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">ノード名</param>
		public Node( string name = null)
		{
			Name = name;
		}
	}

	/// <summary>
	/// ViewModel
	/// </summary>
	class ViewModel
	{
		/// <summary>
		/// ルートノード
		/// </summary>
		public Node RootNode { get; private set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ViewModel()
		{
			RootNode =
				new Node() {
					new Node( "Node 1-X" ) {
						new Node( "Node 1-1" ),
						new Node( "Node 1-2" ),
						new Node( "Node 1-3" ),
					},

					new Node("Node 2-X") {
						new Node( "Node 2-1" ){
							new Node("Node 2-1-1"),
							new Node("Node 2-1-2"),
						}
					},
				};
			
		}
	}
}
