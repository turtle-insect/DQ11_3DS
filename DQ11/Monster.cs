using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DQ11
{
	class Monster : AllStatus
	{
		private readonly RadioButton mAll;
		private readonly StackPanel mPanel;
		private readonly TextBox mValue;

		private enum Type
		{
			All,
			None,
			Have,
		};

		public Monster(StackPanel panel, RadioButton all, RadioButton none, RadioButton have, TextBox value, Button decision)
		{
			mPanel = panel;
			mAll = all;
			mValue = value;
			all.Checked += ((x, y) => CreateList(Type.All));
			none.Checked += ((x, y) => CreateList(Type.None));
			have.Checked += ((x, y) => CreateList(Type.Have));
			decision.Click += Decision_Click;
		}

		private void Decision_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			foreach (var comp in mPanel.Children)
			{
				Grid grid = comp as Grid;
				if (grid == null) continue;
				TextBox count = grid.Children[1] as TextBox;
				if (count == null) continue;
				count.Text = mValue.Text;
			}
		}

		public override void Init()
		{
			CreateList(Type.All);
		}

		public override void Open()
		{
			mAll.IsChecked = true;
			CreateList(Type.All);
		}

		public override void Save()
		{
			SaveData savedata = SaveData.Instance();
			uint num = 1;
			foreach (var comp in mPanel.Children)
			{
				Grid grid = comp as Grid;
				if (grid == null) continue;
				TextBox count = grid.Children[1] as TextBox;
				if (count == null) continue;
				var info = count.Tag as ItemInfo;
				if (info == null) continue;
				uint value;
				if (!uint.TryParse(count.Text, out value)) continue;
				savedata.WriteNumber(Util.MonsterStartAddress + info.ID * 4, 4, value);
			}
		}

		private void CreateList(Type type)
		{
			mPanel.Children.Clear();

			SaveData savedata = SaveData.Instance();
			foreach (var info in Item.Instance().Monsters)
			{
				uint value = savedata.ReadNumber(Util.MonsterStartAddress + info.ID * 4, 4);
				bool isAppend = true;
				switch(type)
				{
					case Type.Have:
						if (value == 0) isAppend = false;
						break;

					case Type.None:
						if (value != 0) isAppend = false;
						break;
				}
				if (isAppend)
				{
					Grid grid = new Grid();
					grid.ColumnDefinitions.Add(new ColumnDefinition());
					grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(45) });

					Label name = new Label();
					name.Content = info.Name;
					name.VerticalAlignment = VerticalAlignment.Center;
					grid.Children.Add(name);

					TextBox count = new TextBox();
					count.Tag = info;
					count.Text = value.ToString();
					count.SetValue(Grid.ColumnProperty, 1);
					grid.Children.Add(count);

					mPanel.Children.Add(grid);
				}
			}
		}
	}
}
