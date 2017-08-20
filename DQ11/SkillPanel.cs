using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace DQ11
{
	class SkillPanel : AllStatus
	{
		private readonly ListBox mList;

		public SkillPanel(ListBox list)
		{
			mList = list;
		}
		public override void Open()
		{
			mList.Items.Clear();
			List<String> names = Util.GetPartyNames();
			SaveData saveData = SaveData.Instance();
			for (uint i = 0; i < 8; i++)
			{
				CheckBox check = new CheckBox();
				check.Content = names[(int)i];
				check.IsChecked = saveData.ReadNumber(0x6A01 + i, 1) == 1;
				mList.Items.Add(check);
			}
		}

		public override void Save()
		{
			SaveData saveData = SaveData.Instance();
			for (uint i = 0; i < 8; i++)
			{
				CheckBox check = mList.Items[(int)i] as CheckBox;
				if (check == null) continue;
				uint value = 0;
				if (check.IsChecked == true) value = 1;
				saveData.WriteNumber(0x6A01 + i, 1, value);
			}
		}
	}
}
