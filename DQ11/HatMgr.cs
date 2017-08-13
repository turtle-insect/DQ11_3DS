using System.Collections.Generic;
using System.Windows.Controls;

namespace DQ11
{
	class HatMgr
	{
		public HatMgr(List<AllStatus> status, Panel panel)
		{
			Item item = Item.Instance();
			foreach(ItemInfo info in item.Hats)
			{
				Grid grid = new Grid();
				grid.ColumnDefinitions.Add(new ColumnDefinition());
				grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(45) });
				grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(30) });

				Label name = new Label();
				name.Content = info.Name;
				grid.Children.Add(name);

				TextBox ticket = new TextBox();
				status.Add(new AllNumberStatus(ticket, Util.HatStartAddress + info.ID, 1, 0, 999));
				ticket.SetValue(Grid.ColumnProperty, 1);
				grid.Children.Add(ticket);

				CheckBox obtain = new CheckBox();
				status.Add(new HatObtain(obtain, info.ID));
				obtain.SetValue(Grid.ColumnProperty, 2);
				obtain.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
				obtain.VerticalAlignment = System.Windows.VerticalAlignment.Center;
				grid.Children.Add(obtain);

				panel.Children.Add(grid);
			}
		}
	}
}
