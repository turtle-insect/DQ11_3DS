using System;
using System.Windows;
using System.Windows.Controls;

namespace DQ11
{
	/// <summary>
	/// ItemSelect.xaml の相互作用ロジック
	/// </summary>
	public partial class ItemSelectWindow : Window
	{
		public enum eType
		{
			Tool,
			Equipment,
			All,
		};

		public eType Type { private get; set; } = eType.All;
		public ItemInfo Info { get; set; } = null;
		public uint Kind { get; set; }

		public ItemSelectWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			CreateItemList("");
			if (Info != null && Info != Item.Instance().None)
			{
				ListBoxItem.SelectedItem = Info;
				ListBoxItem.ScrollIntoView(Info);
			}
		}

		private void TextBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
		{
			CreateItemList(TextBoxFilter.Text);
		}

		private void ListBoxItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBoxOption.Items.Clear();
			ComboBoxOption.IsEnabled = false;
			ButtonDecision.IsEnabled = ListBoxItem.SelectedIndex >= 0;
			if(ListBoxItem.SelectedIndex >= 0)
			{
				ItemInfo info = ListBoxItem.SelectedItem as ItemInfo;
				if (info == null) return;
				if(info.Count > 1)
				{
					ComboBoxOption.IsEnabled = true;
					ComboBoxOption.Items.Add("");
					for (uint i = 1; i < info.Count; i++)
					{
						ComboBoxOption.Items.Add("+" + i.ToString());
					}
					ComboBoxOption.SelectedIndex = 0;
				}
			}
		}

		private void ButtonDecision_Click(object sender, RoutedEventArgs e)
		{
			Info = ListBoxItem.SelectedItem as ItemInfo;
			int index = ComboBoxOption.SelectedIndex;
			if (index < 0) index = 0;
			Kind = (uint)index;
			Close();
		}

		private void CreateItemList(String filter)
		{
			ListBoxItem.Items.Clear();
			Item item = Item.Instance();

			ListBoxItem.Items.Add(item.None);
			if (Type != eType.Equipment)
			{
				foreach (var info in item.Tools)
				{
					if (String.IsNullOrEmpty(filter) || info.Name.IndexOf(filter) >= 0)
					{
						ListBoxItem.Items.Add(info);
					}
				}
			}
			if (Type != eType.Tool)
			{
				foreach (var info in item.Equipments)
				{
					if (String.IsNullOrEmpty(filter) || info.Name.IndexOf(filter) >= 0)
					{
						ListBoxItem.Items.Add(info);
					}
				}
			}
		}
	}
}
