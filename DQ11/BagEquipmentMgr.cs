using System.Collections.Generic;
using System.Windows.Controls;

namespace DQ11
{
	class BagEquipmentMgr : BagBaseMgr
	{
		public override void CreateComp(List<AllStatus> status, Panel panel)
		{
			for (uint i = 0; i < OnePageCount; i++)
			{
				Grid grid = new Grid();
				grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(30) });
				grid.ColumnDefinitions.Add(new ColumnDefinition());
				grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(45) });
				grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(30) });

				Button button = new Button();
				button.Content = "削除";
				button.Tag = i;
				button.Click += ButtonDelete_Click;
				grid.Children.Add(button);

				ComboBox item = new ComboBox();
				item.SetValue(Grid.ColumnProperty, 1);
				grid.Children.Add(item);

				ComboBox kind = new ComboBox();
				kind.SetValue(Grid.ColumnProperty, 2);
				grid.Children.Add(kind);

				TextBox count = new TextBox();
				count.SetValue(Grid.ColumnProperty, 3);
				grid.Children.Add(count);
				BagEquipmentItem equipItem = new BagEquipmentItem(item, kind, count, i * 4);
				status.Add(equipItem);
				mItems.Add(equipItem);
				panel.Children.Add(grid);
			}
		}
	}
}
