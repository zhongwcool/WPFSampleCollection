﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace ListViewDoubleClick;

/// <summary>
/// ビューモデル
/// </summary>
internal class ViewModel
{
    /// <summary>
    /// 犬リスト
    /// </summary>
    public ObservableCollection<string> DogList { get; set; }

    /// <summary>
    /// リストアイテム選択時のコマンド
    /// </summary>
    public RelayCommand<string> SelectCommand { get; set; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ViewModel()
    {
        DogList = new ObservableCollection<string>()
        {
            "Golden Retriever",
            "Miniature Dachshund",
            "Border Collie",
            "English Foxhound",
            "Silky Terrier",
            "Chihuahua",
        };

        SelectCommand = new RelayCommand<string>(OnSelect);
    }

    /// <summary>
    /// 選択時の処理
    /// </summary>
    /// <param name="item">選択アイテム</param>
    private static void OnSelect(string item)
    {
        System.Windows.MessageBox.Show(item);
    }
}