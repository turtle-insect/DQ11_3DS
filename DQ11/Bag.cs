using System.Collections.Generic;
using System.Windows.Controls;

namespace DQ11
{
	class Bag
	{
		protected static readonly uint OnePageCount = 12;
		private ComboBox mPage;
		private uint mMax;
		private ItemSelectWindow.eType mType;
		protected uint mAddress;
		protected readonly List<AllStatus> mItems = new List<AllStatus>();

		public Bag(List<AllStatus> status, Panel panel, ItemSelectWindow.eType type, ComboBox page, uint address, uint max)
		{
			mPage = page;
			mAddress = address;
			mMax = max;
			mType = type;
			mPage.SelectionChanged += ComboBoxPage_SelectionChanged;

			uint maxPage = mMax / OnePageCount + (uint)(mMax % OnePageCount == 0 ? 0 : 1);
			for (uint i = 0; i < maxPage; i++)
			{
				mPage.Items.Add((i + 1).ToString() + " / " + maxPage.ToString());
			}
			page.SelectedIndex = 0;
			CreateComp(status, panel);
		}

		public void CreateComp(List<AllStatus> status, Panel panel)
		{
			for (uint i = 0; i < OnePageCount; i++)
			{
				Grid grid = new Grid();
				grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(30) });
				grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(30) });
				grid.ColumnDefinitions.Add(new ColumnDefinition());
				grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(30) });

				Button button = new Button();
				button.Content = "削除";
				button.Tag = i;
				button.Click += ButtonDelete_Click;
				grid.Children.Add(button);

				button = new Button();
				button.SetValue(Grid.ColumnProperty, 1);
				button.Content = " ... ";
				button.Tag = i;
				button.Click += ButtonChange_Click;
				grid.Children.Add(button);

				Label item = new Label();
				item.Content = "";
				item.SetValue(Grid.ColumnProperty, 2);
				grid.Children.Add(item);

				TextBox count = new TextBox();
				count.SetValue(Grid.ColumnProperty, 3);
				grid.Children.Add(count);
				BagItem toolItem = new BagItem(item, count, mAddress, i * 4);
				status.Add(toolItem);
				mItems.Add(toolItem);
				panel.Children.Add(grid);
			}
		}

		private void ComboBoxPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			mItems.ForEach(x => x.Save());
			mItems.ForEach(x => x.Page((uint)mPage.SelectedIndex));
			mItems.ForEach(x => x.Open());
		}

		protected void ButtonDelete_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Item item = Item.Instance();
			int itemCount = item.Tools.Count;
			Button button = sender as Button;
			if (button == null) return;
			uint i = (uint)button.Tag;
			i += (uint)mPage.SelectedIndex * OnePageCount;
			SaveData saveData = SaveData.Instance();
			for (; i < mMax - 1; i++)
			{
				saveData.WriteNumber(mAddress + i * 4, 4, saveData.ReadNumber(mAddress + (i + 1) * 4, 4));
			}
			saveData.WriteNumber(mAddress + (mMax - 1) * 4, 4, 0xFFFF);
			mItems.ForEach(x => x.Open());
		}

		protected void ButtonChange_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Item item = Item.Instance();
			int itemCount = item.Tools.Count;
			Button button = sender as Button;
			if (button == null) return;
			uint i = (uint)button.Tag;
			i += (uint)mPage.SelectedIndex * OnePageCount;
			SaveData saveData = SaveData.Instance();
			uint id = saveData.ReadNumber(mAddress + i * 4, 2);

			ItemSelectWindow window = new ItemSelectWindow();
			window.ID = id;
			window.Type = mType;
			window.ShowDialog();
			if (window.ID == item.None.ID)
			{
				ButtonDelete_Click(sender, e);
			}
			else
			{
				saveData.WriteNumber(mAddress + i * 4, 2, window.ID);
				saveData.WriteNumber(mAddress + i * 4 + 2, 1, 1);
				mItems.ForEach(x => x.Open());
			}
		}
	}
}
