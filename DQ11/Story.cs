using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace DQ11
{
	class Story : AllStatus
	{
		private readonly ListBox mList;
		private readonly ButtonCheckObserver mButtonCheck;
		public Story(ListBox list, Button check, Button uncheck)
		{
			mList = list;
			mButtonCheck = new ButtonCheckObserver(check, uncheck);
		}

		public override void Init()
		{
			mList.Items.Clear();
			foreach (ItemInfo info in Item.Instance().Storys)
			{
				CheckBox check = new CheckBox();
				check.Content = info;
				mList.Items.Add(check);

				mButtonCheck.Append(check);
			}
		}

		public override void Open()
		{
			SaveData saveData = SaveData.Instance();
			foreach (var item in mList.Items)
			{
				CheckBox check = item as CheckBox;
				if (check == null) continue;
				ItemInfo info = check.Content as ItemInfo;
				if (info == null) continue;
				check.IsChecked = saveData.ReadNumber(info.ID, 1) == 1;
			}
		}

		public override void Save()
		{
			SaveData saveData = SaveData.Instance();
			foreach (var item in mList.Items)
			{
				CheckBox check = item as CheckBox;
				if (check == null) continue;
				ItemInfo info = check.Content as ItemInfo;
				if (info == null) continue;
				uint value = 0;
				if (check.IsChecked == true) value = 1;
				saveData.WriteNumber(info.ID, 1, value);
			}
		}
	}
}
