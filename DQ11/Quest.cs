using System.Collections.Generic;
using System.Windows.Controls;

namespace DQ11
{
	class Quest : AllStatus
	{
		private readonly ListBox mQuest;
		private readonly Dictionary<ItemInfo, ComboBox> mStatus = new Dictionary<ItemInfo, ComboBox>();

		public Quest(ListBox quest)
		{
			mQuest = quest;
		}

		public override void Init()
		{
			foreach (var info in Item.Instance().Quests)
			{
				StackPanel panel = new StackPanel();
				panel.Orientation = Orientation.Horizontal;

				ComboBox status = new ComboBox();
				status.Width = 100;
				status.Items.Add("詳細不明");
				status.Items.Add("詳細判明");
				status.Items.Add("受注中");
				status.Items.Add("クリア");
				status.Items.Add("見えない");
				status.Items.Add("不定");
				panel.Children.Add(status);
				mStatus.Add(info, status);

				Label label = new Label();
				label.VerticalAlignment = System.Windows.VerticalAlignment.Center;
				label.Content = info.Name;
				panel.Children.Add(label);
				mQuest.Items.Add(panel);
			}
		}
		public override void Open()
		{
			SaveData saveData = SaveData.Instance();
			foreach (var info in Item.Instance().Quests)
			{
				if (!mStatus.ContainsKey(info)) continue;
				ComboBox status = mStatus[info];
				uint value = saveData.ReadNumber(0x6F65 + info.ID, 1);
				switch(value)
				{
					case 0x00:
						status.SelectedIndex = 0;
						break;
					case 0x01:
						status.SelectedIndex = 1;
						break;
					case 0x02:
						status.SelectedIndex = 2;
						break;
					case 0x09:
						status.SelectedIndex = 3;
						break;
					case 0xFF:
						status.SelectedIndex = 4;
						break;
					default:
						status.SelectedIndex = 5;
						break;
				}
			}
		}

		public override void Save()
		{
			SaveData saveData = SaveData.Instance();
			foreach (var info in Item.Instance().Quests)
			{
				if (!mStatus.ContainsKey(info)) continue;
				ComboBox status = mStatus[info];
				int index = status.SelectedIndex;
				if (index < 0) continue;
				if (index == 5) continue;
				uint value = 0;
				switch(index)
				{
					case 3:
						value = 9;
						break;
					case 4:
						value = 0xFF;
						break;
					default:
						value = (uint)index;
						break;
				}
				saveData.WriteNumber(0x6F65 + info.ID, 1, value);
			}
		}
	}
}
