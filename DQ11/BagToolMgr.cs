using System.Collections.Generic;
using System.Windows.Controls;

namespace DQ11
{
	class BagToolMgr
	{
		private uint mMax;
		private List<ComboBox> mItems = new List<ComboBox>();
		private List<TextBox> mCounts = new List<TextBox>();

		public void Init(List<AllStatus> status, Panel panel, uint address, uint max)
		{
			mMax = max;
			for (int i = 0; i < max; i++)
			{
				Grid grid = new Grid();
				grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(30) });
				grid.ColumnDefinitions.Add(new ColumnDefinition());
				grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(30) });

				Button button = new Button();
				button.Content = "削除";
				button.Tag = i;
				button.Click += Button_Click;
				grid.Children.Add(button);

				ComboBox item = new ComboBox();
				mItems.Add(item);
				item.SetValue(Grid.ColumnProperty, 1);
				grid.Children.Add(item);

				TextBox count = new TextBox();
				count.SetValue(Grid.ColumnProperty, 2);
				mCounts.Add(count);
				grid.Children.Add(count);
				status.Add(new BagToolItem(item, count, address + (uint)i * 4));
				panel.Children.Add(grid);
			}
		}

		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Item item = Item.Instance();
			int itemCount = item.Tools.Count;
			Button button = sender as Button;
			if (button == null) return;
			int i = (int)button.Tag;
			for (; i < mMax - 1; i++)
			{
				if (mItems[i].Items.Count > itemCount + 1)
				{
					mItems[i].Items.RemoveAt(mItems[i].Items.Count - 1);
				}
				if (mItems[i + 1].Items.Count > itemCount + 1)
				{
					mItems[i].Items.Add(mItems[i + 1].Items[mItems[i + 1].Items.Count - 1]);
					mItems[i].SelectedIndex = mItems[i].Items.Count - 1;
				}
				mItems[i].Text = mItems[i + 1].Text;
				if (mItems[i + 1].Items.Count > itemCount + 1)
				{
					mItems[i + 1].Items.RemoveAt(mItems[i + 1].Items.Count - 1);
				}
				mCounts[i].Text = mCounts[i + 1].Text;
			}

			mCounts[(int)mMax - 1].Text = Item.Instance().None.Name;
			mCounts[(int)mMax - 1].Text = 0.ToString();
		}
	}
}
