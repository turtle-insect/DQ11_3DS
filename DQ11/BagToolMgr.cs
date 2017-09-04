using System.Collections.Generic;
using System.Windows.Controls;

namespace DQ11
{
	class BagToolMgr : BagBaseMgr
	{
		public override void CreateComp(List<AllStatus> status, Panel panel)
		{
			for (uint i = 0; i < OnePageCount; i++)
			{
				Grid grid = new Grid();
				grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(30) });
				grid.ColumnDefinitions.Add(new ColumnDefinition());
				grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(30) });

				Button button = new Button();
				button.Content = "削除";
				button.Tag = i;
				button.Click += ButtonDelete_Click;
				grid.Children.Add(button);

				ComboBox item = new ComboBox();
				item.SetValue(Grid.ColumnProperty, 1);
				grid.Children.Add(item);

				TextBox count = new TextBox();
				count.SetValue(Grid.ColumnProperty, 2);
				grid.Children.Add(count);
				BagToolItem toolItem = new BagToolItem(item, count, i * 4);
				status.Add(toolItem);
				mItems.Add(toolItem);
				panel.Children.Add(grid);
			}
		}
	}
}
